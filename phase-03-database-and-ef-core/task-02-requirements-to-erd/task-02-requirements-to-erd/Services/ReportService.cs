using Microsoft.EntityFrameworkCore;
using task_02_requirements_to_erd.Data;
using task_02_requirements_to_erd.DTOs;
using task_02_requirements_to_erd.Interface;
using task_02_requirements_to_erd.Models.Enums;

namespace task_02_requirements_to_erd.Services
{
    public class ReportService : IReportService
    {
        private readonly AppDbContext _context;

        public ReportService(AppDbContext context)
        {
            _context = context;
        }

        public DashboardSummaryDto GetDashboardSummary()
        {
            return new DashboardSummaryDto
            {
                TotalStudents = _context.Students.Count(),
                TotalTracks = _context.TrainingTracks.Count(),
                TotalEnrollments = _context.Enrollments.Count(),
                TotalPayments = _context.Payments.Count()
            };
        }

        public List<UnpaidEnrollmentDto> GetUnpaidEnrollments()
        {
            var enrollments = _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.TrainingTrack)
                .Where(e => !e.Payments.Any(p =>
                    p.PaymentStatus == PaymentStatus.Paid))
                .ToList();

            return enrollments.Select(e => new UnpaidEnrollmentDto
            {
                EnrollmentId = e.EnrollmentId,
                StudentId = e.StudentId,
                StudentName = e.Student.FullName,
                TrainingTrackId = e.TrainingTrackId,
                TrackTitle = e.TrainingTrack.Title
            }).ToList();
        }

        public List<TrackCapacityDto> GetTrackCapacity()
        {
            var tracks = _context.TrainingTracks.Include(t => t.Enrollments).ToList();

            return tracks.Select(t => new TrackCapacityDto
            {
                TrainingTrackId = t.TrainingTrackId,
                TrackTitle = t.Title,
                Capacity = t.Capacity,
                EnrolledCount = t.Enrollments.Count,
                RemainingCapacity = t.Capacity - t.Enrollments.Count
            }).ToList();
        }

        public List<AvailableSeatsDto> AvailableSeats()
        {
            return _context.TrainingTracks
                .Select(t => new AvailableSeatsDto
                {
                    TrainingTrackId = t.TrainingTrackId,
                    TrackTitle = t.Title,
                    Capacity = t.Capacity,
                    ActiveEnrollments = t.Enrollments
                        .Count(e => e.Status == EnrollmentStatus.Active),
                    RemainingSeats = t.Capacity - t.Enrollments.Count(e => e.Status == EnrollmentStatus.Active)
                })
                .Where(t => t.RemainingSeats > 0)
                .ToList();
        }

        public RevenueSummaryDto GetRevenueSummary()
        {
            var paidPayments = _context.Payments.Where(p => p.PaymentStatus == PaymentStatus.Paid);

            return new RevenueSummaryDto
            {
                TotalRevenue = paidPayments.Sum(p => (decimal?)p.Amount) ?? 0,
                TotalPaidPayments = paidPayments.Count()
            };
        }

        public List<RevenueByTrackDto> GetRevenueByTrack()
        {
            var revenue = _context.Payments
                .Where(p => p.PaymentStatus == PaymentStatus.Paid)
                .Include(p => p.Enrollment)
                .ThenInclude(e => e.TrainingTrack)
                .ToList()
                .GroupBy(p => new
                {
                    p.Enrollment.TrainingTrackId,
                    p.Enrollment.TrainingTrack.Title
                })
                .Select(g => new RevenueByTrackDto
                {
                    TrainingTrackId = g.Key.TrainingTrackId,
                    TrackTitle = g.Key.Title,
                    TotalRevenue = g.Sum(p => p.Amount)
                })
                .ToList();

            return revenue;
        }

        public List<TopTrackDto> GetTopTracks()
        {
            return _context.TrainingTracks.Select(s => new TopTrackDto
            {
                TrackTitle = s.Title,
                TrainingTrackId = s.TrainingTrackId,
                ActiveEnrollments = s.Enrollments.Count(d => d.Status == EnrollmentStatus.Active)
            }).OrderByDescending(d => d.ActiveEnrollments)
            .Take(5).ToList();
        }

        public List<InstructorWorkloadDto> GetInstructorWorkload()
        {
            return _context.Instructors
                .Select(i => new InstructorWorkloadDto
                {
                    InstructorId = i.InstructorId,
                    InstructorName = i.FullName,
                    TrackCount = i.TrainingTracks.Count(),
                    ActiveStudents = i.TrainingTracks
                        .SelectMany(t => t.Enrollments)
                        .Count(e => e.Status == EnrollmentStatus.Active)
                })
                .ToList();
        }

        public List<StudentWithoutPaymentDto> GetStudentsWithoutPayments()
        {
            return _context.Enrollments.Where(e =>
                    e.Status == EnrollmentStatus.Active &&
                    !e.Payments.Any())
                .Select(e => new StudentWithoutPaymentDto
                {
                    StudentId = e.StudentId,
                    StudentName = e.Student.FullName,
                    Email = e.Student.Email,
                    EnrollmentId = e.EnrollmentId,
                    TrainingTrackId = e.TrainingTrackId
                })
                .ToList();
        }
    }
}