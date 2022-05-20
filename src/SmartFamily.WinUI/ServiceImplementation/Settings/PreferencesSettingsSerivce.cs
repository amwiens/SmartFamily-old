using SmartFamily.Backend.Services.Settings;
using SmartFamily.WinUI.Serialization;

namespace SmartFamily.WinUI.ServiceImplementation.Settings;

internal sealed class PreferencesSettingsSerivce : BaseJsonSettings, IPreferencesSettingsService
{
    public PreferencesSettingsSerivce(ISettingsSharingContext settingsSharingContext)
    {
        RegisterSettingsContext(settingsSharingContext);
    }
}