using Shared.Enums.Settings;

namespace Logic.Shared.Interfaces
{
    public interface ISettingsUnitOfWork: IDisposable
    {
        Task<string> GetAsJson(SettingsTypeEnum settingsType);
        Task<T?> GetAsModel<T>(SettingsTypeEnum settingsType);
        Task AddOrUpdateSettings(SettingsTypeEnum settingsType, string json);

    }
}
