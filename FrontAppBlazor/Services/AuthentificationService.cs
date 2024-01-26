using FrontAppBlazor.Entities;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

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
      var login = new UserLogin() { Email = email, Password = password };

      HttpResponseMessage response = await _httpClient.PostAsJsonAsync("http://localhost:5000/api/user/login", login);
      if (response.IsSuccessStatusCode)
      {
        var result = await response.Content.ReadFromJsonAsync<UserToken>();
        if (result != null)
        {
          await _sessionStorage.SetAsync("jwt", result.Token);
          await _sessionStorage.SetAsync("id", result.User.Id.ToString());
          await _sessionStorage.SetAsync("role", result.User.Role.ToString());
          await _sessionStorage.SetAsync("group", result.User.GroupId.ToString());

          return result.User;
        }
      }
      return null;
    }
    public async Task<User?> RegisterUser(string prenom, string nom, string email, string password, string username, string gender)
    {
      var registerInfo = new User(nom, prenom, email, password, username, gender);
      HttpResponseMessage response = await _httpClient.PostAsJsonAsync("http://localhost:5000/api/User/register", registerInfo);
      if (response.IsSuccessStatusCode)
      {
        var result = await response.Content.ReadFromJsonAsync<UserToken>();
        if (result != null)
        {
          await _sessionStorage.SetAsync("jwt", result.Token);
          await _sessionStorage.SetAsync("id", result.User.Id.ToString());
          await _sessionStorage.SetAsync("role", result.User.Role.ToString());
          await _sessionStorage.SetAsync("group", result.User.GroupId.ToString());


          return result.User;
        }
      }
      return null;
    }
    public async System.Threading.Tasks.Task Logout()
    {
      await _sessionStorage.DeleteAsync("jwt");
      await _sessionStorage.DeleteAsync("id");
      await _sessionStorage.DeleteAsync("role");
      await _sessionStorage.DeleteAsync("group");
    }
    public async Task<String> GetId()
    {
      var id = await _sessionStorage.GetAsync<string>("id");
      return id.Value;
    }
    public async Task<String> GetToken()
    {
      var token = await _sessionStorage.GetAsync<string>("jwt");
      return token.Value;
    }
    public async Task<String> GetRole()
    {
      var role = await _sessionStorage.GetAsync<string>("role");
      return role.Value;
    }
    public async Task<String> GetGroup()
    {
      var group = await _sessionStorage.GetAsync<string>("group");
      return group.Value;
    }
    public async Task<bool> IsAdmin()
    {
      var role = await GetRole();
      return role == "Admin";
    }
    public async Task<bool> IsProjectManager()
    {
      var role = await GetRole();
      return role == "ProjectManager";
    }
  }
}