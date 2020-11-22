using System.Collections.Generic;
using System.Linq;
using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
	public static class ApplicationServicesExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped<IGenericRepository, GenericRepository>();
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = actionContext =>
				{
					IEnumerable<string> errors = actionContext.ModelState
						.Where(mse => mse.Value.Errors.Count > 0)
						.SelectMany(mse => mse.Value.Errors)
						.Select(me => me.ErrorMessage);

					return new BadRequestObjectResult(new ApiValidationErrorResponse(errors));
				};
			});

			return services;
		}
	}
}
