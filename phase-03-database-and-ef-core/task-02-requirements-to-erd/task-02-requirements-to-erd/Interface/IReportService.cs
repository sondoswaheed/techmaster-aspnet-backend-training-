using task_02_requirements_to_erd.DTOs;

namespace task_02_requirements_to_erd.Interface
{
    public interface IReportService
    {
        DashboardSummaryDto GetDashboardSummary();

        List<UnpaidEnrollmentDto> GetUnpaidEnrollments();

        List<TrackCapacityDto> GetTrackCapacity();

        RevenueSummaryDto GetRevenueSummary();

        List<RevenueByTrackDto> GetRevenueByTrack();
    }
}