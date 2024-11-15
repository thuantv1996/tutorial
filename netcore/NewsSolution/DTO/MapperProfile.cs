using AutoMapper;
using DAL.Models;

namespace DTO
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Category, CategoryDTO>()
                .ForMember(x => x.ParentId, otp => otp.MapFrom(x => x.CategoryId));

            CreateMap<CategoryDTO, Category>()
                   .ForMember(x=>x.Id, opt=>opt.Ignore())
                   .ForMember(x => x.CategoryId, otp => otp.MapFrom(x => x.ParentId))
                   .ForMember(x=>x.News, opt=>opt.Ignore());

            CreateMap<News, NewsDTO>()
               .ReverseMap();
        }
    }
}
