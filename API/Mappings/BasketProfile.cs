using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Mappings
{
	public class BasketProfile : Profile
	{
		public BasketProfile()
		{
			CreateMap<CustomerBasketDto, CustomerBasket>().ReverseMap();

			CreateMap<BasketItemDto, BasketItem>().ReverseMap();
		}
	}
}
