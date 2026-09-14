using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using task_01_ef_core_modeling_drills.Interface;

namespace task_01_ef_core_modeling_drills.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;
        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpGet("{id}/payment-summary")]
        public IActionResult GetPaymentSummary(int id)
        {
            var result = _enrollmentService.GetEnrollmentWithPaymentSummary(id);

            if (result == null)
                return NotFound("Enrollment not found");

            return Ok(result);
        }
    }
}
