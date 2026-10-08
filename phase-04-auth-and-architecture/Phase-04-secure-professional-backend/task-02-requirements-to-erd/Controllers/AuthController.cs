using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using task_02_requirements_to_erd.DTOs.Auth;
using task_02_requirements_to_erd.Interface;

namespace task_02_requirements_to_erd.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register( RegisterRequest request)
        {
            try
            {
                var result = await _authService.RegisterAsync(request);

                return StatusCode(StatusCodes.Status201Created, result);
            }catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login( LoginRequest request)
        {
            try
            {
                var result = await _authService.LoginAsync(request);

                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> Me()
        {
            var userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var result =
                await _authService.GetCurrentUserAsync(userId);

            return Ok(result);
        }
        [HttpPost("change-Password")]
        public async Task<IActionResult> ChangePass(ChangePasswordRequest request)
        {
            try
            {
                await _authService.ChangePassAsync(request.Email, request.CurrentPassword, request.NewPassword);

                return Ok(new {message = "Password changed successfully"});
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}