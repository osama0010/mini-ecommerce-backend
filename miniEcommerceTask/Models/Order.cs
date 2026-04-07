namespace miniEcommerceTask.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
