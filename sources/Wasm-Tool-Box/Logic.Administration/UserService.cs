using Data.Database;
using Data.Models.Entities.Log;
using Data.Models.Entities.User;
using Data.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Logic.Administration
{
    public class UserService : IDisposable
    {
        private readonly DatabaseContext _context;
        private readonly HttpContext _httpContext;
        private readonly IConfiguration _configuration;

        private bool disposedValue;

        public UserService(DatabaseContext context, HttpContext httpContext, IConfiguration configuration)
        {
            _context = context;
            _httpContext = httpContext;
            _configuration = configuration;
        }

        public async Task<UserDataModel?> GetProfileData(int userId)
        {
            using (var unitOfWork = new UserAdministrationUnitOfWork(_context, _httpContext))
            {
                try
                {
                    var userEntity = await unitOfWork.UserRepository.GetFirstOrDefaultAsync(x => x.Id == userId);

                    if (userEntity == null)
                    {
                        return null;
                    }

                    return new UserDataModel
                    {
                        UserId = userEntity.Id,
                        FirstName = userEntity.FirstName,
                        LastName = userEntity.LastName,
                        Email = userEntity.Email,
                        FailedLogins = userEntity.FailedLogins,
                        IsActive = userEntity.IsActive,
                    };
                }
                catch (Exception exception)
                {
                    await unitOfWork.LogRepository.Insert(new LogEntity
                    {
                        Message = $"Loading user data for {userId} failed!",
                        ExMessage = exception.Message,
                        StackTrace = exception.StackTrace,
                        TimeStamp = DateTime.UtcNow,
                        Trigger = nameof(UserService)
                    });
                }

                return null;
            }
        }
        public async Task<string?> LoginAsync(UserLoginModel loginModel)
        {
            var claims = new List<Claim>();
            string? token = null;

            using (var unitOfWork = new UserAdministrationUnitOfWork(_context, _httpContext))
            {
                try
                {
                    var userEntity = await unitOfWork.UserRepository.GetFirstOrDefaultAsync(x => x.Email.ToLower().Equals(loginModel.EmailAdress.ToLower()));

                    if (userEntity == null)
                    {
                        return token;
                    }

                    var credentialsEntity = await unitOfWork.UserCredentialRepository.GetFirstOrDefaultAsync(x => x.Id == userEntity.CredentialsId);

                    if (credentialsEntity == null || credentialsEntity.Password == null || credentialsEntity.Salt == null)
                    {
                        return token;
                    }

                    var hashedPassword = unitOfWork.GetHashedPassword(loginModel.Password, credentialsEntity.Salt);

                    if (hashedPassword != null && hashedPassword.Equals(credentialsEntity.Password))
                    {
                        claims = await GetUserClaims(unitOfWork, userEntity);
                    }
                    else
                    {
                        userEntity.FailedLogins = userEntity.FailedLogins + 1;

                        if (userEntity.FailedLogins == 3)
                        {
                            userEntity.IsActive = false;
                        }

                        await unitOfWork.Save(_httpContext);


                    }

                    if (claims.Any())
                    {
                        var jwt = GenerateToken(claims);

                        token = new JwtSecurityTokenHandler().WriteToken(jwt);

                        credentialsEntity.Token = token;

                        unitOfWork.UserCredentialRepository.Update(credentialsEntity);

                        await unitOfWork.Save(_httpContext);
                    }
                }
                catch (Exception exception)
                {
                    await unitOfWork.LogRepository.Insert(new LogEntity
                    {
                        Message = "Login failed!",
                        ExMessage = exception.Message,
                        StackTrace = exception.StackTrace,
                        TimeStamp = DateTime.UtcNow,
                        Trigger = nameof(UserService)
                    });
                }

            }

            return token;
        }
        public async Task LogOutAsync(int userId)
        {
            using (var unitOfWork = new UserAdministrationUnitOfWork(_context, _httpContext))
            {
                try
                {
                    var userEntity = await unitOfWork.UserRepository.GetFirstOrDefaultAsync(x => x.Id == userId);

                    if (userEntity == null)
                    {
                        throw new Exception($"Could not logout user [{userId}], Reason: user not found!");
                    }

                    var credentials = await unitOfWork.UserCredentialRepository.GetFirstOrDefaultAsync(x => x.Id == userEntity.CredentialsId);

                    if (credentials == null)
                    {
                        throw new Exception("Could not logout user [{userId}], Reason: could not find credentials!");
                    }

                    credentials.Token = null;

                    unitOfWork.UserCredentialRepository.Update(credentials);

                    await unitOfWork.Save(_httpContext);

                }
                catch (Exception exception)
                {
                    await unitOfWork.LogRepository.Insert(new LogEntity
                    {
                        Message = $"Logout for User [{userId}] failed!",
                        ExMessage = exception.Message,
                        StackTrace = exception.StackTrace,
                        TimeStamp = DateTime.UtcNow,
                        Trigger = nameof(UserService)
                    });
                }
            }
        }
        public async Task UpdateUserData(UserDataModel userData)
        {
            using (var unitOfWork = new UserAdministrationUnitOfWork(_context, _httpContext))
            {
                try
                {
                    var userEntity = await unitOfWork.UserRepository.GetFirstOrDefaultAsync(x => x.Id == userData.UserId, false);

                    if (userEntity != null)
                    {
                        userEntity.FirstName = userData.FirstName;
                        userEntity.LastName = userData.LastName;
                        userEntity.Email = userData.Email;
                        userEntity.IsActive = userData.IsActive;
                        userData.FailedLogins = userData.FailedLogins;

                        unitOfWork.UserRepository.Update(userEntity);

                        await unitOfWork.Save(_httpContext);
                    }
                }
                catch (Exception exception)
                {
                    await unitOfWork.LogRepository.Insert(new LogEntity
                    {
                        Message = $"Update user data for user {userData.UserId} failed!",
                        ExMessage = exception.Message,
                        StackTrace = exception.StackTrace,
                        TimeStamp = DateTime.UtcNow,
                        Trigger = nameof(UserService)
                    });
                }
            }
        }
        public async Task UpdatePassword(UserPasswordModel passwordModel)
        {
            if (passwordModel == null)
            {
                return;
            }

            using (var unitOfWork = new UserAdministrationUnitOfWork(_context, _httpContext))
            {
                try
                {
                    var credentials = await unitOfWork.UserCredentialRepository.GetFirstOrDefaultAsync(x => x.Id == passwordModel.CredentialId);

                    if (credentials == null || credentials.Salt == null || credentials.Password == null)
                    {
                        return;
                    }

                    var oldPassword = unitOfWork.GetHashedPassword(passwordModel.CurrentPassword, credentials.Salt);

                    if (oldPassword != null && oldPassword.ToLower().Equals(credentials.Password))
                    {
                        var salt = Guid.NewGuid().ToString();

                        var newHashedPassword = unitOfWork.GetHashedPassword(passwordModel.NewPassword, salt);

                        credentials.Salt = salt;
                        credentials.Password = newHashedPassword;
                        credentials.ExpieresAt = DateTime.UtcNow.AddDays(90);

                        unitOfWork.UserCredentialRepository.Update(credentials);

                        await unitOfWork.Save(_httpContext);
                    }
                }
                catch (Exception exception)
                {
                    await unitOfWork.LogRepository.Insert(new LogEntity
                    {
                        Message = $"Update password for User [{passwordModel.UserId}] failed!",
                        ExMessage = exception.Message,
                        StackTrace = exception.StackTrace,
                        TimeStamp = DateTime.UtcNow,
                        Trigger = nameof(UserService)
                    });
                }
            }
        }

        private async Task<List<Claim>> GetUserClaims(UserAdministrationUnitOfWork unitOfWork, AppUserEntity user)
        {
            var rolesOfUser = new List<KeyValuePair<int, string>>();

            var userRoles = await unitOfWork.UserRoleRepository.GetAllAsync(x => x.AppuserId == user.Id);

            foreach (var usrRole in userRoles)
            {
                var role = await unitOfWork.RoleRepository.GetFirstOrDefaultAsync(x => x.Id == usrRole.Id);

                if (role != null)
                {
                    rolesOfUser.Add(new KeyValuePair<int, string>(role.Id, role.Name));
                }
            }


            var claims = new List<Claim> {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(user)),
            };

            foreach (var role in rolesOfUser)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Value));
            }

            return claims;
        }
        private JwtSecurityToken GenerateToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["apiKey"] ?? throw new InvalidOperationException("Secret")));

            return new JwtSecurityToken(
                issuer: "Test",
                audience: "Test",
                expires: DateTime.Now.AddMinutes(60),
                claims: claims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature));
        }


        #region dispose

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
