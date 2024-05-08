using System.Text.Json;
using Xunit;

namespace Pargoon.Core.Tests;

public class TResultTests
{
	[Fact]
	public void TResultShouldSerialized()
	{
		var result = new TResult(200,"ok");
		var txt = JsonSerializer.Serialize(result);
		var x = txt;
	}
}
