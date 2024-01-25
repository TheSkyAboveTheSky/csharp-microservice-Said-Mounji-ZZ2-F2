using FrontAppBlazor.Entities;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Todo = FrontAppBlazor.Entities.Task;

namespace FrontAppBlazor.Services
{
  public class TaskService
  {
    private readonly HttpClient _httpClient;

    public TaskService(HttpClient httpClient)
    {
      _httpClient = httpClient;
    }
    public async Task<Todo[]> GetAllTasks()
    {
      try
      {
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
    public async Task<Todo[]> GetUserTasks(string userId)
    {
      try
      {
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
  }
}
