using System;
using Core.Entities;

namespace Core.Specifications
{
	public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
	{
		public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams) 
			: base(x => (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) && (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId))
		{
			Include(x => x.ProductBrand);
			Include(x => x.ProductType);
			AddOrderBy(x => x.Name);
			ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

			if (string.IsNullOrEmpty(productParams.Sort))
			{
				return;
			}

			switch (productParams.Sort)
			{
				case "priceAsc":
					AddOrderBy(p => p.Price);
					break;
				case "priceDesc":
					AddOrderByDescending(p => p.Price);
					break;
				default:
					AddOrderBy(p => p.Name);
					break;
			}
		}

		public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
		{
			Include(x => x.ProductBrand);
			Include(x => x.ProductType);
		}
	}
}
