using API.Dtos;
using API.Resolvers;
using AutoMapper;
using Core.Entities.OrderAggregate;

namespace API.Mappings
{
	public class OrderProfile : Profile
	{
		public OrderProfile()
		{
			CreateMap<AddressDto, Address>();

			CreateMap<Order, OrderToReturnDto>()
				.ForMember(dto => dto.DeliveryMethod, o => o.MapFrom(e => e.DeliveryMethod.ShortName))
				.ForMember(dto => dto.ShippingPrice, o => o.MapFrom(e => e.DeliveryMethod.Price));

			CreateMap<OrderItem, OrderItemDto>()
				.ForMember(dto => dto.ProductId, o => o.MapFrom(e => e.ItemOrdered.ProductItemId))
				.ForMember(dto => dto.ProductName, o => o.MapFrom(e => e.ItemOrdered.ProductName))
				.ForMember(dto => dto.PictureUrl, o => o.MapFrom<OrderItemUrlResolver>());
		}
	}
}
