using AutoMapper;
using NewsApi.Models;
using NewsApi.ViewModels;

namespace NewsApi.AutoMapperProfiles
{
    public class OrderMapperProfile : Profile
    {
        public OrderMapperProfile()
        {
            // create mapper order entity to order vm
            CreateMap<Order, OrderVM>()
                .ForMember(x => x.Customer, opt => opt.MapFrom(y => y.CustomerName))
                .ForMember(x=>x.Items, opt=>opt.MapFrom(y=>y.OrderItems)).ReverseMap();

            // create mapper for orer item entity to order item vm  
            CreateMap<OrderItem, OrderItemVM>()
                .ForMember(x => x.Product, opt => opt.MapFrom(y => y.ProductName)).ReverseMap();
        }
    }
}
