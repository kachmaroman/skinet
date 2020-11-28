using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly StoreContext _context;

		public GenericRepository(StoreContext context)
		{
			_context = context;
		}

		public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);

		public async Task<IReadOnlyList<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

		public async Task<T> GetEntityAsync(ISpecification<T> specification = null) => await ApplySpecification(specification).FirstOrDefaultAsync();

		public async Task<IReadOnlyList<T>> GetListAsync(ISpecification<T> specification = null) => await ApplySpecification(specification).ToListAsync();

		public async Task<int> CountAsync(ISpecification<T> specification) => await ApplySpecification(specification).CountAsync();

		private IQueryable<T> ApplySpecification(ISpecification<T> specification) => SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), specification);
	}
}
