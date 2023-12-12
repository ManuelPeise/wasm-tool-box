using Data.Models.Entities.Log;
using Data.Models.Entities.User;
using Data.Shared.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Logic.Shared.Interfaces
{
    public interface IUserAdministrationUnitOfWork: IDisposable
    {
        public IRepository<AppUserEntity> UserRepository { get; }
        public IRepository<UserCredentialEntity> UserCredentialRepository { get; }
        public IRepository<RoleEntity> RoleRepository { get; }
        public IRepository<UserRoleEntity> UserRoleRepository { get; }
        public IRepository<LogEntity> LogRepository { get; }
        Task Save(HttpContext httpContext);
    }
}
