using task_02_requirements_to_erd.Models;
using task_02_requirements_to_erd.Models.Enums;

namespace task_02_requirements_to_erd.DTOs
{
    public class EnrollmentResponseDto
    {
        public int EnrollmentId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public EnrollmentStatus Status { get; set; }
        public decimal? FinalResult { get; set; }
        public decimal ProgressPercentage { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int StudentId { get; set; }
        //public Student Student { get; set; }
        public int TrainingTrackId { get; set; }
        //public TrainingTrack TrainingTrack { get; set; }
        public ICollection<PaymentResponse> Payments { get; set; } = new List<PaymentResponse>();
    }
}
