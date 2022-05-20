using SmartFamily.Backend.Services.Settings;
using SmartFamily.WinUI.Serialization;
using SmartFamily.WinUI.Serialization.Implementation;

using Windows.Storage;

namespace SmartFamily.WinUI.ServiceImplementation.Settings;

internal sealed class ApplicationSettingsService : BaseJsonSettings, IApplicationSettingsService
{
    public ApplicationSettingsService()
    {
        SettingsSerializer = new DefaultSettingsSerializer();
        JsonSettingsSerializer = new DefaultJsonSettingsSerializer();
        JsonSettingsDatabase = new CachingJsonSettingsDatabase(SettingsSerializer, JsonSettingsSerializer);

        Initialize(Path.Combine(ApplicationData.Current.LocalFolder.Path, Constants.LocalSettings.SETTINGS_FOLDER_NAME, Constants.LocalSettings.APPLICATION_SETTINGS_FILENAME));
    }

    public DateTime UpdateLastChecked
    {
        get => Get<DateTime>(() => new());
        set => Set<DateTime>(value);
    }
}