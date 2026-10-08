using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using task_02_requirements_to_erd.Data;
using task_02_requirements_to_erd.DTOs.Auth;
using task_02_requirements_to_erd.Interface;
using task_02_requirements_to_erd.Models;

namespace task_02_requirements_to_erd.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthService(AppDbContext context , IConfiguration configuration , UserManager<ApplicationUser> user)
        {
            _configuration = configuration;
            _userManager= user;
            _context = context;
        }

        public async Task<CurrentUserResponse> GetCurrentUserAsync(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);

            if (user == null)
            {
                throw new UnauthorizedAccessException("User not found.");
            }

            var roles = await _userManager.GetRolesAsync(user);

            return new CurrentUserResponse
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = roles.FirstOrDefault() ?? user.Role,
                LinkedStudentId = user.StudentId,
                LinkedInstructorId = user.InstructorId
            };
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request )
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            if (!user.IsActive)
            {
                throw new UnauthorizedAccessException(
                    "Your account is inactive.");
            }

            var passwordValid = await _userManager.CheckPasswordAsync( user, request.Password);

            if (!passwordValid)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            user.LastLoginAt = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            return await CreateAuthResponseAsync(user);
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            if (!string.Equals( request.Role,"Student", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    "Register is allowed for Student only.");
            }


            var emailExist =  await _userManager.FindByEmailAsync(request.Email);

            if (emailExist != null)
            {
                throw new InvalidOperationException(
                    "Email is already exist.");
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
                throw new InvalidOperationException(
                    "StudentId was not generated.");
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


            var result = await _userManager.CreateAsync( user, request.Password);


            if (!result.Succeeded)
            {
                _context.Students.Remove(student);

                await _context.SaveChangesAsync();


                var errors = string.Join( ", ", result.Errors.Select(e => e.Description));

                throw new InvalidOperationException(errors);
            }


            var roleResult = await _userManager.AddToRoleAsync(user, "Student");


            if (!roleResult.Succeeded)
            {
                var errors = string.Join(
                    ", ",
                    roleResult.Errors.Select(e => e.Description));

                throw new InvalidOperationException(errors);
            }


            // Generate JWT
            return new RegisterResponse
            {
                UserId= user.Id,
                Email= user.Email,
                FullName= user.FullName,
                Role=user.Role
            };
        }

        private async Task<AuthResponse> CreateAuthResponseAsync( ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var role = roles.FirstOrDefault() ?? user.Role;

            var expiresAt = DateTime.UtcNow.AddHours(2);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),

                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),

                new Claim( ClaimTypes.Role, role),

                new Claim( ClaimTypes.Name, user.FullName)
            };

            var secretKey = _configuration["Jwt:Key"];

            if (string.IsNullOrWhiteSpace(secretKey))
            {
                throw new InvalidOperationException( "JWT secret key is not configured.");
            }

            var key = new SymmetricSecurityKey( Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials( key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiresAt,
                signingCredentials: credentials);

            var accessToken = new JwtSecurityTokenHandler()
                .WriteToken(token);

            return new AuthResponse
            {
                AccessToken = accessToken,
                ExpiresAt = expiresAt,
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = role
            };
        }

        public async Task ChangePassAsync(string email, string CurrentPassword, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                throw new InvalidOperationException("Email not found");

            if (!user.IsActive)
                throw new InvalidOperationException("your account is inactive");

            var currentPAssValid = await _userManager.CheckPasswordAsync(user,CurrentPassword);

            if (!currentPAssValid)
                throw new InvalidOperationException("current password is incorrect");

            var result = await _userManager.ChangePasswordAsync(user, CurrentPassword, newPassword );

            if (!result.Succeeded)
            {
                var errors = string.Join(",", result.Errors.Select(s => s.Description));

                throw new InvalidOperationException(errors);
            }

            user.UpdatedAt= DateTime.Now;

            await _userManager.UpdateAsync(user);
        }

    }
}

