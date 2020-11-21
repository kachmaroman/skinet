using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
	public interface IGenericRepository
	{
		Task<Product> GetProductByIdAsync(int id);

		Task<IReadOnlyList<Product>> GetProductsAsync();

		Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();

		Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
	}
}