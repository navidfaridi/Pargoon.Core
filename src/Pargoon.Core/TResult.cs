using System.Net;

namespace Pargoon.Core
{
	public class TResult<T> : TResult
	{
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
		public T? Data { get; set; }
	}
}
