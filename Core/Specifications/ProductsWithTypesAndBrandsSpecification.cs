using Core.Entities;

namespace Core.Specifications
{
	public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
	{
		public ProductsWithTypesAndBrandsSpecification()
		{
			Include(x => x.ProductBrand);
			Include(x => x.ProductType);
		}

		public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
		{
			Include(x => x.ProductBrand);
			Include(x => x.ProductType);
		}
	}
}
