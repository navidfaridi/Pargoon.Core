using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ApiVersioningUsingSample.Controllers.v2;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("2.0")]
[ApiController]
public class SampleController : ControllerBase
{
    [HttpGet]
    public IActionResult TestGetAction(string id)
    {
        return Ok(new { id, version = "2.0" });
    }
}
