using Core.Entities.OrderAggregate;

namespace Core.Specifications
{
	public class OrdersWithItemsAndOrderingSpecification : BaseSpecification<Order>
	{
		public OrdersWithItemsAndOrderingSpecification(string email) : base(o => o.BuyerEmail == email)
		{
			Include(o => o.OrderItems);
			Include(o => o.DeliveryMethod);
			AddOrderByDescending(o => o.OrderDate);
		}

		public OrdersWithItemsAndOrderingSpecification(int id, string email) : base(o => o.Id == id && o.BuyerEmail == email)
		{
			Include(o => o.OrderItems);
			Include(o => o.DeliveryMethod);
		}
	}
}
