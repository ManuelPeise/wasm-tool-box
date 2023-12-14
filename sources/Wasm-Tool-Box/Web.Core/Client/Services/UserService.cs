using Data.Models.User;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Web.Core.Client.Services.Interfaces;

namespace Web.Core.Client.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<UserDataModel?> GetUserData(int userId)
        {
            return await _httpClient.GetFromJsonAsync<UserDataModel?>($"api/userdata/getprofile?userid={userId}");
        }

        public async Task UpdateUserData(UserDataModel userData)
        {
            await _httpClient.PostAsJsonAsync("api/userdata/updateprofile", userData);
        }

        public async Task UpdatePassword(UserPasswordModel userPasswordModel)
        {
            await _httpClient.PostAsJsonAsync("api/userdata/updatepassword", userPasswordModel);
        }
    }
}
