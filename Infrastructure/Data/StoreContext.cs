using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data
{
	public class StoreContext : DbContext
	{
		public StoreContext(DbContextOptions<StoreContext> options) : base(options)
		{ }

		public DbSet<Product> Products { get; set; }

		public DbSet<ProductBrand> ProductBrands { get; set; }

		public DbSet<ProductType> ProductTypes { get; set; }

		public DbSet<Order> Orders { get; set; }

		public DbSet<OrderItem> OrderItems { get; set; }

		public DbSet<DeliveryMethod> DeliveryMethods { get; set; }	

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
			{
				foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
				{
					IEnumerable<PropertyInfo> decimalProperties =
						entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));

					IEnumerable<PropertyInfo> dateTimeOffsetProperties = entityType.ClrType.GetProperties()
						.Where(p => p.PropertyType == typeof(DateTimeOffset));

					foreach (PropertyInfo propertyInfo in decimalProperties)
					{
						modelBuilder.Entity(entityType.Name).Property(propertyInfo.Name).HasConversion<double>();
					}

					foreach (PropertyInfo propertyInfo in dateTimeOffsetProperties)
					{
						modelBuilder.Entity(entityType.Name).Property(propertyInfo.Name).HasConversion(new DateTimeOffsetToBinaryConverter());
					}
				}
			}
		}
	}
}
 