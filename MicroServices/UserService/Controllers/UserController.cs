using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Entities;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return Ok(await _context.User.ToListAsync());
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

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            if (await IsEmailUnique(user.Email))
            {
                _context.User.Add(user);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
            }

            return Conflict("Email must be unique.");
        }

        private async Task<bool> IsEmailUnique(string email)
        {
            return !await _context.User.AnyAsync(u => u.Email == email);
        }
    }
}
