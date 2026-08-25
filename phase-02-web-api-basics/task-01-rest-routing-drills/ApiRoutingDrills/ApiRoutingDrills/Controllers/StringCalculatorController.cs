using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiRoutingDrills.Controllers
{
    [Route("api/Calculator")]
    [ApiController]
    public class StringCalculatorController : ControllerBase
    {
        [HttpGet("add")]
        public IActionResult Add([FromQuery] decimal a, [FromQuery] decimal b)
        {
            decimal result = a + b;
            return Ok(new
            {
                a = a,
                b = b,
                operation = "addition",
                result = result
            });
        }
    }
}
