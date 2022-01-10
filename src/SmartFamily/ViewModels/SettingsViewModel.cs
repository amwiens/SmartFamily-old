using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

using SmartFamily.Contracts.Services;
using SmartFamily.Core.Models;

using System;
using System.Windows.Input;

namespace SmartFamily.ViewModels
{
    // TODO: Change the URL for your privacy policy in the appsettings.json file, currrently set to https://YourPrivacyUrlGoesHere/
    public class SettingsViewModel : BindableBase, INavigationAware
    {
        private readonly AppConfig _appConfig;
        private readonly IThemeSelectorService _themeSelectorService;
        private readonly ISystemService _systemService;
        private readonly IApplicationInfoService _applicationInfoService;
        private readonly IApplicationSettingsService _applicationSettingsService;

        private AppTheme _theme;
        private string _versionDescription;
        private bool _openLastClosedFile;
        private bool _askForBackup;
        private bool _addDateToBackup;
        private bool _checkForDuplicates;

        private ICommand _setThemeCommand;
        private ICommand _privacyStatmentCommand;

        public AppTheme Theme
        {
            get { return _theme; }
            set { SetProperty(ref _theme, value); }
        }

        public string VersionDescription
        {
            get { return _versionDescription; }
            set { SetProperty(ref _versionDescription, value); }
        }

        public bool OpenLastClosedFile
        {
            get { return _openLastClosedFile; }
            set
            {
                SetProperty(ref _openLastClosedFile, value);
                _applicationSettingsService.SetSetting("OpenLastClosedFile", value);
            }
        }

        public bool AskForBackup
        {
            get { return _askForBackup; }
            set
            {
                SetProperty(ref _askForBackup, value);
                _applicationSettingsService.SetSetting("AskForBackup", value);
            }
        }

        public bool AddDateToBackup
        {
            get { return _addDateToBackup; }
            set
            {
                SetProperty(ref _addDateToBackup, value);
                _applicationSettingsService.SetSetting("AddDateToBackup", value);
            }
        }

        public bool CheckForDuplicates
        {
            get { return _checkForDuplicates; }
            set
            {
                SetProperty(ref _checkForDuplicates, value);
                _applicationSettingsService.SetSetting("CheckForDuplicates", value);
            }
        }

        public ICommand SetThemeCommand => _setThemeCommand ?? (_setThemeCommand = new DelegateCommand<string>(OnSetTheme));

        public ICommand PrivacyStatementCommand => _privacyStatmentCommand ?? (_privacyStatmentCommand = new DelegateCommand(OnPrivacyStatement));

        public SettingsViewModel(
            AppConfig appConfig,
            IThemeSelectorService themeSelectorService,
            ISystemService systemService,
            IApplicationInfoService applicationInfoService,
            IApplicationSettingsService applicationSettingsService)
        {
            _appConfig = appConfig;
            _themeSelectorService = themeSelectorService;
            _systemService = systemService;
            _applicationInfoService = applicationInfoService;
            _applicationSettingsService = applicationSettingsService;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            VersionDescription = $"SmartFamily - {_applicationInfoService.GetVersion()}";
            Theme = _themeSelectorService.GetCurrentTheme();
            OpenLastClosedFile = _applicationSettingsService.GetSetting<bool>("OpenLastClosedFile");
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        private void OnSetTheme(string themeName)
        {
            var theme = (AppTheme)Enum.Parse(typeof(AppTheme), themeName);
            _themeSelectorService.SetTheme(theme);
        }

        private void OnPrivacyStatement()
            => _systemService.OpenInWebBrowser(_appConfig.PrivacyStatement);

        public bool IsNavigationTarget(NavigationContext navigationContext)
            => true;
    }
}