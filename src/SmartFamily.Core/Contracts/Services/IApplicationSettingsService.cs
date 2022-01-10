namespace SmartFamily.Core.Contracts.Services
{
    public interface IApplicationSettingsService
    {
        void SetSetting(string key, object value);

        T GetSetting<T>(string key);
    }
}