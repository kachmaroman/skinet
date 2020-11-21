using API.Dtos;
using API.Resolvers;
using AutoMapper;
using Core.Entities;

namespace API.Mappings
{
	public class ProductProfile : Profile
	{
		public ProductProfile()
		{
			CreateMap<Product, ProductDto>()
				.ForMember(dto => dto.Brand, src => src.MapFrom(entity => entity.ProductBrand.Name))
				.ForMember(dto => dto.Type, src => src.MapFrom(entity => entity.ProductType.Name))
				.ForMember(dto => dto.PictureUrl, src => src.MapFrom<ProductUrlResolver>());
		}
	}
}
