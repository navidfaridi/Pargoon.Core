using System.Net;

namespace Pargoon.Core
{
	public class PocoResult
	{
		public HttpStatusCode StatusCode { get; set; }
		public int Code {  get; set; }
		public string? Description { get; set; }

	}
	public class TResult : PocoResult
	{
		public TResult()
		{
		}
		public TResult(int code)
		{
			Code = code;
			StatusCode = (HttpStatusCode)code;
			Success = code == 200;
		}
		public TResult(HttpStatusCode statusCode)
		{
			StatusCode= statusCode;
			Code = (int)statusCode;
			Success = statusCode == HttpStatusCode.OK;
		}
		public TResult(int code, string? description) : this(code)
		{
			Description = description;
		}
		public TResult(HttpStatusCode code, string? description) : this(code)
		{
			Description = description;
		}
		
		public bool Success { get; set; }
	}

	public class BadRequestDataResult : TResult
	{
		public BadRequestDataResult(string message) : base(HttpStatusCode.BadRequest, message) { }
	}
	public class UnauthorizedDataResult : TResult
	{
		public UnauthorizedDataResult(string message) : base(HttpStatusCode.Unauthorized, message) { }
	}
	public class ForbiddenDataResult : TResult
	{
		public ForbiddenDataResult(string message) : base(HttpStatusCode.Forbidden, message) { }
	}

	public class NotFoundDataResult : TResult
	{
		public NotFoundDataResult(string message) : base(HttpStatusCode.NotFound, message) { }
	}

	public class InternalServerErrorResult : TResult
	{
		public InternalServerErrorResult(string message) : base(HttpStatusCode.InternalServerError, message) { }
	}
}
