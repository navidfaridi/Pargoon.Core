using System.Net;

namespace Pargoon.Core
{
	public class TResult<T> : Result where T : class
	{
		public TResult() { }
		public TResult(int code) : base(code) { }
		public TResult(HttpStatusCode code) : base(code) { }
		public TResult(int code, string? description) : base(code, description) { }
		public TResult(HttpStatusCode code, string? description) : base(code, description) { }
		public TResult(int code, string? description, T? data) : base(code, description)
		{
			Data = data;
		}
		public TResult(HttpStatusCode code, string? description, T? data) : base(code, description)
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
