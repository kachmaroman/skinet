using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Authorize]
	public class OrdersController : BaseApiController
	{
		private readonly IOrderService _orderService;

		public OrdersController(IMapper mapper, IOrderService orderService) : base(mapper)
		{
			_orderService = orderService;
		}

		[HttpPost]
		public async Task<ActionResult<OrderToReturnDto>> CreateOrderAsync([FromBody] OrderDto orderDto)
		{
			string email = HttpContext.User.RetreiveEmailFromPrincipal();

			Address address = Map<Address>(orderDto.ShipToAddress);

			Order order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);

			if (order == null)
			{
				return BadRequest(new ApiResponse(400, "Problem creating order"));
			}

			return Map<OrderToReturnDto>(order);
		}

		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersAsync()
		{
			string email = HttpContext.User.RetreiveEmailFromPrincipal();
			
			IReadOnlyList<Order> orders = await _orderService.GetOrdersForUserAsync(email);

			return Ok(Map<IReadOnlyList<OrderToReturnDto>>(orders));
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdAsync([FromRoute] int id)
		{
			string email = HttpContext.User.RetreiveEmailFromPrincipal();

			Order order = await _orderService.GetOrderByIdAsync(id, email);

			if (order == null)
			{
				return NotFound(new ApiResponse(404));
			}

			return Map<OrderToReturnDto>(order);
		}

		[HttpGet]
		[Route("delivery-methods")]
		public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethodsAsync()
		{
			return Ok(await _orderService.GetDeliveryMethodsAsync());
		}
	}
}
