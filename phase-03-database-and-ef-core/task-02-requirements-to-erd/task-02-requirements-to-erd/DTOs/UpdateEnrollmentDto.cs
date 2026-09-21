using task_02_requirements_to_erd.Models;
using task_02_requirements_to_erd.Models.Enums;

namespace task_02_requirements_to_erd.DTOs
{
    public class UpdateEnrollmentDto
    {
        public DateTime EnrollmentDate { get; set; }
        public EnrollmentStatus Status { get; set; }
        public decimal? FinalResult { get; set; }
        public decimal ProgressPercentage { get; set; }
        public int StudentId { get; set; }
        //public Student Student { get; set; }
        public int TrainingTrackId { get; set; }
        //public TrainingTrack TrainingTrack { get; set; }
        //public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
