using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiRoutingDrills.Controllers
{
    [Route("api/tools")]
    [ApiController]
    public class RouteParameterController : ControllerBase
    {
        [HttpGet("echo/{name}")]
        public IActionResult GetName(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                return BadRequest();
            }
            return Ok(new
            {
                message=$"Hello {name}",
                name = name
            });
        }
    }
}
