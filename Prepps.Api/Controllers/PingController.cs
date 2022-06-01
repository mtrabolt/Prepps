using Microsoft.AspNetCore.Mvc;

namespace Prepps.Api.Controllers;

public class PingController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok($"Welcome to {typeof(PingController).Namespace}");
    }
}