using Microsoft.AspNetCore.Mvc;

namespace ApiRoutingDrills.Controllers
{
    [Route("api/status")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        // 200 OK
        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new
            {
                message = "API is healthy"
            });
        }

        // 201 Created
        [HttpPost("create")]
        public IActionResult Create()
        {
            var resource = new
            {
                id = 1,
                message = "Resource created successfully"
            };

            return StatusCode(201, resource);
        }

        // 204 No Content
        [HttpDelete("delete")]
        public IActionResult Delete()
        {
            return NoContent();
        }

        // 400 Bad Request
        [HttpGet("validate")]
        public IActionResult Validate([FromQuery] int value)
        {
            if (value < 0)
            {
                return BadRequest(new
                {
                    message = "Value cannot be negative"
                });
            }

            return Ok(new
            {
                value = value,
                message = "Value is valid"
            });
        }

        // 404 Not Found
        [HttpGet("missing")]
        public IActionResult Missing()
        {
            return NotFound(new
            {
                message = "The requested resource was not found"
            });
        }
    }
}