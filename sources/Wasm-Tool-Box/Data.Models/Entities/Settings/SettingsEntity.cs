using Shared.Enums.Settings;

namespace Data.Models.Entities.Settings
{
    public class SettingsEntity: AEntity
    {
        public SettingsTypeEnum SettingsType { get; set; }
        public string JsonValue { get; set; } = string.Empty;
    }
}
