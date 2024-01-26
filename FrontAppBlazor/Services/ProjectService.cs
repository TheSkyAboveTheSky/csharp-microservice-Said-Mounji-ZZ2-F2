using FrontAppBlazor.Entities;
using System.Net.Http.Headers;

namespace FrontAppBlazor.Services
{
  public class ProjectService
  {
    private readonly HttpClient _httpClient;
    private readonly AuthentificationService _authService;
    public ProjectService(HttpClient httpClient, AuthentificationService authService)
    {

      _httpClient = httpClient;
      _authService = authService;
    }
    public async Task<Project[]> GetAllProjects()
    {
      try
      {
        var jwt = await _authService.GetToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:5000/api/project");

        if (response.IsSuccessStatusCode)
        {
          var result = await response.Content.ReadFromJsonAsync<Project[]>();
          return result ?? new Project[0];
        }
        else
        {
          return new Project[0];
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"An error occurred while loading projects: {ex.Message}");
        return new Project[0];
      }
    }
    public async Task<Project> GetProjectById(string id)
    {
      try
      {
        var jwt = await _authService.GetToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        HttpResponseMessage response = await _httpClient.GetAsync($"http://localhost:5000/api/project/{id}");

        if (response.IsSuccessStatusCode)
        {
          var result = await response.Content.ReadFromJsonAsync<Project>();
          return result;
        }
        else
        {
          return null;
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"An error occurred while loading project: {ex.Message}");
        return null;
      }
    }
    public async Task<Project[]> GetProjectsByGroupId()
    {
      try
      {
        var jwt = await _authService.GetToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        var groupId = await _authService.GetGroup();
        HttpResponseMessage response = await _httpClient.GetAsync($"http://localhost:5000/api/project/groups/{groupId}");

        if (response.IsSuccessStatusCode)
        {
          var result = await response.Content.ReadFromJsonAsync<Project[]>();
          return result ?? new Project[0];
        }
        else
        {
          return new Project[0];
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"An error occurred while loading projects: {ex.Message}");
        return new Project[0];
      }
    }
    public async Task<Project> CreateProject(Project project)
    {
      try
      {
        var jwt = await _authService.GetToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("http://localhost:5000/api/project", project);

        if (response.IsSuccessStatusCode)
        {
          var result = await response.Content.ReadFromJsonAsync<Project>();
          return result;
        }
        else
        {
          return null;
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"An error occurred while creating project: {ex.Message}");
        return null;
      }
    }
    public async Task<Project> UpdateProject(string id, ProjectModelUpdate project)
    {
      try
      {
        var jwt = await _authService.GetToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        HttpResponseMessage response = await _httpClient.PatchAsJsonAsync($"http://localhost:5000/api/project/{id}", project);

        if (response.IsSuccessStatusCode)
        {
          var result = await response.Content.ReadFromJsonAsync<Project>();
          return result;
        }
        else
        {
          return null;
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"An error occurred while patching project: {ex.Message}");
        return null;
      }
    }
    public async Task<bool> DeleteProject(string id)
    {
      try
      {
        var jwt = await _authService.GetToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        HttpResponseMessage response = await _httpClient.DeleteAsync($"http://localhost:5000/api/project/{id}");

        if (response.IsSuccessStatusCode)
        {
          return true;
        }
        else
        {
          return false;
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"An error occurred while deleting project: {ex.Message}");
        return false;
      }
    }
    public async Task<bool> DeleteProjectsByGroupId()
    {
      try
      {
        var jwt = await _authService.GetToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        var groupId = await _authService.GetGroup();
        HttpResponseMessage response = await _httpClient.DeleteAsync($"http://localhost:5000/api/project/groups/{groupId}");

        if (response.IsSuccessStatusCode)
        {
          return true;
        }
        else
        {
          return false;
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"An error occurred while deleting projects: {ex.Message}");
        return false;
      }
    }
    public async Task<bool> DeleteAllProjects()
    {
      try
      {
        var jwt = await _authService.GetToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        HttpResponseMessage response = await _httpClient.DeleteAsync($"http://localhost:5000/api/project/");

        if (response.IsSuccessStatusCode)
        {
          return true;
        }
        else
        {
          return false;
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"An error occurred while deleting projects: {ex.Message}");
        return false;
      }
    }

  }
}