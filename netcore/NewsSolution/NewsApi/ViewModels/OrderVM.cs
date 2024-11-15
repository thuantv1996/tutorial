namespace NewsApi.ViewModels
{
    public class OrderVM
    {
        public Guid Id { get; set; }
        public string Customer { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemVM> Items { get; set; }
    }
}
