using Microsoft.AspNetCore.Mvc;

namespace ApiRoutingDrills.Controllers
{
    [Route("api/grades")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        [HttpGet("calculate")]
        public IActionResult Calculate([FromQuery] decimal score)
        {
            if (score < 0 || score > 100)
            {
                return BadRequest(new
                {
                    error = "Score must be between 0 and 100"
                });
            }

            string grade;
            bool passed;

            if (score >= 90)
            {
                grade = "A";
                passed = true;
            }
            else if (score >= 80)
            {
                grade = "B";
                passed = true;
            }
            else if (score >= 70)
            {
                grade = "C";
                passed = true;
            }
            else if (score >= 60)
            {
                grade = "D";
                passed = true;
            }
            else
            {
                grade = "F";
                passed = false;
            }

            return Ok(new
            {
                score = score,
                grade = grade,
                passed = passed
            });
        }
    }
}