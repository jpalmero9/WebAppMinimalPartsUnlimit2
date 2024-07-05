using AutoMapper;
using WebAppMinimalPartsUnlimit2.Entities.Dtos;
using WebAppMinimalPartsUnlimit2.Entities.Models;

namespace WebAppMinimalPartsUnlimit2.AutoMapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductCreateDto, Product>()
                .ForMember(dest => dest.ProductGuid, opt => opt.MapFrom(src => Guid.NewGuid()));

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryCreateDto, Category>();
        }
    }
}
