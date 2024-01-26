using GatewayService.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GatewayService.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : ControllerBase
  {
    HttpClient client;
    public UserController()
    {
      client = new HttpClient();
      client.BaseAddress = new System.Uri("http://localhost:5001/");
    }
    /// <summary>
    /// Retrieves all users.
    /// </summary>
    [Authorize]
    [HttpGet]
    public async Task<ActionResult> GetAllUsers()
    {
      HttpResponseMessage response = await client.GetAsync($"api/User/");
      Console.WriteLine(response.Content);
      Console.WriteLine(response.StatusCode);

      if (response.IsSuccessStatusCode)
      {
        var users = await response.Content.ReadFromJsonAsync<Entities.User[]>();
        return Ok(users);
      }
      else
      {
        return BadRequest("GetAllUsers failed");
      }
    }
    /// <summary>
    /// Retrieves a user by ID.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.</param>
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult> GetUserById(string id)
    {
      HttpResponseMessage response = await client.GetAsync($"api/User/{id}");
      Console.WriteLine(response.Content);
      Console.WriteLine(response.StatusCode);

      if (response.IsSuccessStatusCode)
      {
        var user = await response.Content.ReadFromJsonAsync<Entities.User>();
        return Ok(user);
      }
      else
      {
        return BadRequest("GetUserById failed");
      }
    }
    /// <summary>
    /// Deletes all users.
    /// </summary>
    [Authorize]
    [HttpDelete]
    public async Task<ActionResult> DeleteAllUsers()
    {
      HttpResponseMessage response = await client.DeleteAsync($"api/User/");
      Console.WriteLine(response.Content);
      Console.WriteLine(response.StatusCode);

      if (response.IsSuccessStatusCode)
      {
        return Ok();
      }
      else
      {
        return BadRequest("DeleteAllUsers failed");
      }
    }
    /// <summary>
    /// Deletes a user by ID.
    /// </summary>
    /// <param name="id">The ID of the user to delete.</param>
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUserById(string id)
    {
      HttpResponseMessage response = await client.DeleteAsync($"api/User/{id}");
      Console.WriteLine(response.Content);
      Console.WriteLine(response.StatusCode);

      if (response.IsSuccessStatusCode)
      {
        return Ok();
      }
      else
      {
        return BadRequest("DeleteUserById failed");
      }
    }
    /// <summary>
    /// Updates a user by ID.
    /// </summary>
    /// <param name="id">The ID of the user to update.</param>
    /// <param name="user">The updated user data.</param>
    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser(string id, User user)
    {
      HttpResponseMessage response = await client.PutAsJsonAsync($"api/User/{id}", user);
      Console.WriteLine(response.Content);
      Console.WriteLine(response.StatusCode);

      if (response.IsSuccessStatusCode)
      {
        return Ok();
      }
      else
      {
        return BadRequest("UpdateUser failed");
      }
    }
    /// <summary>
    /// Partially updates a user by ID.
    /// </summary>
    /// <param name="id">The ID of the user to update.</param>
    /// <param name="user">The updated user data.</param>
    [Authorize]
    [HttpPatch("{id}")]
    public async Task<ActionResult> PatchUser(string id, UserModelUpdate user)
    {
      HttpResponseMessage response = await client.PatchAsJsonAsync($"api/User/{id}", user);
      Console.WriteLine(response.Content);
      Console.WriteLine(response.StatusCode);

      if (response.IsSuccessStatusCode)
      {
        return Ok();
      }
      else
      {
        return BadRequest("PatchUser failed");
      }
    }
    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="user">The user data for registration.</param>
    [HttpPost("register")]
    public async Task<ActionResult> CreateUser(User user)
    {
      HttpResponseMessage response = await client.PostAsJsonAsync($"api/User/register", user);
      Console.WriteLine(response.Content);
      Console.WriteLine(response.StatusCode);

      if (response.IsSuccessStatusCode)
      {
        var createdUserToken = await response.Content.ReadFromJsonAsync<Entities.UserToken>();
        return Ok(createdUserToken);
      }
      else
      {
        return BadRequest("CreateUser failed");
      }
    }
    /// <summary>
    /// Logs in a user.
    /// </summary>
    /// <param name="userLogin">The user login data.</param>
    [HttpPost("login")]
    public async Task<ActionResult> LoginUser(UserLogin userLogin)
    {
      HttpResponseMessage response = await client.PostAsJsonAsync($"api/User/login", userLogin);
      Console.WriteLine(response.Content);
      Console.WriteLine(response.StatusCode);

      if (response.IsSuccessStatusCode)
      {
        var createdUserToken = await response.Content.ReadFromJsonAsync<Entities.UserToken>();
        return Ok(createdUserToken);
      }
      else
      {
        return BadRequest("LoginUser failed");
      }
    }
  }

}