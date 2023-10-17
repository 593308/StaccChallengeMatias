using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Protocols;
using StaccChallenge.Dtos;
using StaccChallenge.Models;

namespace StaccChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Registers a new user in the system
        /// </summary>
        /// <param name="userForRegisterDto">Data Transfer object</param>
        /// <returns>Status code indi</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            // Check if the user already exists based on email
            var userExists = await _context.Users.AnyAsync(u => u.Email == userForRegisterDto.Email);
            if (userExists)
            {
                return BadRequest("Email is already used");
            }

            // Checks if username is taken
            var usernameTaken = await _context.Users.AnyAsync(u => u.Username == userForRegisterDto.Username);
            if (usernameTaken)
            {
                return BadRequest("Username is already taken");
            }

            // Creating a tuple of password hash and password salt
            (byte[] passwordHash, byte[] passwordSalt) = CreatePasswordHash(userForRegisterDto.Password);
            
            // Creates a new user object
            var user = new User
            {
                Username = userForRegisterDto.Username,
                Email = userForRegisterDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            // Adds the user to the database
            _context.Users.Add(user);
            await _context.SaveChangesAsync();


            return StatusCode(201);
        }

        /// <summary>
        /// Creates a password hash
        /// </summary>
        /// <param name="password">The plaintext password</param>
        /// <returns>Returns a tuple consisting of passwordHash and passwordSalt</returns>
        private (byte[] passwordHash, byte[] passwordSalt) CreatePasswordHash(string password)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256())
            {
                var passwordSalt = hmac.Key;
                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return (passwordHash, passwordSalt);
            }
        }

        /// <summary>
        /// Logs a user in, authenticates the user based on email and password
        /// </summary>
        /// <param name="userForLoginDto">Data transfer object containing user login details.</param>
        /// <returns>Returns success or failur in the form of an action result</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            // Retrieve the user based on email
            // TODO: Create login with username as well
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userForLoginDto.Email);

            // Checks if user is found or if it is wrong password
            if (user == null || !VerifyPasswordHash(userForLoginDto.Password, user.PasswordHash, user.PasswordSalt)){
                return Unauthorized();
            }
            return Ok();
        }

        /// <summary>
        /// Verifies the password
        /// </summary>
        /// <param name="password">Plain text password</param>
        /// <param name="storedHash">Stored hash of the password</param>
        /// <param name="storedSalt">Stored salt of the password</param>
        /// <returns>Returns true if password matches, otherwise false</returns>
        private bool VerifyPasswordHash(string password, byte[]? storedHash, byte[]? storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256(storedSalt)) 
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        
    }
}
