using GatewayService.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GatewayService.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProjectController : ControllerBase
  {
    HttpClient _httpClient;
    public ProjectController()
    {
      _httpClient = new HttpClient();
      _httpClient.BaseAddress = new System.Uri("http://localhost:5003/");
    }
    /// <summary>
    /// Retrieves all projects.
    /// </summary>
    [Authorize]
    [HttpGet]
    public async Task<ActionResult> GetAllProjects()
    {
      HttpResponseMessage response = await _httpClient.GetAsync($"api/Project/");
      Console.WriteLine(response.Content);
      Console.WriteLine(response.StatusCode);

      if (response.IsSuccessStatusCode)
      {
        var projects = await response.Content.ReadFromJsonAsync<Entities.Project[]>();
        return Ok(projects);
      }
      else
      {
        return BadRequest("GetAllProjects failed");
      }
    }
    /// <summary>
    /// Retrieves a project by id.
    /// </summary>
    /// <param name="id">Id of the project</param>
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult> GetProjectById(string id)
    {
      HttpResponseMessage response = await _httpClient.GetAsync($"api/Project/{id}");
      Console.WriteLine(response.Content);
      Console.WriteLine(response.StatusCode);

      if (response.IsSuccessStatusCode)
      {
        var project = await response.Content.ReadFromJsonAsync<Entities.Project>();
        return Ok(project);
      }
      else
      {
        return BadRequest("GetProjectById failed");
      }
    }
    /// <summary>
    /// Retrieves all projects by group id.
    /// </summary>
    /// <param name="groupId">Id of the group</param>
    [Authorize]
    [HttpGet("/groups/{groupId}")]
    public async Task<ActionResult> GetProjectsByGroupId(string groupId)
    {
      HttpResponseMessage response = await _httpClient.GetAsync($"api/Project/groups/{groupId}");
      Console.WriteLine(response.Content);
      Console.WriteLine(response.StatusCode);

      if (response.IsSuccessStatusCode)
      {
        var projects = await response.Content.ReadFromJsonAsync<Entities.Project[]>();
        return Ok(projects);
      }
      else
      {
        return BadRequest("GetProjectsByGroupId failed");
      }
    }
    /// <summary>
    ///  Delete a project by id.
    /// </summary>
    /// <param name="id">Id of the project</param>
    /// <returns></returns>
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProjectById(string id)
    {
      HttpResponseMessage response = await _httpClient.DeleteAsync($"api/Project/{id}");
      Console.WriteLine(response.Content);
      Console.WriteLine(response.StatusCode);

      if (response.IsSuccessStatusCode)
      {
        return Ok();
      }
      else
      {
        return BadRequest("DeleteProjectById failed");
      }
    }
    /// <summary>
    /// Delete all projects.
    /// </summary>
    [Authorize]
    [HttpDelete]
    public async Task<ActionResult> DeleteAllProjects()
    {
      HttpResponseMessage response = await _httpClient.DeleteAsync($"api/Project/");
      Console.WriteLine(response.Content);
      Console.WriteLine(response.StatusCode);

      if (response.IsSuccessStatusCode)
      {
        return Ok();
      }
      else
      {
        return BadRequest("DeleteAllProjects failed");
      }
    }
    /// <summary>
    /// Delete all projects by group id.
    /// </summary>
    /// <param name="groupId">Id of the group</param>
    /// <returns></returns>
    [Authorize]
    [HttpDelete("/groups/{groupId}")]
    public async Task<ActionResult> DeleteProjectsByGroupId(string groupId)
    {
      HttpResponseMessage response = await _httpClient.DeleteAsync($"api/Project/groups/{groupId}");
      Console.WriteLine(response.Content);
      Console.WriteLine(response.StatusCode);

      if (response.IsSuccessStatusCode)
      {
        return Ok();
      }
      else
      {
        return BadRequest("DeleteProjectsByGroupId failed");
      }
    }
    /// <summary>
    /// Create a project.
    /// </summary>
    /// <param name="project">Project object</param>
    [Authorize]
    [HttpPost]
    public async Task<ActionResult> CreateProject(Project project)
    {
      HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/Project/", project);
      Console.WriteLine(response.Content);
      Console.WriteLine(response.StatusCode);

      if (response.IsSuccessStatusCode)
      {
        return Ok();
      }
      else
      {
        return BadRequest("CreateProject failed");
      }
    }
    /// <summary>
    /// Update a project.
    /// </summary>
    /// <param name="id">Id of the project</param>
    /// <param name="project">Project object</param>
    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProject(string id, Project project)
    {
      HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/Project/{id}", project);
      Console.WriteLine(response.Content);
      Console.WriteLine(response.StatusCode);

      if (response.IsSuccessStatusCode)
      {
        return Ok();
      }
      else
      {
        return BadRequest("UpdateProject failed");
      }
    }
    /// <summary>
    /// Patch a project.
    /// </summary>
    /// <param name="id">Id of the project</param>
    /// <param name="project">Project object</param>
    [Authorize]
    [HttpPatch("{id}")]
    public async Task<ActionResult> PatchProject(string id, ProjectModelUpdate project)
    {
      HttpResponseMessage response = await _httpClient.PatchAsJsonAsync($"api/Project/{id}", project);
      Console.WriteLine(response.Content);
      Console.WriteLine(response.StatusCode);

      if (response.IsSuccessStatusCode)
      {
        return Ok();
      }
      else
      {
        return BadRequest("PatchProject failed");
      }
    }

  }
}