using System.Collections.Generic;

namespace API.Errors
{
	public class ApiValidationErrorResponse : ApiResponse
	{
		public IEnumerable<string> Errors { get; }

		public ApiValidationErrorResponse(IEnumerable<string> errors) : base(400)
		{
			Errors = errors;
		}
	}
}
