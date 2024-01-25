using FrontAppBlazor.Entities;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Net.Http;

namespace FrontAppBlazor.Services
{
  public class AuthentificationService
  {
    private readonly HttpClient _httpClient;
    private ProtectedLocalStorage _sessionStorage;
    public AuthentificationService(HttpClient httpClient, ProtectedLocalStorage sessionStorage)
    {
      _httpClient = httpClient;
      _sessionStorage = sessionStorage;
    }
    public async Task<User?> AuthenticateUser(string email, string password)
    {
      var login = new UserLogin() { Email = email, Pass = password };

      HttpResponseMessage response = await _httpClient.PostAsJsonAsync("http://localhost:5000/api/user/login", login);
      if (response.IsSuccessStatusCode)
      {
        var result = await response.Content.ReadFromJsonAsync<UserToken>();
        if (result != null)
        {
          await _sessionStorage.SetAsync("jwt", result.Token);
          await _sessionStorage.SetAsync("id", result.User.Id.ToString());

          return result.User;
        }
      }
      return null;
    }
    public async Task<User?> RegisterUser(string prenom, string nom, string email, string password, string username)
    {
      var registerInfo = new User(nom, prenom, email, password, username);
      HttpResponseMessage response = await _httpClient.PostAsJsonAsync("http://localhost:5000/api/User/register", registerInfo);
      if (response.IsSuccessStatusCode)
      {
        var result = await response.Content.ReadFromJsonAsync<UserToken>();
        if (result != null)
        {
          await _sessionStorage.SetAsync("jwt", result.Token);
          await _sessionStorage.SetAsync("id", result.User.Id.ToString());
          return result.User;
        }
      }
      return null;
    }
    public async System.Threading.Tasks.Task Logout()
    {
      await _sessionStorage.DeleteAsync("jwt");
      await _sessionStorage.DeleteAsync("id");
    }
    public async Task<String> GetId()
    {
      var id = await _sessionStorage.GetAsync<string>("id");
      return id.Value;
    }
  }
}