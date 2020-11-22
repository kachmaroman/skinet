using API.Errors;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("errors/{code}")]
	[ApiExplorerSettings(IgnoreApi = true)]
	public class ErrorController : BaseApiController
	{
		public ErrorController(IMapper mapper) : base(mapper)
		{ }

		public ActionResult<ApiResponse> Error(int code)
		{
			return new ApiResponse(code);
		}
	}
}
