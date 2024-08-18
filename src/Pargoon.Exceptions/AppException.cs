using System;
using System.Net;

namespace Pargoon.Exceptions;

public class AppException : Exception
{
	public string ErrorCode { get; set; }
	public HttpStatusCode Status { get; set; } = HttpStatusCode.InternalServerError;
	public AppException(string message) : base(message) { ErrorCode = "0000"; }
	public AppException(AppError error) : base(error.ErrorMessage)
	{
		this.ErrorCode = error.ErrorCode;
		this.Status = error.StatusCode;
	}
	public AppException(HttpStatusCode status, string message) : base(message)
	{
		this.Status = status;
		this.ErrorCode = status.ToString();
	}
	public AppException(HttpStatusCode status, string message, string errorCode) : base(message)
	{
		this.Status = status;
		this.ErrorCode = errorCode;
	}
}
