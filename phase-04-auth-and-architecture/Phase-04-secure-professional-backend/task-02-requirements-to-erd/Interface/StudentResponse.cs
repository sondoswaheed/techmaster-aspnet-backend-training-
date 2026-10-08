using System.ComponentModel.DataAnnotations;
using task_02_requirements_to_erd.DTOs;

namespace task_02_requirements_to_erd.Interface
{
    public class StudentResponse
    {
        public int StudentId { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int TotalEnrollment { get; set; }
    }
}
