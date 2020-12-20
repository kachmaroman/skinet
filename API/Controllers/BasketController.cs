using System.Threading.Tasks;
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
		public async Task<ActionResult<CustomerBasket>> GetBasketByIdAsync([FromQuery] string id)
		{
			CustomerBasket basket = await _basketRepository.GetBasketAsync(id);

			return Ok(basket ?? new CustomerBasket(id));
		}

		[HttpPost]
		public async Task<ActionResult<CustomerBasket>> UpdateBasketAsync([FromBody] CustomerBasket basket)
		{
			CustomerBasket updated = await _basketRepository.UpdateBasketAsync(basket);

			return Ok(updated);
		}

		[HttpDelete]
		[Route("{id}")]
		public async Task DeleteBasketAsync([FromRoute] string id)
		{
			await _basketRepository.DeleteBasketAsync(id);
		}
	}
}
