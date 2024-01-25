using FrontAppBlazor.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FrontAppBlazor.Services;

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
                HttpResponseMessage response = await _httpClient.DeleteAsync($"http://localhost:5000/api/user/{id}");

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("User deleted");
                    await _authService.Logout();
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
    }
}
