namespace task_02_requirements_to_erd.DTOs.Auth
{
    public class AuthResponse
    {
        public string AccessToken { get; set; }

        public DateTime ExpiresAt { get; set; }

        public string UserId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }
    }
}