using System.ComponentModel.DataAnnotations;

namespace task_02_requirements_to_erd.DTOs
{
    public class UpdateStudentDto
    {
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
