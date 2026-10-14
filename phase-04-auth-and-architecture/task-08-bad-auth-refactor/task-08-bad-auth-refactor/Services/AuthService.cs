using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using task_08_bad_auth_refactor.Data;
using task_08_bad_auth_refactor.Dtos;
using task_08_bad_auth_refactor.Models;

namespace task_08_bad_auth_refactor.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AuthService> _logger;

        public AuthService(AppDbContext context, IConfiguration configuration, UserManager<ApplicationUser> user, ILogger<AuthService> logger
            )
        {
            _configuration = configuration;
            _userManager = user;
            _context = context;
            _logger = logger;
        }


        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                _logger.LogWarning("Login failed: account was not found.");

                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            if (!user.IsActive)
            {
                _logger.LogWarning("Login denied for inactive user {UserId}.", user.Id);

                throw new UnauthorizedAccessException("Your account is inactive.");
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!passwordValid)
            {
                _logger.LogWarning("Login failed: invalid credentials for user {UserId}.", user.Id);

                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            user.LastLoginAt = DateTime.UtcNow;
            var updateResult = await _userManager.UpdateAsync(user);


            if (!updateResult.Succeeded)
            {
                _logger.LogError(
                    "Could not update LastLoginAt for user {UserId}.",
                    user.Id);

                throw new InvalidOperationException(
                    "Unable to complete login. Please try again.");
            }

            var authResponse = await CreateAuthResponseAsync(user);

            _logger.LogInformation(
                "Login succeeded for user {UserId} with role {Role}.",
                user.Id,
                authResponse.Role);


            return authResponse;
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            if (!string.Equals(request.Role, "Student", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("Register is allowed for Student only.");
            }


            var emailExist = await _userManager.FindByEmailAsync(request.Email);

            if (emailExist != null)
            {
                throw new InvalidOperationException("Email is already exist.");
            }


            // Create Student
            var student = new Student
            {
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = null,
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false
            };


            _context.Students.Add(student);

            await _context.SaveChangesAsync();


            if (student.StudentId <= 0)
            {
                throw new InvalidOperationException("StudentId was not generated.");
            }

            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                FullName = request.FullName,
                Role = "Student",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                StudentId = student.StudentId
            };


        }
    }
}
