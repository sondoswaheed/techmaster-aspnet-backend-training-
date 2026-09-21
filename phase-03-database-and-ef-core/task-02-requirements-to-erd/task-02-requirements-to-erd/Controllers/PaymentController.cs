using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using task_02_requirements_to_erd.DTOs;
using task_02_requirements_to_erd.Interface;
using task_02_requirements_to_erd.Models.Enums;

namespace task_02_requirements_to_erd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public IActionResult Get(DateOnly? FromDate, DateOnly? ToDate, PaymentStatus? status)
        {
            var pay = _paymentService.GetAll(FromDate, ToDate,status);
            return Ok(pay);
        }

        [HttpPost]
        public IActionResult Create([FromForm]CreatePaymentDto dto)
        {
            try
            {
                var pay = _paymentService.Create(dto);

                return StatusCode(201, pay);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/Status")]
        public IActionResult Update(int id,UpdatePaymentDto dto)
        {
            var pay= _paymentService.Update(id, dto);

            if (pay == null)
                return NotFound("Payment not found");

            return Ok(pay);
        }

        [HttpGet("/api/enrollments/{id}/payments")]
        public IActionResult GetByEnrollmentId(int id)
        {
            try
            {
                var payments = _paymentService.GetByEnrollmentId(id);

                return Ok(payments);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
