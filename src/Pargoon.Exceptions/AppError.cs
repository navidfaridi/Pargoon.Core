using System.Net;

namespace Pargoon.Exceptions;
public class AppError
{
	public AppError()
	{

	}
	public HttpStatusCode StatusCode { get; set; }
	public string ErrorCode { get; set; } = null!;
	public string ErrorMessage { get; set; } = null!;
}
