using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class BasketController : BaseApiController
	{
		private readonly IBasketRepository _basketRepository;

		public BasketController(IMapper mapper, IBasketRepository basketRepository) : base(mapper)
		{
			_basketRepository = basketRepository;
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<ActionResult<CustomerBasket>> GetBasketByIdAsync([FromRoute] string id)
		{
			CustomerBasket basket = await _basketRepository.GetBasketAsync(id);

			return Ok(basket ?? new CustomerBasket(id));
		}

		[HttpPost]
		public async Task<ActionResult<CustomerBasketDto>> UpdateBasketAsync([FromBody] CustomerBasketDto basketDto)
		{
			CustomerBasket basket = Map<CustomerBasket>(basketDto);

			CustomerBasket updated = await _basketRepository.UpdateBasketAsync(basket);

			return Map<CustomerBasketDto>(updated);
		}

		[HttpDelete]
		[Route("{id}")]
		public async Task DeleteBasketAsync([FromRoute] string id)
		{
			await _basketRepository.DeleteBasketAsync(id);
		}
	}
}
