using Data.Database;
using Data.Models.Entities.Log;
using Data.Models.Entities.User;
using Data.Shared;
using Data.Shared.Interfaces;
using Logic.Shared;
using Logic.Shared.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace Logic.Administration
{
    public class UserAdministrationUnitOfWork : AUnitOfWork, IUserAdministrationUnitOfWork
    {
        private bool disposedValue;
        private readonly HttpContext _httpContext;
        private IRepository<AppUserEntity>? _userRepository;
        private IRepository<UserCredentialEntity>? _userCredentialsRepository;
        private IRepository<RoleEntity>? _roleRepository;
        private IRepository<UserRoleEntity>? _userRoleRepository;
        private IRepository<LogEntity>? _logRepository;

        public IRepository<AppUserEntity> UserRepository { get => _userRepository??=new RepositoryBase<AppUserEntity>(Context); }
        public IRepository<UserCredentialEntity> UserCredentialRepository { get => _userCredentialsRepository ??= new RepositoryBase<UserCredentialEntity>(Context); }
        public IRepository<RoleEntity> RoleRepository { get => _roleRepository ??= new RepositoryBase<RoleEntity>(Context); }
        public IRepository<UserRoleEntity> UserRoleRepository { get => _userRoleRepository ??= new RepositoryBase<UserRoleEntity>(Context); }
        public IRepository<LogEntity> LogRepository { get => _logRepository ??= new RepositoryBase<LogEntity>(Context); }
        
        public UserAdministrationUnitOfWork(DatabaseContext databaseContext, HttpContext httpContext): base(databaseContext) 
        {
            _httpContext = httpContext;
        }

        public string? GetHashedPassword(string password, string salt)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(salt))
            {
                return null;
            }

            var bytes = Encoding.ASCII.GetBytes(password).ToList();
            bytes.AddRange(Encoding.ASCII.GetBytes(salt));

            return Convert.ToBase64String(bytes.ToArray());
        }
        #region dispose

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Context.Dispose();
                }

                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
