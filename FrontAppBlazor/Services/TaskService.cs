using FrontAppBlazor.Entities;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Todo = FrontAppBlazor.Entities.Task;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Net.Http.Headers;


namespace FrontAppBlazor.Services
{
  public class TaskService
  {
    private readonly HttpClient _httpClient;
    private readonly AuthentificationService _authService;

    public TaskService(HttpClient httpClient, AuthentificationService authService)
    {

      _httpClient = httpClient;
      _authService = authService;
    }
    public async Task<Todo[]> GetAllTasks()
    {
      try
      {
        var jwt = await _authService.GetToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:5000/api/task");

        if (response.IsSuccessStatusCode)
        {
          var result = await response.Content.ReadFromJsonAsync<Todo[]>();
          return result ?? new Todo[0];
        }
        else
        {
          return new Todo[0];
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"An error occurred while loading tasks: {ex.Message}");
        return new Todo[0];
      }
    }
    public async Task<Todo> GetTaskById(string id)
    {
      try
      {
        var jwt = await _authService.GetToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        HttpResponseMessage response = await _httpClient.GetAsync($"http://localhost:5000/api/task/{id}");

        if (response.IsSuccessStatusCode)
        {
          var result = await response.Content.ReadFromJsonAsync<Todo>();
          return result;
        }
        else
        {
          return null;
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"An error occurred while loading tasks: {ex.Message}");
        return null;
      }
    }
    public async Task<Todo[]> GetUserTasks()
    {
      try
      {
        var userId = await _authService.GetId();
        var jwt = await _authService.GetToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        HttpResponseMessage response = await _httpClient.GetAsync($"http://localhost:5000/api/task/user/{userId}");

        if (response.IsSuccessStatusCode)
        {
          var result = await response.Content.ReadFromJsonAsync<Todo[]>();
          return result ?? new Todo[0];
        }
        else
        {
          return new Todo[0];
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"An error occurred while loading tasks: {ex.Message}");
        return new Todo[0];
      }
    }
    public async System.Threading.Tasks.Task DeleteAllTasks()
    {
      try
      {
        var jwt = await _authService.GetToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        HttpResponseMessage response = await _httpClient.DeleteAsync($"http://localhost:5000/api/task");

        if (response.IsSuccessStatusCode)
        {
          Console.WriteLine("Tasks deleted");
        }
        else
        {
          Console.WriteLine("Tasks not deleted");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"An error occurred while loading tasks: {ex.Message}");
      }
    }
    public async System.Threading.Tasks.Task DeleteTaskById(string id)
    {
      try
      {
        var jwt = await _authService.GetToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        HttpResponseMessage response = await _httpClient.DeleteAsync($"http://localhost:5000/api/task/{id}");

        if (response.IsSuccessStatusCode)
        {
          Console.WriteLine("Task deleted");
        }
        else
        {
          Console.WriteLine("Task not deleted");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"An error occurred while loading tasks: {ex.Message}");
      }
    }
    public async System.Threading.Tasks.Task DeleteAllUserTasks()
    {

      try
      {
        var userId = await _authService.GetId();
        var jwt = await _authService.GetToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        HttpResponseMessage response = await _httpClient.DeleteAsync($"http://localhost:5000/api/task/user/{userId}");

        if (response.IsSuccessStatusCode)
        {
          Console.WriteLine("Tasks deleted");
        }
        else
        {
          Console.WriteLine("Tasks not deleted");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"An error occurred while loading tasks: {ex.Message}");
      }
    }
    public async System.Threading.Tasks.Task CreateTask(Todo task)
    {
      try
      {
        var jwt = await _authService.GetToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"http://localhost:5000/api/task", task);

        if (response.IsSuccessStatusCode)
        {
          Console.WriteLine("Task created");
        }
        else
        {
          Console.WriteLine("Task not created");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"An error occurred while loading tasks: {ex.Message}");
      }
    }
    public async System.Threading.Tasks.Task UpdateTask(string id, Todo task)
    {
      try
      {
        var jwt = await _authService.GetToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"http://localhost:5000/api/task/{id}", task);

        if (response.IsSuccessStatusCode)
        {
          Console.WriteLine("Task updated");
        }
        else
        {
          Console.WriteLine("Task not updated");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"An error occurred while loading tasks: {ex.Message}");
      }
    }
    public async System.Threading.Tasks.Task PatchTask(string id, TaskModelUpdate task)
    {
      try
      {
        var jwt = await _authService.GetToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        HttpResponseMessage response = await _httpClient.PatchAsJsonAsync($"http://localhost:5000/api/task/{id}", task);

        if (response.IsSuccessStatusCode)
        {
          Console.WriteLine("Task patched");
        }
        else
        {
          Console.WriteLine("Task not patched");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"An error occurred while loading tasks: {ex.Message}");
      }
    }
  }
}
