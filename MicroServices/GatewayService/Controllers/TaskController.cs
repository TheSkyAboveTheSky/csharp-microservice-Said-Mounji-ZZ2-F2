using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        /// <summary>
        /// Retrieves all tasks.
        /// </summary>
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
        /// <summary>
        /// Retrieves a task by ID.
        /// </summary>
        /// <param name="id">The ID of the task to retrieve.</param>
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
        /// <summary>
        /// Retrieves all tasks by user ID.
        /// </summary>
        /// <param name="userId">The ID of the user whose tasks to retrieve.</param>
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
        /// <summary>
        /// Deletes all tasks.
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Deletes a task by ID.
        /// </summary>
        /// <param name="id">The ID of the task to delete.</param>
        /// <returns></returns>
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
        /// <summary>
        /// Deletes all tasks by user ID.
        /// </summary>
        /// <param name="userId">The ID of the user whose tasks to delete.</param>
        /// <returns></returns>
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
        /// <summary>
        /// Creates a task.
        /// </summary>
        /// <param name="task">The task to create.</param>
        /// <returns>The created task.</returns>
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
        /// <summary>
        /// Updates a task.
        /// </summary>
        /// <param name="id">The ID of the task to update.</param>
        /// <param name="task">The task to update.</param>
        /// <returns></returns>
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
        /// <summary>
        /// Updates a task by ID.
        /// </summary>
        /// <param name="id">The ID of the task to update.</param>
        /// <param name="task">The updated task data.</param>
        /// <returns></returns>
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