using System;
using Core.Entities;

namespace Core.Specifications
{
	public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
	{
		public ProductsWithTypesAndBrandsSpecification(string sort)
		{
			Include(x => x.ProductBrand);
			Include(x => x.ProductType);
			AddOrderBy(x => x.Name);

			if (string.IsNullOrEmpty(sort))
			{
				return;
			}

			switch (sort)
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
