using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectService.Data;
using ProjectService.Entities;
using Microsoft.AspNetCore.Identity;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace ProjectService.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProjectController : ControllerBase
  {
    private readonly DataContext _context;

    public ProjectController(DataContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
    {
      var projects = await _context.Project.ToListAsync();
      return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Project>> GetProjectById(string id)
    {
      var project = await _context.Project.FindAsync(id);

      if (project == null)
      {
        return NotFound();
      }

      return project;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProjectById(string id)
    {
      var project = await _context.Project.FindAsync(id);

      if (project == null)
      {
        return NotFound();
      }

      _context.Project.Remove(project);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAllProjects()
    {
      var projects = await _context.Project.ToListAsync();

      if (projects == null)
      {
        return NotFound();
      }

      _context.Project.RemoveRange(projects);
      await _context.SaveChangesAsync();

      return NoContent();
    }
    [HttpDelete("/groups/{groupId}")]
    public async Task<IActionResult> DeleteAllProjectsByGroupId(int groupId)
    {
      var projects = await _context.Project.Where(p => p.GroupId == groupId).ToListAsync();

      if (projects == null)
      {
        return NotFound();
      }

      _context.Project.RemoveRange(projects);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Project>> PostProject(Project project)
    {
      _context.Project.Add(project);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetProjectById", new { id = project.Id }, project);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutProject(string id, Project project)
    {
      if (id != project.Id)
      {
        return BadRequest();
      }

      _context.Entry(project).State = EntityState.Modified;
      await _context.SaveChangesAsync();

      return NoContent();
    }
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchProject(string id, ProjectModelUpdate project)
    {
      var projectToUpdate = await _context.Project.FindAsync(id);
      if (projectToUpdate == null)
      {
        return NotFound();
      }
      if (project.Nom != null)
      {
        projectToUpdate.Nom = project.Nom;
      }
      if (project.Description != null)
      {
        projectToUpdate.Description = project.Description;
      }
      if (project.Status != null)
      {
        projectToUpdate.Status = project.Status;
      }
      if (project.GroupId != null)
      {
        projectToUpdate.GroupId = project.GroupId.Value;
      }
      _context.Entry(projectToUpdate).State = EntityState.Modified;
      await _context.SaveChangesAsync();
      return NoContent();
    }
    [HttpGet("/groups/{groupId}")]
    public async Task<ActionResult<Project[]>> GetProjectByGroupId(int groupId)
    {
      var projects = await _context.Project.Where(p => p.GroupId == groupId).ToListAsync();

      if (projects == null)
      {
        return NotFound();
      }

      return Ok(projects);
    }
  }
}