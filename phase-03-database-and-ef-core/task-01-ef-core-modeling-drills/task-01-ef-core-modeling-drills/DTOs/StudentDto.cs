using task_01_ef_core_modeling_drills.Models;

namespace task_01_ef_core_modeling_drills.DTOs
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public ICollection<EnrollmentDto> Enrollments { get; set; } 
    }
}
