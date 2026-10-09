using System.ComponentModel.DataAnnotations;

namespace task_02_requirements_to_erd.DTOs
{
    public class EnrollmentRequestDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int TrainingTrackId { get; set; }
    }
}