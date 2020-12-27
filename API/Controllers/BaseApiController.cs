using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class BaseApiController : ControllerBase
	{
		protected readonly IMapper Mapper;

		public BaseApiController(IMapper mapper)
		{
			Mapper = mapper;
		}

		protected TDestination Map<TDestination>(object source)
		{
			return Mapper.Map<TDestination>(source);
		}

		protected TDestination Map<TSource, TDestination>(TSource source)
		{
			return Mapper.Map<TSource, TDestination>(source);
		}
	}
}
