using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;

using SmartFamily.Backend.Enums;
using SmartFamily.Backend.Models;
using SmartFamily.Backend.Services;
using SmartFamily.Backend.Services.Settings;
using SmartFamily.Backend.ViewModels.Controls;

using System.Collections.ObjectModel;
using System.Diagnostics;

namespace SmartFamily.Backend.ViewModels.Pages.SettingsDialog;

public sealed class GeneralSettingsPageViewModel : BaseSettingsDialogPageViewModel
{
    private ILocalizationService LocalizationService { get; } = Ioc.Default.GetRequiredService<ILocalizationService>();

    private IUpdateService UpdateService { get; } = Ioc.Default.GetRequiredService<IUpdateService>();

    private IApplicationSettingsService ApplicationSettingsService { get; } = Ioc.Default.GetRequiredService<IApplicationSettingsService>();

    public ObservableCollection<AppLanguageModel> AppLanguages { get; }

    public InfoBarViewModel VersionInfoBar { get; }

    private string? _updateStatusText;
    public string? UpdateStatusText
    {
        get => _updateStatusText;
        set => SetProperty(ref _updateStatusText, value);
    }

    private bool _isUpdateSupported;
    public bool IsUpdateSupported
    {
        get => _isUpdateSupported;
        set => SetProperty(ref _isUpdateSupported, value);
    }

    private bool _isRestartRequired;
    public bool IsRestartRequired
    {
        get => _isRestartRequired;
        set => SetProperty(ref _isRestartRequired, value);
    }

    private DateTime _updateLastChecked;
    public DateTime UpdateLastChecked
    {
        get => _updateLastChecked;
        set
        {
            if (SetProperty(ref _updateLastChecked, value) && ApplicationSettingsService.IsAvailable)
            {
                ApplicationSettingsService.UpdateLastChecked = value;
            }
        }
    }

    private int _selectedLanguageIndex;
    public int SelectedLanguageIndex
    {
        get => _selectedLanguageIndex;
        set
        {
            if (SetProperty(ref _selectedLanguageIndex, value))
            {
                LocalizationService.SetActiveLanguage(AppLanguages[value]);

                IsRestartRequired = LocalizationService.CurrentAppLanguage.Id != AppLanguages[value].Id;
            }
        }
    }

    public IAsyncRelayCommand CheckForUpdatesCommand { get; }

    public GeneralSettingsPageViewModel()
    {
        if (ApplicationSettingsService.IsAvailable)
        {
            UpdateLastChecked = ApplicationSettingsService.UpdateLastChecked;
        }
        VersionInfoBar = new();
        AppLanguages = new(LocalizationService.GetLanguages());
        _updateStatusText = "Latest version installed";
        _isUpdateSupported = false;

        CheckForUpdatesCommand = new AsyncRelayCommand(CheckForUpdates);
    }

    public async Task ConfigureUpdates()
    {
        var updatingAppSupported = await UpdateService.AreAppUpdatesSupportedAsync();

        if (!updatingAppSupported)
        {
            IsUpdateSupported = false;
            VersionInfoBar.IsOpen = true;
            VersionInfoBar.Message = "Updates are not supported for the sideloaded version.";
            VersionInfoBar.InfoBarSeverity = InfoBarSeverityType.Warning;
            VersionInfoBar.CanBeClosed = false;
        }
    }

    private async Task CheckForUpdates()
    {
        UpdateLastChecked = DateTime.Now;

        Debug.WriteLine(await UpdateService.IsNewUpdateAvailableAsync());
    }
}