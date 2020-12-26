using API.Dtos;
using AutoMapper;
using Core.Entities.Identity;

namespace API.Mappings
{
	public class IdentityProfile : Profile
	{
		public IdentityProfile()
		{
			CreateMap<AppUser, UserDto>();

			CreateMap<RegisterDto, AppUser>()
				.ForMember(entity => entity.UserName, src => src.MapFrom(dto => dto.Email));

			CreateMap<Address, AddressDto>().ReverseMap();
		}
	}
}
