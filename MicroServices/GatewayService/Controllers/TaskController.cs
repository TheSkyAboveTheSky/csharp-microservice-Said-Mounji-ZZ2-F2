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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTask(string id, Entities.Task task)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"api/Task/{id}", task);
            Console.WriteLine(response.Content);
            Console.WriteLine(response.StatusCode);

            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            else
            {
                return BadRequest("UpdateTask failed");
            }
        }
        [Authorize]
        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchTask(string id, TaskModelUpdate task)
        {
            HttpResponseMessage response = await client.PatchAsJsonAsync($"api/Task/{id}", task);
            Console.WriteLine(response.Content);
            Console.WriteLine(response.StatusCode);

            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            else
            {
                return BadRequest("PatchTask failed");
            }
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