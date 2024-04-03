using System.Net;

namespace Pargoon.Core
{
	public class PocoResult
	{
		public HttpStatusCode StatusCode { get; set; }
		public int Code {  get; set; }
		public string? Description { get; set; }

	}
	public class Result : PocoResult
	{
		public Result()
		{
		}
		public Result(int code)
		{
			Code = code;
			StatusCode = (HttpStatusCode)code;
			Success = code == 200;
		}
		public Result(HttpStatusCode statusCode)
		{
			StatusCode= statusCode;
			Code = (int)statusCode;
			Success = statusCode == HttpStatusCode.OK;
		}
		public Result(int code, string? description) : this(code)
		{
			Description = description;
		}
		public Result(HttpStatusCode code, string? description) : this(code)
		{
			Description = description;
		}
		public Result(Result result) : this(result.Code, result.Description) { }
		public bool Success { get; set; }
	}

	public class BadRequestDataResult : Result
	{
		public BadRequestDataResult(string message) : base(HttpStatusCode.BadRequest, message) { }
	}
	public class UnauthorizedDataResult : Result
	{
		public UnauthorizedDataResult(string message) : base(HttpStatusCode.Unauthorized, message) { }
	}
	public class ForbiddenDataResult : Result
	{
		public ForbiddenDataResult(string message) : base(HttpStatusCode.Forbidden, message) { }
	}

	public class NotFoundDataResult : Result
	{
		public NotFoundDataResult(string message) : base(HttpStatusCode.NotFound, message) { }
	}

	public class InternalServerErrorResult : Result
	{
		public InternalServerErrorResult(string message) : base(HttpStatusCode.InternalServerError, message) { }
	}
}
