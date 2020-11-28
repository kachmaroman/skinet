using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
	public class BaseSpecification<T> : ISpecification<T>
	{
		public BaseSpecification()
		{
			Includes = new List<Expression<Func<T, object>>>();
		}

		public BaseSpecification(Expression<Func<T, bool>> criteria) : this()
		{
			Criteria = criteria;
		}

		public Expression<Func<T, bool>> Criteria { get; }
		public List<Expression<Func<T, object>>> Includes { get; }
		public Expression<Func<T, object>> OrderBy { get; private set; }
		public Expression<Func<T, object>> OrderByDescending { get; private set; }
		public int Take { get; private set; }
		public int Skip { get; private set; }
		public bool IsPagingEnabled { get; private set; }

		protected BaseSpecification<T> Include(Expression<Func<T, object>> include)
		{
			Includes.Add(include);

			return this;
		}

		protected void AddOrderBy(Expression<Func<T, object>> orderByExpressions) => OrderBy = orderByExpressions;

		protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression) => OrderByDescending = orderByDescExpression;

		protected void ApplyPaging(int skip, int take)
		{
			Skip = skip;
			Take = take;
			IsPagingEnabled = true;
		;}
	}
}