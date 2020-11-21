using System.Linq;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
	public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
	{
		public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> sourceQuery, ISpecification<TEntity> specification)
		{
			if (sourceQuery == null || specification == null)
			{
				return sourceQuery;
			}

			IQueryable<TEntity> query = sourceQuery;

			if (specification.Criteria != null)
			{
				query = query.Where(specification.Criteria);
			}

			query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

			return query;
		}
	}
}
