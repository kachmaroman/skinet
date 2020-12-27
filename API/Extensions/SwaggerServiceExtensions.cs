using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace API.Extensions
{
	public static class SwaggerServiceExtensions
	{
		public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
		{
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "SkiNet API",
					Version = "v1"
				});

				OpenApiSecurityScheme securitySchema = new()
				{
					Name = "Authorization",
					Description = "JWT Auth Bearer Scheme",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.Http,
					Scheme = "bearer",
					Reference = new OpenApiReference
					{
						Type = ReferenceType.SecurityScheme,
						Id = "Bearer"
					}
				};

				OpenApiSecurityRequirement securityRequirement = new() { { securitySchema, new[] { "Bearer" } } };

				options.AddSecurityDefinition("Bearer", securitySchema);

				options.AddSecurityRequirement(securityRequirement);
			});

			return services;
		}

		public static IApplicationBuilder UseSwaggerDocumention(this IApplicationBuilder app)
		{
			app.UseSwagger();
			app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "SkiNet API v1"));

			return app;
		}
	}
}
