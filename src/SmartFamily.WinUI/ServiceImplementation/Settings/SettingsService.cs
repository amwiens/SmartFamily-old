using CommunityToolkit.Mvvm.DependencyInjection;

using SmartFamily.Backend.Models;
using SmartFamily.Backend.Services.Settings;
using SmartFamily.Backend.ViewModels;
using SmartFamily.Shared.Extensions;
using SmartFamily.WinUI.Serialization;
using SmartFamily.WinUI.Serialization.Implementation;

using Windows.Storage;

namespace SmartFamily.WinUI.ServiceImplementation.Settings;

internal sealed class SettingsService : BaseJsonSettings, ISettingsService
{
    private IGeneralSettingsService? _generalSettingsService;
    public IGeneralSettingsService GeneralSettingsService
    {
        get => GetSettingsService(ref _generalSettingsService);
    }

    private IPreferencesSettingsService? _preferencesSettingsService;
    public IPreferencesSettingsService PreferencesSettingsService
    {
        get => GetSettingsService(ref _preferencesSettingsService);
    }

    public Dictionary<DatabaseIdModel, DatabaseViewModel> SavedDatabases
    {
        get => Get<List<KeyValuePair<DatabaseIdModel, DatabaseViewModel>>>(() => new())!.ToDictionary()!;
        set => Set<List<KeyValuePair<DatabaseIdModel, DatabaseViewModel>>>(value.ToList());
    }

    public SettingsService()
    {
        SettingsSerializer = new DefaultSettingsSerializer();
        JsonSettingsSerializer = new DefaultJsonSettingsSerializer();
        JsonSettingsDatabase = new CachingJsonSettingsDatabase(SettingsSerializer, JsonSettingsSerializer);

        Initialize(Path.Combine(ApplicationData.Current.LocalFolder.Path, Constants.LocalSettings.SETTINGS_FOLDER_NAME, Constants.LocalSettings.USER_SETTINGS_FILE_NAME));
    }

    private TSettingsService GetSettingsService<TSettingsService>(ref TSettingsService? settingsServiceMember)
        where TSettingsService : class, IBaseSettingsService
    {
        settingsServiceMember ??= Ioc.Default.GetRequiredService<TSettingsService>();
        return settingsServiceMember;
    }

    public TSharingContext GetSharingContext<TSharingContext>()
        where TSharingContext : class
    {
        return (TSharingContext)base.GetSharingContext();
    }
}