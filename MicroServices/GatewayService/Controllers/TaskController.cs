using GatewayService.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System;

namespace GatewayService.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TaskController : ControllerBase
  {
      HttpClient client;
      public TaskController()
      {
          client = new HttpClient();
          client.BaseAddress = new System.Uri("http://localhost:5002/");
      }
      [HttpGet]
      public async Task<ActionResult> GetAllTasks()
      {
          HttpResponseMessage response = await client.GetAsync($"api/Task/");
          Console.WriteLine(response.Content);
          Console.WriteLine(response.StatusCode);

          if (response.IsSuccessStatusCode)
          {
              var tasks = await response.Content.ReadFromJsonAsync<Entities.Task[]>();
              return Ok(tasks);
          }
          else
          {
              return BadRequest("GetAllTasks failed");
          }
      }
      [HttpGet("{id}")]
      public async Task<ActionResult> GetTaskById(string id)
      {
          HttpResponseMessage response = await client.GetAsync($"api/Task/{id}");
          Console.WriteLine(response.Content);
          Console.WriteLine(response.StatusCode);

          if (response.IsSuccessStatusCode)
          {
              var task = await response.Content.ReadFromJsonAsync<Entities.Task>();
              return Ok(task);
          }
          else
          {
              return BadRequest("GetTaskById failed");
          }
      }
      [HttpGet("user/{userId}")]
      public async Task<ActionResult> GetTasksByUserId(string userId)
      {
          HttpResponseMessage response = await client.GetAsync($"api/Task/user/{userId}");
          Console.WriteLine(response.Content);
          Console.WriteLine(response.StatusCode);

          if (response.IsSuccessStatusCode)
          {
              var tasks = await response.Content.ReadFromJsonAsync<Entities.Task[]>();
              return Ok(tasks);
          }
          else
          {
              return BadRequest("GetTasksByUserId failed");
          }
      }
      [HttpDelete]
      public async Task<ActionResult> DeleteAllTasks()
      {
          HttpResponseMessage response = await client.DeleteAsync($"api/Task/");
          Console.WriteLine(response.Content);
          Console.WriteLine(response.StatusCode);

          if (response.IsSuccessStatusCode)
          {
              return Ok();
          }
          else
          {
              return BadRequest("DeleteAllTasks failed");
          }
      }
      [HttpDelete("{id}")]
      public async Task<ActionResult> DeleteTaskById(string id)
      {
          HttpResponseMessage response = await client.DeleteAsync($"api/Task/{id}");
          Console.WriteLine(response.Content);
          Console.WriteLine(response.StatusCode);

          if (response.IsSuccessStatusCode)
          {
              return Ok();
          }
          else
          {
              return BadRequest("DeleteTaskById failed");
          }
      }
      [HttpDelete("user/{userId}")]
      public async Task<ActionResult> DeleteTasksByUserId(string userId)
      {
          HttpResponseMessage response = await client.DeleteAsync($"api/Task/user/{userId}");
          Console.WriteLine(response.Content);
          Console.WriteLine(response.StatusCode);

          if (response.IsSuccessStatusCode)
          {
              return Ok();
          }
          else
          {
              return BadRequest("DeleteTasksByUserId failed");
          }
      }
      [HttpPost]
      public async Task<ActionResult> CreateTask(Entities.Task task)
      {
          HttpResponseMessage response = await client.PostAsJsonAsync($"api/Task/", task);
          Console.WriteLine(response.Content);
          Console.WriteLine(response.StatusCode);

          if (response.IsSuccessStatusCode)
          {
              return Ok();
          }
          else
          {
              return BadRequest("CreateTask failed");
          }
      }
  }
}