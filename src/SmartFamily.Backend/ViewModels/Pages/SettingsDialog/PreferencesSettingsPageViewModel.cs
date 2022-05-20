using CommunityToolkit.Mvvm.DependencyInjection;

using SmartFamily.Backend.Services.Settings;

namespace SmartFamily.Backend.ViewModels.Pages.SettingsDialog;

public sealed class PreferencesSettingsPageViewModel : BaseSettingsDialogPageViewModel
{
    private IPreferencesSettingsService PreferencesSettingsService { get; } = Ioc.Default.GetRequiredService<IPreferencesSettingsService>();

    public PreferencesSettingsPageViewModel()
    {

    }
}