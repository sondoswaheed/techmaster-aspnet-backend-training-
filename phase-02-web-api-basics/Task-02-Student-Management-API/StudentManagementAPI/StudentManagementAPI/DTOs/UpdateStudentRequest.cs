using System.ComponentModel.DataAnnotations;

namespace StudentManagementAPI.DTOs
{
    public class UpdateStudentRequest
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string TrackName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string? GitHubProfileUrl { get; set; }

        public string? LinkedInProfileUrl { get; set; }
    }
}