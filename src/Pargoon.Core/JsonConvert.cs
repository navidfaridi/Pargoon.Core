using System.Text.Encodings.Web;
using System.Text.Json;

namespace Pargoon.Core;

public class JsonConvert
{
	public static JsonSerializerOptions UnsafeCamelCase
	{
		get
		{
			return new JsonSerializerOptions
			{
				Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
				PropertyNameCaseInsensitive = true
			};
		}
	}
	public static string Serialize(object obj, JsonSerializerOptions? options = null)
	{
		return JsonSerializer.Serialize(obj, options ?? JsonConvert.UnsafeCamelCase);
	}

	public static T? Deserialize<T>(string txt, JsonSerializerOptions? options = null)
	{
		return JsonSerializer.Deserialize<T>(txt, options ?? JsonConvert.UnsafeCamelCase);
	}
}
