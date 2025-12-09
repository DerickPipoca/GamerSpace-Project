using AutoMapper;
using GamerSpace.Application.DTOs;
using GamerSpace.Domain.Entities;

namespace GamerSpace.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(x => x.Price, opt => opt.MapFrom(src => src.Variants.Any() ? src.Variants.Min(v => v.Price) : 0))
                .ForMember(x => x.CategoryIds, opt => opt.MapFrom(src => src.ProductCategories.Select(pc => pc.CategoryId)))
                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(src => src.Variants.FirstOrDefault() != null ? src.Variants.FirstOrDefault()!.ImageUrl : null));

            CreateMap<ProductVariant, ProductVariantDto>();

            CreateMap<ClassificationType, ClassificationTypeDto>();
            CreateMap<Category, CategoryDto>()
                .ForMember(x => x.TypeName, opt => opt.MapFrom(src => src.Type.Name));

            CreateMap<User, UserDto>();

            CreateMap<Order, OrderDto>();
            CreateMap<OrderProduct, OrderProductDto>()
                .ForMember(x => x.SKU, opt => opt.MapFrom(src => src.ProductVariant.SKU));
        }
    }
}