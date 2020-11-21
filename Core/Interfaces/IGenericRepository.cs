using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
	public interface IGenericRepository<T> where T : BaseEntity
	{
		Task<T> GetByIdAsync(int id);

		Task<IReadOnlyList<T>> GetAllAsync();

		Task<T> GetEntityAsync(ISpecification<T> specification = null);

		Task<IReadOnlyList<T>> GetListAsync(ISpecification<T> sspecificationpec = null);
	}
}