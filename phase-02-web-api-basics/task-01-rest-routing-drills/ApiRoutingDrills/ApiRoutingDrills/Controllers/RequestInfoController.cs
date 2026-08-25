using Microsoft.AspNetCore.Mvc;

namespace ApiRoutingDrills.Controllers
{
    [Route("api/request-info")]
    [ApiController]
    public class RequestInfoController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetRequestInfo()
        {
            var studentname = Request.Headers["X-Student-Name"].FirstOrDefault();

            if (string.IsNullOrWhiteSpace(studentname))
            {
                return BadRequest(new
                {
                    message = "X-Student-Name header is required"
                });
            }

            return Ok(new
            {
                studentName = studentname,
                requestPath = Request.Path.ToString()
            });
        }
    }
}