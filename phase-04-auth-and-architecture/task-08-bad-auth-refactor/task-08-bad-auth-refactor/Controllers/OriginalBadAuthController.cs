using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using task_08_bad_auth_refactor.Services;

namespace task_08_bad_auth_refactor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OriginalBadAuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public OriginalBadAuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var user = _authService.Users.FirstOrDefault(x => x.Email == request.Email);
            if (user == null) return Ok("wrong email");
            if (user.PasswordHash != request.Password) return Ok("wrong password");
            var token = "fake-token-" + user.Id;
            return Ok(new { token = token, user = user });
        }
        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            var user = new ApplicationUser();
            user.FullName = request.FullName;
            user.Email = request.Email;
            user.PasswordHash = request.Password;
            user.Role = request.Role;
            db.Users.Add(user);
            db.SaveChanges();
            return Ok(user);
        }
    }
}
