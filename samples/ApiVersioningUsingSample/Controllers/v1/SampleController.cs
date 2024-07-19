using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ApiVersioningUsingSample.Controllers.v1;

/// <summary>
/// this is a sample controller in version 1.0
/// </summary>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class SampleController : ControllerBase
{
    /// <summary>
    /// this is a get sample action in api version 1.0
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult TestGetAction(string id)
    {
        return Ok(new { id, version = "1.0" });
    }
}