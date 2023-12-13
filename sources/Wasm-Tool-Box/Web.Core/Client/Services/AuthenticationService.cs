using Blazored.LocalStorage;
using Data.Models.User;
using System.Net;
using System.Net.Http.Json;
using Web.Core.Client.Services.Interfaces;

namespace Web.Core.Client.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public AuthenticationService(HttpClient client, ILocalStorageService localStorage)
        {
            _httpClient = client;
            _localStorage = localStorage;
        }

        public async Task<bool> Login(UserLoginModel loginModel)
        {
            var result = await _httpClient.PostAsJsonAsync("api/authentication/signin", loginModel);

            var jwtToken = await result.Content.ReadAsStringAsync();

            if (result.StatusCode == HttpStatusCode.BadRequest || string.IsNullOrWhiteSpace(jwtToken))
            {
                return false;
            }

            if (result.IsSuccessStatusCode && !string.IsNullOrWhiteSpace(jwtToken))
            {
                await _localStorage.SetItemAsync("token", jwtToken);

                return true;
            }

            return false;
        }

        public async Task<bool> Logout(int userId)
        {
            var result = await _httpClient.PostAsync($"api/authentication/signout?userid={userId}", null);

            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadAsStringAsync();

                if (bool.TryParse(response, out var success))
                {
                    if (success)
                    {
                        await _localStorage.SetItemAsync<string>("token", "");
                    }
                }

                return success;
            }

            return false;
        }
    }
}
