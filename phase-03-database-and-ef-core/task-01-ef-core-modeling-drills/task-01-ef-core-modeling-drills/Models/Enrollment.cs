using task_01_ef_core_modeling_drills.Models.Enums;

namespace task_01_ef_core_modeling_drills.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public EnrollmentStatus Status { get; set; }

        public decimal? FinalGrade { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int TrainingTrackId { get; set; }
        public TrainingTrack TrainingTrack { get; set; }
        public PaymentSummary PaymentSummary { get; set; }
    }
}