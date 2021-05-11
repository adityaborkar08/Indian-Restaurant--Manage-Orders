namespace IndianRestaurant
{
    public class Orders
    {
        public int order_id { get; set; }
        public int id { get; set; } //table_id
        public string date { get; set; }
        public string items { get; set; }
        public int quantity { get; set; }
        public float amount { get; set; }
        public string billing_status { get; set; }
        public string action { get; set; }

    }
}