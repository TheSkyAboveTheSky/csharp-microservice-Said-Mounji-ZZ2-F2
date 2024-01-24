using GatewayService.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System;

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

    [HttpGet]
    public async Task<ActionResult> GetAllUsers(User user)
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
  }
}