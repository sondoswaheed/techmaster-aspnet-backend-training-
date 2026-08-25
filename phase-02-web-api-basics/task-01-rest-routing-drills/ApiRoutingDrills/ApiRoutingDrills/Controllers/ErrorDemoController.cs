using Microsoft.AspNetCore.Mvc;

namespace ApiRoutingDrills.Controllers
{
    [Route("api/errors")]
    [ApiController]
    public class ErrorDemoController : ControllerBase
    {
        [HttpGet("demo")]
        public IActionResult Demo([FromQuery] string type = "bad-request")
        {
            if (type == "bad-request")
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid request",
                    code = "VALIDATION_ERROR",
                    details = new[]
                    {
                        "Name is required"
                    }
                });
            }

            if (type == "not-found")
            {
                return NotFound(new
                {
                    success = false,
                    message = "Resource not found",
                    code = "NOT_FOUND",
                    details = new[]
                    {
                        "The requested resource does not exist"
                    }
                });
            }

            return Ok(new
            {
                success = true,
                message = "No error"
            });
        }
    }
}