using System;
using System.Net;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionMiddleware> _logger;
		private readonly IHostEnvironment _environment;

		public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment environment)
		{
			_next = next;
			_logger = logger;
			_environment = environment;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);

				int statusCode = (int)HttpStatusCode.InternalServerError;

				context.Response.ContentType = "application/json";
				context.Response.StatusCode = statusCode;

				ApiException response = _environment.IsDevelopment()
					? new ApiException(statusCode, ex.Message, ex.StackTrace)
					: new ApiException(statusCode, ex.Message);

				await context.Response.WriteAsJsonAsync(response);
			}
		}
	}
}
