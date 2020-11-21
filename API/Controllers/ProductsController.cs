using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class ProductsController : ControllerBase
	{
		private readonly IProductRepository _repository;

		public ProductsController(IProductRepository repository)
		{
			_repository = repository;
		}

		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<Product>>> GetProductsAsync()
		{
			return Ok(await _repository.GetProductsAsync());
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<ActionResult<Product>> GetProductAsync([FromRoute] int id)
		{
			return await _repository.GetProductByIdAsync(id);
		}

		[HttpGet]
		[Route("brands")]
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrandsAsync()
		{
			return Ok(await _repository.GetProductBrandsAsync());
		}

		[HttpGet]
		[Route("types")]
		public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypesAsync()
		{
			return Ok(await _repository.GetProductTypesAsync());
		}
	}
}
