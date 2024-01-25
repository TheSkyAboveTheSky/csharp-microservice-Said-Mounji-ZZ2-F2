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
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(string id, Entities.Task task)
    {
      task.Id = id;
      _context.Entry(task).State = EntityState.Modified;
      await _context.SaveChangesAsync();

      return NoContent();
    }
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateTaskPatch(string id, TaskModelUpdate task)
    {
      var taskToUpdate = await _context.Task.FindAsync(id);
      if(task.Titre != null)
      {
        taskToUpdate.Titre = task.Titre;
      }
      if(task.Description != null)
      {
        taskToUpdate.Description = task.Description;
      }
      if(task.UserId != null)
      {
        taskToUpdate.UserId = task.UserId;
      }
      if(task.IsChecked != null)
      {
        taskToUpdate.IsChecked = (bool)task.IsChecked;
      }
      _context.Entry(taskToUpdate).State = EntityState.Modified;
      await _context.SaveChangesAsync();

      return NoContent();
    }
    public class TaskModelUpdate
    {
      public string? Titre { get; set; }
      public string? Description { get; set; }
      public string? UserId { get; set; }
      public bool? IsChecked { get; set; }
    }
  }
}