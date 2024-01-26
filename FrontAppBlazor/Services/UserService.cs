using FrontAppBlazor.Entities;
using System.Net.Http.Headers;

namespace FrontAppBlazor.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthentificationService _authService;



        public UserService(HttpClient httpClient, AuthentificationService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        public async Task<User[]> GetAllUsers()
        {
            try
            {
                var jwt = await _authService.GetToken();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:5000/api/user");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<User[]>();
                    Console.WriteLine(result[0].Email);
                    return result ?? new User[0];
                }
                else
                {
                    return new User[0];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading users: {ex.Message}");
                return new User[0];
            }
        }
        public async Task<User> GetUserById(string id)
        {
            try
            {
                var jwt = await _authService.GetToken();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                HttpResponseMessage response = await _httpClient.GetAsync($"http://localhost:5000/api/user/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<User>();
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading users: {ex.Message}");
                return null;
            }
        }
        public async System.Threading.Tasks.Task DeleteUserById(string id)
        {
            try
            {
                var jwt = await _authService.GetToken();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                HttpResponseMessage response = await _httpClient.DeleteAsync($"http://localhost:5000/api/user/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var userId = await _authService.GetId();
                    if (userId == id)
                    {
                        await _authService.Logout();
                    }
                    Console.WriteLine("User deleted");
                }
                else
                {
                    Console.WriteLine("User not deleted");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading users: {ex.Message}");
            }
        }
        public async System.Threading.Tasks.Task DeleteAllUsers()
        {
            try
            {
                var jwt = await _authService.GetToken();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                HttpResponseMessage response = await _httpClient.DeleteAsync($"http://localhost:5000/api/user");

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Users deleted");
                    await _authService.Logout();
                }
                else
                {
                    Console.WriteLine("Users not deleted");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading users: {ex.Message}");
            }
        }
        public async System.Threading.Tasks.Task UpdateUser(string id, UserModelUpdate user)
        {
            try
            {
                var jwt = await _authService.GetToken();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                HttpResponseMessage response = await _httpClient.PatchAsJsonAsync($"http://localhost:5000/api/user/{id}", user);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("User updated");
                }
                else
                {
                    Console.WriteLine("User not updated");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading users: {ex.Message}");
            }
        }
        public async Task<User?> CreateUser(string prenom, string nom, string email, string password, string username,string gender)
        {
            var registerInfo = new User(nom, prenom, email, password, username,gender);
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("http://localhost:5000/api/User/register", registerInfo);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<UserToken>();
                if (result != null)
                {
                    return result.User;
                }
            }
            return null;
        }
    }
}
