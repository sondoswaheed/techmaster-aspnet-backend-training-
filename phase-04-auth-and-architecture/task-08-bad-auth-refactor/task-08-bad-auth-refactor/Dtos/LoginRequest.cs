using System.ComponentModel.DataAnnotations;

namespace task_08_bad_auth_refactor.Dtos
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
