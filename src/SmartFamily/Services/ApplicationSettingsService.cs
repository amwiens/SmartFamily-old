using SmartFamily.Contracts.Services;
using SmartFamily.Core.Contracts.Services;

namespace SmartFamily.Services
{
    public class ApplicationSettingsService : IApplicationSettingsService
    {
        public T GetSetting<T>(string key)
        {
            if (App.Current.Properties.Contains(key))
            {
                var setting = (T)App.Current.Properties[key];
                return setting;
            }

            return default(T);
        }

        public void SetSetting(string key, object value)
        {
            App.Current.Properties[key] = value;
        }
    }
}