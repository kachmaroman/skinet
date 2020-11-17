using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductsController : ControllerBase
	{
		private readonly StoreContext _storeContext;

		public ProductsController(StoreContext storeContext)
		{
			_storeContext = storeContext;
		}

		[HttpGet]
		public async Task<List<Product>> GetProductsAsync()
		{
			return await _storeContext.Products.ToListAsync();
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<Product> GetProductAsync([FromRoute] int id)
		{
			return await _storeContext.Products.FindAsync(id);
		}
	}
}
