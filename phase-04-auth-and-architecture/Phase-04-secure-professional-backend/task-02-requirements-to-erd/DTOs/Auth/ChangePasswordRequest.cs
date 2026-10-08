using System.ComponentModel.DataAnnotations;

namespace task_02_requirements_to_erd.DTOs.Auth
{
    public class ChangePasswordRequest
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        [MinLength(6)]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
    }
}