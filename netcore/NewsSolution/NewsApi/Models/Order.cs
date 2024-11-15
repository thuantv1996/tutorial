namespace NewsApi.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
