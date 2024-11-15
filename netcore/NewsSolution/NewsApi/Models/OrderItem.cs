namespace NewsApi.Models
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}
