using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ApiVersioningUsingSample.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class SampleController : ControllerBase
{
    [HttpGet]
    public IActionResult TestGetAction(string id)
    {
        return Ok(new { id, version = "1.0" });
    }
}