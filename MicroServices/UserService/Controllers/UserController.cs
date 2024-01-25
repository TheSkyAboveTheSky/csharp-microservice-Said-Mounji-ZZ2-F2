using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UserService.Data;
using UserService.Entities;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserController(DataContext context, PasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.User.ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(string id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserById(string id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllUsers()
        {
            var allUsers = await _context.User.ToListAsync();

            if (allUsers == null || allUsers.Count == 0)
            {
                return NotFound();
            }

            _context.User.RemoveRange(allUsers);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id,User user)
        {
            user.Id = id;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchUser(string id, UserModelUpdate user)
        {
            var userFromDb = await _context.User.FindAsync(id);
            if(user.Prenom != null)
            {
                userFromDb.Prenom = user.Prenom;
            }
            if (user.Nom != null)
            {
                userFromDb.Nom = user.Nom;
            }
            if (user.Email != null)
            {
                userFromDb.Email = user.Email;
            }
            if (user.Username != null)
            {
                userFromDb.Username = user.Username;
            }
            _context.Entry(userFromDb).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPost("register")]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            if (await IsEmailUnique(user.Email))
            {
                user.Pass = _passwordHasher.HashPassword(user, user.Pass);

                _context.User.Add(user);
                await _context.SaveChangesAsync();

                var token = GenerateJwtToken(user.Id);
                var userToken = new UserToken
                {
                    User = user,
                    Token = token
                };
                return Ok(userToken);
            }

            return Conflict("Email must be unique.");
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserToken>> Login(UserLogin userlogin)
        {
            var userFromDb = await _context.User.FirstOrDefaultAsync(u => u.Email == userlogin.Email);

            if (userFromDb == null)
            {
                return NotFound();
            }

            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(userFromDb, userFromDb.Pass, userlogin.Pass);

            if (passwordVerificationResult == PasswordVerificationResult.Success)
            {
                var token = GenerateJwtToken(userFromDb.Id);
                var userToken = new UserToken
                {
                    User = userFromDb,
                    Token = token
                };
                return Ok(userToken);
            }

            return NotFound();
        }

        private async Task<bool> IsEmailUnique(string email)
        {
            return !await _context.User.AnyAsync(u => u.Email == email);
        }

        private string GenerateJwtToken(string userId)
        {
            var claims = new List<Claim>
            {
                new Claim("UserId", userId)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecretKeyLongLongLongLongEnough"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "TodoProject",
                audience: "localhost:5000",
                claims: claims,
                expires: DateTime.Now.AddMinutes(3000),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
