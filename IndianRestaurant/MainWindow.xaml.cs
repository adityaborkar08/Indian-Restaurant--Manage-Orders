using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IndianRestaurant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
     
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Lbx_Menu.ItemsSource = App._menu;
            Lbx_Summary.ItemsSource = App._summary;
        }


        private ObservableCollection<Menu> CreateMenuList(int cnt)
        {
            var lst = new ObservableCollection<Menu>();
            for (int i = 0; i < cnt; i++)
            {
                lst.Add(new Menu { menuName = $"menuName{i}"});
            }
            return lst;
        }

        private void Tbx_Filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App._menu == null) return;
            var filter = (sender as TextBox).Text;
            var result = from s in App._menu where s.menuName.ToLower().Contains(filter) || s.menuName.ToUpper().Contains(filter) select s;
            Lbx_Menu.ItemsSource = result;
            TxtBlc_SearchCount.Text = Lbx_Menu.Items.Count.ToString();
        }


        private void Cbx_Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (App._menu == null) return;
            var filter = (sender as ComboBox).SelectedItem.ToString().ToLower();
            var result = from b in App._menu where b.category.ToLower().Contains(filter) select b;
            Lbx_Menu.ItemsSource = result;
            TxtBlc_SearchCount.Text = Lbx_Menu.Items.Count.ToString();
        }

        private void Lbx_Menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Lbx_Menu.SelectedItem != null)
            {
                Lbx_Summary.Items.Add(Lbx_Menu.SelectedItem);
                TxtBlc_NumberOfItems.Text = Lbx_Summary.Items.Count.ToString();

                AddItem addedMenu = new AddItem();
                var selectedMenu = (sender as ListBox).SelectedItem as Menu;
                var addedList = from p in App._menu where p.id == selectedMenu.id select p;
                foreach (var item in addedList)
                {
                    if (item != null)
                    {
                        addedMenu = new AddItem()
                        {
                            id = item.id,
                            menuName = item.menuName,
                            category = item.category,
                            quantity = item.quantity,
                            price = item.price,
                        };
                        App._addItem.Add(addedMenu);
                    }
                }
            }
            AddTotalAmount(App._addItem);
        }

        private void AddTotalAmount(ObservableCollection<AddItem> summary_orders)
        {
            float totalAmount = summary_orders.Select(x => x.price).Sum();
            TxtBlc_Sum.Text = totalAmount.ToString();
        }

        //Side Menu

        DoubleAnimation da = new DoubleAnimation(40, TimeSpan.FromSeconds(1.5));
        private void Grd_SideMenu_MouseEnter(object sender, MouseEventArgs e)
        {
            da.To = 100;
            Grd_SideMenu.BeginAnimation(Grid.WidthProperty, da);
        }

        private void Grd_SideMenu_MouseLeave(object sender, MouseEventArgs e)
        {
            da.To = 20;
            Grd_SideMenu.BeginAnimation(Grid.WidthProperty, da);
        }

        private void Btn_Delete_Item_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            index = Lbx_Summary.SelectedIndex;
            if (index >= 0)
            {
                Lbx_Summary.Items.RemoveAt(index);
                App._addItem.RemoveAt(index);
            }
            AddTotalAmount(App._addItem);
            TxtBlc_NumberOfItems.Text = Lbx_Summary.Items.Count.ToString();

        }

        private void Btn_Order_list_Click(object sender, RoutedEventArgs e)
        {
            var win = new OrderList();
            win.Width = Width;
            win.Height = Height;
            win.Title = "The Taste of India";
            win.Owner = this;
            win.Show();
            Visibility = Visibility.Collapsed;
        }

        private void Btn_ClearAll_Tems_Click(object sender, RoutedEventArgs e)
        {
            var msg = MessageBox.Show("Are you sure you want to delete order ? \n please click \n\n 'Yes' to delete order; \n 'No' to Cancel \n ",
                                        "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Cancel);



            switch (msg)
            {
                case MessageBoxResult.None:
                    break;
                case MessageBoxResult.OK:
                    break;
                case MessageBoxResult.Cancel:
                    break;
                case MessageBoxResult.Yes:
                    Lbx_Summary.Items.Clear();
                    TxtBlc_NumberOfItems.Text = "0";
                    TxtBlc_Sum.Text = "0";
                    break;
                case MessageBoxResult.No:
                    break;
                default:
                    break;
            }
        }

        private void Txt_Quantity_SelectionChanged(object sender, RoutedEventArgs e)
        {

            int filter = int.Parse((sender as TextBox).Text);
            var selectedItem = (Menu)Lbx_Summary.SelectedItem;
            if (selectedItem != null)
            {
                float sum = filter * selectedItem.price;
                App._addItem.FirstOrDefault(a => a.id == selectedItem.id).price = sum;
                AddTotalAmount(App._addItem);
            }
        }
    }
}
