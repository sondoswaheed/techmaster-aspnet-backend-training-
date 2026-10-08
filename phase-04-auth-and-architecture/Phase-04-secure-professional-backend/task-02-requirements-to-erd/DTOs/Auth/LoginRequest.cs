using System.ComponentModel.DataAnnotations;

namespace task_02_requirements_to_erd.DTOs.Auth
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email {get;set;}
        [Required]
        public string Password {get;set;}
    }
}
