using SmartFamily.Backend.Models;
using SmartFamily.Backend.ViewModels;

namespace SmartFamily.Backend.Services.Settings;

public interface ISettingsService : IBaseSettingsService
{
    IGeneralSettingsService GeneralSettingsService { get; }

    IPreferencesSettingsService PreferencesSettingsService { get; }

    Dictionary<DatabaseIdModel, DatabaseViewModel> SavedDatabases { get; set; }

    TSharingContext GetSharingContext<TSharingContext>() where TSharingContext : class;
}