using Data.Database;
using Data.Models.Entities.Log;
using Data.Models.Entities.User;
using Data.Models.Extensions;
using Data.Models.User;
using Microsoft.AspNetCore.Http;
using Shared.Enums.User;

namespace Logic.Administration
{
    public class UserAdministration
    {
        private readonly DatabaseContext _context;
        private readonly HttpContext _httpContext;

        public UserAdministration(DatabaseContext dbContext, HttpContext httpContext)
        {
            _context = dbContext;
            _httpContext = httpContext;
        }

        public async Task RegisterUser(UserModel user)
        {
            using (var unitOfWork = new UserAdministrationUnitOfWork(_context, _httpContext))
            {
                try
                {
                    var salt = Guid.NewGuid().ToString();

                    var hashedPassword = unitOfWork.GetHashedPassword(user.Password, salt);

                    if (hashedPassword == null)
                    {
                        return;
                    }

                    var credentialEntity = new UserCredentialEntity
                    {
                        Salt = salt,
                        Password = hashedPassword,
                        Token = string.Empty,
                        ExpieresAt = DateTime.UtcNow.AddDays(30),
                    };

                    var credentialsResult = await unitOfWork.UserCredentialRepository.Insert(credentialEntity);

                    var userEntity = user.ToEntity(credentialsResult.Key);

                    var userResult = await unitOfWork.UserRepository.Insert(userEntity);

                    await AssignUserToRole(unitOfWork, userResult.Key, user.UserRole);

                    await unitOfWork.Save(_httpContext);
                }
                catch (Exception exception)
                {
                    await unitOfWork.LogRepository.Insert(new LogEntity
                    {
                        Message = "Register new user failed.",
                        ExMessage = exception.Message,
                        StackTrace = exception.StackTrace,
                        TimeStamp = DateTime.UtcNow,
                        Trigger = nameof(UserAdministration)
                    });
                }
            }

        }
        public async Task SetUserActiveState(int userId, bool isActive)
        {
            using (var unitOfWork = new UserAdministrationUnitOfWork(_context, _httpContext))
            {
                try
                {
                    var user = await unitOfWork.UserRepository.GetFirstOrDefaultAsync(x => x.Id == userId, false);

                    if(user != null)
                    {
                        user.IsActive = isActive;

                        await unitOfWork.Save(_httpContext);
                    }
                }
                catch (Exception exception)
                {
                    await unitOfWork.LogRepository.Insert(new LogEntity
                    {
                        Message = "Enable or disable user failed.",
                        ExMessage = exception.Message,
                        StackTrace = exception.StackTrace,
                        TimeStamp = DateTime.UtcNow,
                        Trigger = nameof(UserAdministration)
                    });
                }
            }
        }

        #region private

        private async Task AssignUserToRole(UserAdministrationUnitOfWork unitOfWork, int userId, UserRoleEnum role)
        {
            var roleEntity = await unitOfWork.RoleRepository.GetFirstOrDefaultAsync(x => x.Name.Equals(role.ToString()));

            if (roleEntity == null)
            {
                return;
            }

            await unitOfWork.UserRoleRepository.Insert(new UserRoleEntity { AppuserId = userId, RoleId = roleEntity.Id });
        }
       

        #endregion
    }
}
