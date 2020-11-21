using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
	public class StoreContextSeed
	{
		public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
		{
			try
			{
				if (!context.ProductBrands.Any())
				{
					string brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");

					List<ProductBrand> brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

					brands.ForEach(brand => context.ProductBrands.Add(brand));

					await context.SaveChangesAsync();
				}

				if (!context.ProductTypes.Any())
				{
					string typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");

					List<ProductType> types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

					types.ForEach(type => context.ProductTypes.Add(type));

					await context.SaveChangesAsync();
				}

				if (!context.Products.Any())
				{
					string productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");

					List<Product> products = JsonSerializer.Deserialize<List<Product>>(productsData);

					products.ForEach(product => context.Products.Add(product));

					await context.SaveChangesAsync();
				}
			}
			catch (Exception ex)
			{
				loggerFactory.CreateLogger<StoreContextSeed>().LogError(ex.Message);
			}
		} 
	}
}
