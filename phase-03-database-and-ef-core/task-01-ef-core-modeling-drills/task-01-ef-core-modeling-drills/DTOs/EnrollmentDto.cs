using task_01_ef_core_modeling_drills.Models.Enums;

namespace task_01_ef_core_modeling_drills.DTOs
{
    public class EnrollmentDto
    {
        public int Id { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public EnrollmentStatus Status { get; set; }

        public decimal? FinalGrade { get; set; }

        public int TrainingTrackId { get; set; }

        public string TrainingTrackTitle { get; set; }

        public int StudentId {  get; set; }
        public string FullName { get; set; }
        public PaymentSummaryDto PaymentSummary { get; set; }
    }
}