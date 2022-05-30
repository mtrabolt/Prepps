using Microsoft.AspNetCore.Mvc;

namespace Prepps.Api.Controllers;

[Route("[controller]")]
public class PingController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok("Ping");
    }
}