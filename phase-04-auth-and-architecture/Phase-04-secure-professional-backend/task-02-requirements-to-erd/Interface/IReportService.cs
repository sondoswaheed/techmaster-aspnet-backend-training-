using Microsoft.EntityFrameworkCore;
using task_02_requirements_to_erd.DTOs;
using task_02_requirements_to_erd.Models.Enums;

namespace task_02_requirements_to_erd.Interface
{
    public interface IReportService
    {
        DashboardSummaryDto GetDashboardSummary();

        List<UnpaidEnrollmentDto> GetUnpaidEnrollments();

        List<TrackCapacityDto> GetTrackCapacity();

        RevenueSummaryDto GetRevenueSummary();

        List<RevenueByTrackDto> GetRevenueByTrack();

        List<AvailableSeatsDto> AvailableSeats();

        List<TopTrackDto> GetTopTracks();

        public List<InstructorWorkloadDto> GetInstructorWorkload();

        List<StudentWithoutPaymentDto> GetStudentsWithoutPayments();

    }
}