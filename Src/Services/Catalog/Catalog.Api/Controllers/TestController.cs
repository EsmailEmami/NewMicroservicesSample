using Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = "PublicSecure")]
public class TestController : ControllerBase
{
    private readonly IUser _user;

    public TestController(IUser user)
    {
        _user = user;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var b = _user.UserId;
        return Ok();
    }
}
