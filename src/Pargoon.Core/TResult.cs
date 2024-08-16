using System;
using System.Net;
using System.Runtime.Serialization;

namespace Pargoon.Core;

[Serializable]
public class PocoResult
{
	[DataMember(Name = "statusCode")]
	public HttpStatusCode StatusCode { get; set; }
	[DataMember(Name = "code")]
	public int Code { get; set; }
	[DataMember(Name = "description")]
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
		IsSuccess = code == 200;
	}
	public TResult(HttpStatusCode statusCode)
	{
		StatusCode = statusCode;
		Code = (int)statusCode;
		IsSuccess = statusCode == HttpStatusCode.OK;
	}
	public TResult(int code, string? description) : this(code)
	{
		Description = description;
	}
	public TResult(HttpStatusCode code, string? description) : this(code)
	{
		Description = description;
	}

	public bool IsSuccess { get; }
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

public class TResult<T> : TResult
{
	public static TResult<T> Success(T value, string description = "")
	{
		return new TResult<T>(HttpStatusCode.OK, description, value);
	}

	public static TResult<T> Fail(int code, string? description)
	{
		return new TResult<T>(code, description);
	}
	public TResult() { }
	public TResult(int code) : base(code) { }
	public TResult(HttpStatusCode statusCode) : base(statusCode) { }
	public TResult(int code, string? description) : base(code, description) { }
	public TResult(HttpStatusCode statusCode, string? description) : base(statusCode, description) { }
	public TResult(int code, string? description, T? data) : base(code, description)
	{
		Data = data;
	}
	public TResult(HttpStatusCode statusCode, string? description, T? data) : base(statusCode, description)
	{
		Data = data;
	}

	public TResult(string? description, T? data) : base(200, description)
	{
		Data = data;
	}
	[DataMember(Name = "data")]
	public T? Data { get; set; }
}
