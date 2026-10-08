using task_02_requirements_to_erd.DTOs.Auth;

namespace task_02_requirements_to_erd.Interface
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
        Task<CurrentUserResponse> GetCurrentUserAsync(string UserId);
        Task ChangePassAsync(string email , string CurrentPassword , string newPassword);
    }
}
