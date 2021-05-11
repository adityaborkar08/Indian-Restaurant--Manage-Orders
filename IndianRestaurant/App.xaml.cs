using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace IndianRestaurant
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ObservableCollection<Menu> _menu;
        public static ObservableCollection<Orders> _order;
        public static ObservableCollection<Summary> _summary;
        public static ObservableCollection<AddItem> _addItem;

        public void Application_Startup(object sender, StartupEventArgs e)
        {
           
            _menu = My_Storage.ReadXml<ObservableCollection<Menu>>("MenuList.xml");
            if (_menu == null)
                _menu = new ObservableCollection<Menu>();

            _order = My_Storage.ReadXml<ObservableCollection<Orders>>("OrderList.xml");
            if (_order == null)
                _order = new ObservableCollection<Orders>();

            _addItem = My_Storage.ReadXml<ObservableCollection<AddItem>>("AddItem.xml");
            if (_addItem == null)
                _addItem = new ObservableCollection<AddItem>();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            //My_Storage.WriteXml(App._students, "Students.xml");

            //var dishes = new ObservableCollection<Menu> { new Menu { id = 0, menuName = "a", category= "abc", price= 3.99F }, new Menu { id = 0, menuName = "a", category = "abc", price= 3.99F },
            //                              new Menu { id = 0, menuName = "a", category= "abc", price= 3.99F }, };

            //var orders = new ObservableCollection<Orders> { new Orders { order_id = 0 ,id = 0, date = "12/02/2021", items = "Chicken Tikka", quantity = 1, amount= 17.99F, billing_status= "abc", action= "0" },
            //                                                new Orders { order_id = 0 ,id = 0, date = "12/02/2021", items = "Chicken Tikka", quantity = 1, amount= 17.99F, billing_status= "abc", action= "0" },
            //                                                new Orders { order_id = 0 ,id = 0, date = "12/02/2021", items = "Chicken Tikka", quantity = 1, amount= 17.99F, billing_status= "abc", action= "0" } };
            My_Storage.WriteXml(App._order, "OrderList.xml");
            //My_Storage.WriteXml(orders, "OrderList.xml");
            My_Storage.WriteXml(App._menu, "MenuList.xml");
        }

    }
}
