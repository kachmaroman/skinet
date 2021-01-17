using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;

namespace Infrastructure.Services
{
	public class OrderService : IOrderService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IBasketRepository _basketRepo;

		public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepo)
		{
			_unitOfWork = unitOfWork;
			_basketRepo = basketRepo;
		}

		public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address address)
		{
			CustomerBasket basket = await _basketRepo.GetBasketAsync(basketId);

			List<OrderItem> items = new List<OrderItem>();

			foreach (BasketItem item in basket.Items)
			{
				Product productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
				ProductItemOrdered itemOrdered = new(productItem);
				OrderItem orderItem = new(itemOrdered, productItem.Price, item.Quantity);

				items.Add(orderItem);
			}

			DeliveryMethod deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

			decimal subtotal = items.Sum(item => item.Price * item.Quantity);

			Order order = new(items, buyerEmail, address, deliveryMethod, subtotal);

			_unitOfWork.Repository<Order>().Add(order);

			int result = await _unitOfWork.Complete();

			if (result <= 0)
			{
				return null;
			}

			await _basketRepo.DeleteBasketAsync(basketId);

			return order;
		}

		public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
		{
			OrdersWithItemsAndOrderingSpecification spec = new(buyerEmail);

			return await _unitOfWork.Repository<Order>().GetListAsync(spec);
		}

		public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
		{
			OrdersWithItemsAndOrderingSpecification spec = new(id, buyerEmail);

			return await _unitOfWork.Repository<Order>().GetEntityAsync(spec);
		}

		public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
		{
			return await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
		}
	}
}
