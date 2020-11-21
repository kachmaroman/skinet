using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class ProductsController : ControllerBase
	{
		private readonly IGenericRepository<Product> _repository;
		private readonly IMapper _mapper;

		public ProductsController(IGenericRepository<Product> repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<List<ProductDto>>> GetProductsAsync()
		{
			ProductsWithTypesAndBrandsSpecification specification = new ProductsWithTypesAndBrandsSpecification();

			IReadOnlyList<Product> products = await _repository.GetListAsync(specification);

			return _mapper.Map<List<ProductDto>>(products);
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<ActionResult<ProductDto>> GetProductAsync([FromRoute] int id)
		{
			ProductsWithTypesAndBrandsSpecification specification = new ProductsWithTypesAndBrandsSpecification(id);

			Product product = await _repository.GetEntityAsync(specification);

			return _mapper.Map<ProductDto>(product);
		}

		[HttpGet]
		[Route("brands")]
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrandsAsync()
		{
			return Ok(await _repository.GetAllAsync());
		}

		[HttpGet]
		[Route("types")]
		public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypesAsync()
		{
			return Ok(await _repository.GetAllAsync());
		}
	}
}
