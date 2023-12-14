using Data.Database;
using Data.Models.Entities.Settings;
using Data.Shared;
using Data.Shared.Interfaces;
using Logic.Shared.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json;
using Shared.Enums.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Shared
{
    internal class SettingsUnitOfWork : AUnitOfWork, ISettingsUnitOfWork
    {
        private bool disposedValue;
        private IRepository<SettingsEntity> _settingsRepo;
        private HttpContext _httpContext;

        public SettingsUnitOfWork(DatabaseContext context, HttpContext httpContext) : base(context)
        {
            _settingsRepo = new RepositoryBase<SettingsEntity>(Context);
            _httpContext = httpContext;
        }

        public async Task<string> GetAsJson(SettingsTypeEnum settingsType)
        {
            var entity = await _settingsRepo.GetFirstOrDefaultAsync(x => x.SettingsType == settingsType);

            if (entity != null)
            {
                return entity.JsonValue;
            }

            return string.Empty;
        }
        public async Task<T?> GetAsModel<T>(SettingsTypeEnum settingsType)
        {
            var entity = await _settingsRepo.GetFirstOrDefaultAsync(x => x.SettingsType == settingsType);

            if (entity != null)
            {
                var model = JsonConvert.DeserializeObject<T>(entity.JsonValue);

                return model != null ? model : default(T);
            }

            return default;
        }
        public async Task AddOrUpdateSettings(SettingsTypeEnum settingsType, string json)
        {
            var dbModified = false;

            var existing = await _settingsRepo.GetFirstOrDefaultAsync(x => x.SettingsType == settingsType);

            if (existing != null && !string.IsNullOrWhiteSpace(json))
            {
                existing.JsonValue = json;

                _settingsRepo.Update(existing);

                dbModified = true;
            }
            else if (!string.IsNullOrWhiteSpace(json))
            {
                await _settingsRepo.Insert(new SettingsEntity { SettingsType = settingsType, JsonValue = json });

                dbModified = true;
            }

            if (dbModified)
            {
                await Save(_httpContext);
            }
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
