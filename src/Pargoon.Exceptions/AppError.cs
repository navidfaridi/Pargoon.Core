using Pargoon.Core;
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


public static class TResultExtensions
{
    public static TResult<T> Fail<T>(AppError error)
    {
        return new TResult<T>(error.StatusCode, $"({error.ErrorCode}): {error.ErrorMessage}");
    }
}