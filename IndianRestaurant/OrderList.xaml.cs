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
using System.Windows.Shapes;

namespace IndianRestaurant
{
    /// <summary>
    /// Interaction logic for OrderList.xaml
    /// </summary>
    public partial class OrderList : Window
    {
        public OrderList()
        {
            InitializeComponent();
        }

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

        private void Btn_PlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            var win = new MainWindow();
            win.Width = Width;
            win.Height = Height;
            win.Title = "The Taste of India";
            win.Owner = this;
            win.Show();
            Visibility = Visibility.Collapsed;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Lbx_OrderList.ItemsSource = App._order;
        }

        private void Lbx_OrderList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void Tbx_Filter_Order_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (App._order == null) return;
            var filter = (sender as TextBox).Text;
            var result = from s in App._order where s.order_id.ToString().Contains(filter) || s.id.ToString().Contains(filter) select s;
            Lbx_OrderList.ItemsSource = result;
            TxtBlc_SearchCount.Text = Lbx_OrderList.Items.Count.ToString();
        }
    }
}
