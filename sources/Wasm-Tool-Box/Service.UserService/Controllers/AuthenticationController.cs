using Data.Database;
using Data.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Service.Shared;

namespace Service.UserService.Controllers
{
    public class AuthenticationController : AApiController
    {
        private readonly DatabaseContext _context;
        private IConfiguration _configuration;

        public AuthenticationController(DatabaseContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost(Name = "SignIn")]
        public async Task<string?> SignIn([FromBody] UserLoginModel loginModel)
        {
            using (var userService = new Logic.Administration.UserService(_context, this.HttpContext, _configuration))
            {
                return await userService.LoginAsync(loginModel);
            }
        }

        [HttpPost(Name = "SignOut")]
        public async Task SignOut([FromQuery] int userId)
        {
            using (var userService = new Logic.Administration.UserService(_context, this.HttpContext, _configuration))
            {
                await userService.LogOutAsync(userId);
            }
        }
    }
}
