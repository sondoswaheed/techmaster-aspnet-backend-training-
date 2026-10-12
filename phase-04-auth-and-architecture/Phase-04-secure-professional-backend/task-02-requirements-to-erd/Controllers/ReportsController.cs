using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using task_02_requirements_to_erd.Common.Responses;
using task_02_requirements_to_erd.Interface;

namespace task_02_requirements_to_erd.Controllers
{
    [Route("api/reports")]
    [ApiController]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("dashboard-summary")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetDashboardSummary()
        {
            var result = _reportService.GetDashboardSummary();

            return Ok(ApiResponse<object>.SuccessResponse(result, "dashboard retrieved successfully"));
        }

        [HttpGet("unpaid-enrollments")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetUnpaidEnrollments()
        {
            var result = _reportService.GetUnpaidEnrollments();

            return Ok(ApiResponse<object>.SuccessResponse(result, "unpaid enrollments retrieved successfully"));
        }

        [HttpGet("track-capacity")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetTrackCapacity()
        {
            var result = _reportService.GetTrackCapacity();

            return Ok(ApiResponse<object>.SuccessResponse(result, "track capacity retrieved successfully"));
        }

        [HttpGet("revenue-summary")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetRevenueSummary()
        {
            var result = _reportService.GetRevenueSummary();

            return Ok(ApiResponse<object>.SuccessResponse(result, "revenue summary retrieved successfully"));
        }

        [HttpGet("revenue-by-track")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetRevenueByTrack()
        {
            var result = _reportService.GetRevenueByTrack();

            return Ok(ApiResponse<object>.SuccessResponse(result));
        }

        [HttpGet("tracks-with-available-seats")]
        [Authorize(Roles = "Admin,Student")]
        public IActionResult GetTracksWithAvailableSeats()
        {
            var result = _reportService.AvailableSeats();

            return Ok(ApiResponse<object>.SuccessResponse(result));
        }

        [HttpGet("top-tracks")]
        [Authorize(Roles = "Admin")]

        public IActionResult GetTopTracks()
        {
            var result = _reportService.GetTopTracks();

            return Ok(ApiResponse<object>.SuccessResponse(result, "top tracks retrieved successfully"));
        }

        [HttpGet("students-without-payments")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetStudentsWithoutPayments()
        {
            var result = _reportService.GetStudentsWithoutPayments();
                
            return Ok(ApiResponse<object>.SuccessResponse(result));
        }
    }
}