using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Service;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using MongoDB.Bson.Serialization.Attributes;
using Server.DTOs.Account;
using Server.DTOs.Stock.Account;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IConfiguration _config;

        public AuthController(UserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)

        {
            Console.WriteLine($"Login attempt: {registerDto.Email}");


            var existingUser = await _userService.GetByEmailAsync(registerDto.Email);
            if (existingUser != null)
                return BadRequest("Email already exists.");

            var user = new AppUser
            {
                UserName = registerDto.Username,
                LastName = registerDto.Lastname,
                Email = registerDto.Email.ToLower(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
            };

            await _userService.CreateUserAsync(user);
            return Ok(new NewUserDto
            {
                UserName = user.UserName,
                Lastname = user.LastName,
                Email = user.Email,
                Role = user.Role,
                Token = GenerateJwtToken(user)
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userService.GetByEmailAsync(loginDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                return Unauthorized("Invalid credentials.");

            var token = GenerateJwtToken(user);
            return Ok(new NewUserDto
            {
                UserName = user.UserName,
                Lastname = user.LastName,
                Email = user.Email,
                Role = user.Role,
                Token = token
            });
        }

        private string GenerateJwtToken(AppUser user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
