using task_01_ef_core_modeling_drills.Models;

namespace task_01_ef_core_modeling_drills.DTOs
{
    public class TrainingTrackDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int InstructorId { get; set; }
        public ICollection<EnrollmentDto> Enrollments { get; set; }
    }
}