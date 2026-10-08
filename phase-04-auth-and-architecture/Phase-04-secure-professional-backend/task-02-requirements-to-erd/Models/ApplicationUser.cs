using Microsoft.AspNetCore.Identity;

namespace task_02_requirements_to_erd.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }

        public string Role { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? LastLoginAt { get; set; }

        public int? StudentId { get; set; }

        public int? InstructorId { get; set; }

        public Student? Student { get; set; }

        public Instructor? Instructor { get; set; }
    }
}