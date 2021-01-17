﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly StoreContext _context;
		private Hashtable _repositories;

		public UnitOfWork(StoreContext context)
		{
			_context = context;
		}

		public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
		{
			_repositories ??= new Hashtable();

			string type = typeof(TEntity).Name;

			if (!_repositories.ContainsKey(type))
			{
				Type repositoryType = typeof(GenericRepository<>);
				object repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

				_repositories.Add(type, repositoryInstance);
			}

			return (IGenericRepository<TEntity>) _repositories[type];
		}

		public async Task<int> Complete()
		{
			return await _context.SaveChangesAsync();
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
