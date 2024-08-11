using System.Net;
using Xunit;

namespace Pargoon.Core.Tests;

public class TResultTests
{
	[Fact]
	public void TResultShouldReturnOkStatusWhenCreatedBySuccess()
	{
		var x = TResult<int>.Success(int.MaxValue);

		var y = TResult<int>.Fail(400, "some description");

		Assert.Equal(HttpStatusCode.OK, x.StatusCode);
		Assert.Equal(200, x.Code);
		Assert.Equal("", x.Description);
	}


	[Fact]
	public void TResult_DefaultConstructor_ShouldInitializeWithDefaults()
	{
		var result = new TResult();

		Assert.Equal(0, result.Code);
		Assert.Equal((HttpStatusCode)0, result.StatusCode);
		Assert.False(result.IsSuccess);
		Assert.Null(result.Description);
	}

	[Fact]
	public void TResult_CodeConstructor_ShouldSetCorrectValues()
	{
		var result = new TResult(200);

		Assert.Equal(200, result.Code);
		Assert.Equal(HttpStatusCode.OK, result.StatusCode);
		Assert.True(result.IsSuccess);
	}

	[Fact]
	public void TResult_HttpStatusCodeConstructor_ShouldSetCorrectValues()
	{
		var result = new TResult(HttpStatusCode.BadRequest);

		Assert.Equal(400, result.Code);
		Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
		Assert.False(result.IsSuccess);
	}

	[Fact]
	public void TResult_CodeAndDescriptionConstructor_ShouldSetCorrectValues()
	{
		var result = new TResult(404, "Not Found");

		Assert.Equal(404, result.Code);
		Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
		Assert.Equal("Not Found", result.Description);
		Assert.False(result.IsSuccess);
	}

	[Fact]
	public void TResult_HttpStatusCodeAndDescriptionConstructor_ShouldSetCorrectValues()
	{
		var result = new TResult(HttpStatusCode.InternalServerError, "Internal Server Error");

		Assert.Equal(500, result.Code);
		Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
		Assert.Equal("Internal Server Error", result.Description);
		Assert.False(result.IsSuccess);
	}

	[Fact]
	public void TResult_T_Success_ShouldReturnSuccessfulResult()
	{
		var result = TResult<string>.Success("Success!");

		Assert.Equal(200, result.Code);
		Assert.Equal(HttpStatusCode.OK, result.StatusCode);
		Assert.True(result.IsSuccess);
		Assert.Equal("Success!", result.Data);
	}

	[Fact]
	public void TResult_T_Fail_ShouldReturnFailedResult()
	{
		var result = TResult<string>.Fail(400, "Bad Request");

		Assert.Equal(400, result.Code);
		Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
		Assert.False(result.IsSuccess);
		Assert.Equal("Bad Request", result.Description);
		Assert.Null(result.Data);
	}

	[Fact]
	public void BadRequestDataResult_ShouldSetCorrectValues()
	{
		var result = new BadRequestDataResult("Invalid input");

		Assert.Equal(400, result.Code);
		Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
		Assert.Equal("Invalid input", result.Description);
		Assert.False(result.IsSuccess);
	}

	[Fact]
	public void UnauthorizedDataResult_ShouldSetCorrectValues()
	{
		var result = new UnauthorizedDataResult("Unauthorized access");

		Assert.Equal(401, result.Code);
		Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
		Assert.Equal("Unauthorized access", result.Description);
		Assert.False(result.IsSuccess);
	}

	[Fact]
	public void ForbiddenDataResult_ShouldSetCorrectValues()
	{
		var result = new ForbiddenDataResult("Access forbidden");

		Assert.Equal(403, result.Code);
		Assert.Equal(HttpStatusCode.Forbidden, result.StatusCode);
		Assert.Equal("Access forbidden", result.Description);
		Assert.False(result.IsSuccess);
	}

	[Fact]
	public void NotFoundDataResult_ShouldSetCorrectValues()
	{
		var result = new NotFoundDataResult("Resource not found");

		Assert.Equal(404, result.Code);
		Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
		Assert.Equal("Resource not found", result.Description);
		Assert.False(result.IsSuccess);
	}

	[Fact]
	public void InternalServerErrorResult_ShouldSetCorrectValues()
	{
		var result = new InternalServerErrorResult("Server error");

		Assert.Equal(500, result.Code);
		Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
		Assert.Equal("Server error", result.Description);
		Assert.False(result.IsSuccess);
	}
}
