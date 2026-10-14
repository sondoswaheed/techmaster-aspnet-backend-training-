using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using task_02_requirements_to_erd.Common.Responses;
using task_02_requirements_to_erd.DTOs;
using task_02_requirements_to_erd.Interface;
using task_02_requirements_to_erd.Models.Enums;

namespace task_02_requirements_to_erd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Get(DateOnly? FromDate, DateOnly? ToDate, PaymentStatus? status)
        {
            var pay = _paymentService.GetAll(FromDate, ToDate,status);
            return Ok(ApiResponse<List<PaymentResponse>>.SuccessResponse(pay));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm]CreatePaymentDto dto)
        {
            try
            {
                var pay =await _paymentService.Create(dto);

                return StatusCode(201,ApiResponse<PaymentResponse>.SuccessResponse(pay, "Payments Created successfully"));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ApiResponse<PaymentResponse>.ErrorResponse(ex.Message));
            }
        }

        [HttpPut("{id}/Status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id,UpdatePaymentDto dto)
        {
            var pay= await _paymentService.Update(id, dto);

            if (pay == null)
                return NotFound(ApiResponse<PaymentResponse>.ErrorResponse( "Payment not found"));

            return Ok(ApiResponse<PaymentResponse>.SuccessResponse(pay,"Payments updated successfully"));
        }

        [HttpGet("/api/enrollments/{id}/payments")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetByEnrollmentId(int id)
        {
            try
            {
                var payments = _paymentService.GetByEnrollmentId(id);

                return Ok(ApiResponse<List<PaymentResponse>>.SuccessResponse(payments,"payments returns successfully"));
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ApiResponse<List<PaymentResponse>>.ErrorResponse(ex.Message));
            }
        }
    }
}
