using Data.Database;
using Data.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Service.Shared;

namespace Service.UserService.Controllers
{
    public class UserDataController : AApiController
    {
        private readonly DatabaseContext _context;
        private IConfiguration _configuration;

        public UserDataController(DatabaseContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet(Name = "GetProfile")]
        public async Task<UserDataModel?> GetProfile([FromQuery] int userId)
        {
            using (var userService = new Logic.Administration.UserService(_context, this.HttpContext, _configuration))
            {
                return await userService.GetProfileData(userId);
            }
        }

        [HttpPost(Name = "UpdateProfile")]
        public async Task UpdateProfile([FromBody] UserDataModel userData)
        {
            using (var userService = new Logic.Administration.UserService(_context, this.HttpContext, _configuration))
            {
                await userService.UpdateUserData(userData);
            }
        }

        [HttpPost(Name = "UpdatePassword")]
        public async Task UpdatePassword([FromBody] UserPasswordModel passwordModel)
        {
            using (var userService = new Logic.Administration.UserService(_context, this.HttpContext, _configuration))
            {
                await userService.UpdatePassword(passwordModel);
            }
        }
    }
}
