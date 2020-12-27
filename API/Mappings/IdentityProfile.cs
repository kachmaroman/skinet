using API.Dtos;
using API.Resolvers;
using AutoMapper;
using Core.Entities.Identity;

namespace API.Mappings
{
	public class IdentityProfile : Profile
	{
		public IdentityProfile()
		{
			CreateMap<AppUser, UserDto>()
				.ForMember(dto => dto.Token, src => src.MapFrom<IdentityTokenResolver>());

			CreateMap<RegisterDto, AppUser>()
				.ForMember(entity => entity.UserName, src => src.MapFrom(dto => dto.Email));

			CreateMap<Address, AddressDto>().ReverseMap();
		}
	}
}
