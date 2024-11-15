namespace NewsApi.Models
{
    public class NewsDbContext
    {
        public static List<News> News = new List<News>();

        public static List<Order> Order = new List<Order>
        {
            new Order
            {
                Id = Guid.Empty,
                CustomerName = "Từ Vạn Thuận",
                TotalAmount = 1000000,
                CreatedDate = DateTime.UtcNow,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        ProductName = "Sản phẩm 1",
                        Quantity = 10
                    },
                    new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        ProductName = "Sản phẩm 2",
                        Quantity = 5
                    },
                    new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        ProductName = "Sản phẩm 3",
                        Quantity = 7
                    }
                }
            },
            new Order
            {
                Id = Guid.NewGuid(),
                CustomerName = "Từ Vạn Thuận 2",
                TotalAmount = 5000000,
                CreatedDate = DateTime.UtcNow,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        ProductName = "Sản phẩm 5",
                        Quantity = 4
                    },
                    new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        ProductName = "Sản phẩm 8",
                        Quantity = 4
                    },
                    new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        ProductName = "Sản phẩm 9",
                        Quantity = 17
                    }
                }
            }
        };
    }
}
