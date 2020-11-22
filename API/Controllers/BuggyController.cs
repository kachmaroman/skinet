using System;
using API.Errors;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class BuggyController : BaseApiController
	{
		public BuggyController(IMapper mapper) : base(mapper)
		{ }

		[HttpGet]
		[Route("notfound")]
		public ActionResult GetNotFoundRequest()
		{
			return NotFound(new ApiResponse(404));
		}

		[HttpGet]
		[Route("badrequest")]
		public ActionResult GetBadRequest()
		{
			return NotFound(new ApiResponse(400));
		}

		[HttpGet("badrequest/{id}")]
		public ActionResult GetNotFoundRequest(int id)
		{
			return Ok();
		}

		[HttpGet("servererror")]
		public ActionResult GetServerError()
		{
			throw new NullReferenceException();
		}

	}
}
