using Microsoft.AspNetCore.Mvc;
using task_02_requirements_to_erd.Interface;

namespace task_02_requirements_to_erd.Controllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("dashboard-summary")]
        public IActionResult GetDashboardSummary()
        {
            var result = _reportService.GetDashboardSummary();

            return Ok(result);
        }

        [HttpGet("unpaid-enrollments")]
        public IActionResult GetUnpaidEnrollments()
        {
            var result = _reportService.GetUnpaidEnrollments();

            return Ok(result);
        }

        [HttpGet("track-capacity")]
        public IActionResult GetTrackCapacity()
        {
            var result = _reportService.GetTrackCapacity();

            return Ok(result);
        }

        [HttpGet("revenue-summary")]
        public IActionResult GetRevenueSummary()
        {
            var result = _reportService.GetRevenueSummary();

            return Ok(result);
        }

        [HttpGet("revenue-by-track")]
        public IActionResult GetRevenueByTrack()
        {
            var result = _reportService.GetRevenueByTrack();

            return Ok(result);
        }

        [HttpGet("tracks-with-available-seats")]
        public IActionResult GetTracksWithAvailableSeats()
        {
            var result = _reportService.AvailableSeats();

            return Ok(result);
        }

        [HttpGet("top-tracks")]
        public IActionResult GetTopTracks()
        {
            var result = _reportService.GetTopTracks();
            return Ok(result);
        }

        [HttpGet("students-without-payments")]
        public IActionResult GetStudentsWithoutPayments()
        {
            var result = _reportService.GetStudentsWithoutPayments();
            return Ok(result);
        }
    }
}