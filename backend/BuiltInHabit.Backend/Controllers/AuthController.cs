using BCrypt.Net;
using BuiltInHabit.Backend.DTOs;
using BuiltInHabit.Backend.Models;
using BuiltInHabit.Backend.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Security.Claims;
using System.Text;

namespace BuiltInHabit.Backend.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly MongoDbContext _context;

        public AuthController(MongoDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            var existingUser = await _context.Users
                .Find(user => user.Email == request.Email)
                .FirstOrDefaultAsync();
            if(existingUser != null)
            {
                return BadRequest("User email already exists");
            }
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                UserName = request.Username,
                Email = request.Email,
                PasswordHash = passwordHash
            };
            //Save Data
            await _context.Users.InsertOneAsync(user);
            return Ok(new { message = "User registerd successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
        {
            var user = await _context.Users
                                     .Find(u => u.Email == request.Email)
                                     .FirstOrDefaultAsync();

            if (user == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                return Unauthorized("Invalid credentials.");
            }
            return Ok(new { message = "Login successful" });
        }
    }
}
