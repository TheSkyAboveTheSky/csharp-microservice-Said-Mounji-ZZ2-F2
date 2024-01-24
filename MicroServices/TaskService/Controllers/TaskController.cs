using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskService.Data;
using TaskService.Entities;


namespace TaskService.controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TaskController : ControllerBase
  {
    private readonly DataContext _context;
    public TaskController(DataContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<System.Threading.Tasks.Task>>> GetTasks()
    {
      var tasks = await _context.Task.ToListAsync();
      return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<System.Threading.Tasks.Task>> GetTaskById(string id)
    {
      var task = await _context.Task.FindAsync(id);

      if (task == null)
      {
        return NotFound();
      }

      return Ok(task);
    }
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<System.Threading.Tasks.Task>>> GetUserTasks(string userId)
    {
      var tasks = await _context.Task.Where(t => t.UserId == userId).ToListAsync();
      return Ok(tasks);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAllTasks()
    {
      _context.Task.RemoveRange(_context.Task);
      await _context.SaveChangesAsync();

      return NoContent();
    }
    [HttpDelete("user/{userId}")]
    public async Task<IActionResult> DeleteAllUserTasks(string userId)
    {
      _context.Task.RemoveRange(_context.Task.Where(t => t.UserId == userId));
      await _context.SaveChangesAsync();

      return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTaskById(string id)
    {
      var task = await _context.Task.FindAsync(id);

      if (task == null)
      {
        return NotFound();
      }

      _context.Task.Remove(task);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<System.Threading.Tasks.Task>> CreateTask(Entities.Task task)
    {
      _context.Task.Add(task);
      await _context.SaveChangesAsync();

      return Ok(task);
    }
  }
}