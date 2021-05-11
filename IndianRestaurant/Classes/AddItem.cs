using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndianRestaurant
{
    public class AddItem
    {
        public int id { get; set; }
        public string menuName { get; set; }
        public string category { get; set; }

        public float price { get; set; }
        public string quantity { get; set; }
    }
}
