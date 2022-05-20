using SmartFamily.Backend.Services.Settings;
using SmartFamily.WinUI.Serialization;

namespace SmartFamily.WinUI.ServiceImplementation.Settings;

internal sealed class GeneralSettingsService : BaseJsonSettings, IGeneralSettingsService
{
    public GeneralSettingsService(ISettingsSharingContext settingsSharingContext)
    {
        RegisterSettingsContext(settingsSharingContext);
    }
}