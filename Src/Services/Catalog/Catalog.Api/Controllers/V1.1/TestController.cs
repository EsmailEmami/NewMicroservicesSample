using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers.V1._1;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.1")]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok();
    }
}
