using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class ProductsController : BaseApiController
	{
		private readonly IGenericRepository<Product> _repository;

		public ProductsController(IGenericRepository<Product> repository, IMapper mapper) : base(mapper)
		{
			_repository = repository;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<Pagination<ProductDto>>> GetProductsAsync([FromQuery] ProductSpecParams productParams)
		{
			ProductsWithTypesAndBrandsSpecification specification = new ProductsWithTypesAndBrandsSpecification(productParams);

			ProductWithFiltersForCountSpecification countSpecification = new ProductWithFiltersForCountSpecification(productParams);

			int totalItems = await _repository.CountAsync(countSpecification);

			IReadOnlyList<Product> products = await _repository.GetListAsync(specification);

			IReadOnlyList<ProductDto> data = Map<IReadOnlyList<ProductDto>>(products); ;

			return new Pagination<ProductDto>(productParams.PageIndex, productParams.PageSize, totalItems, data);
		}

		[HttpGet]
		[Route("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ProductDto>> GetProductAsync([FromRoute] int id)
		{
			ProductsWithTypesAndBrandsSpecification specification = new ProductsWithTypesAndBrandsSpecification(id);

			Product product = await _repository.GetEntityAsync(specification);

			if (product == null)
			{
				return NotFound(new ApiResponse(404, "Product not found"));
			}

			return Map<ProductDto>(product);
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
