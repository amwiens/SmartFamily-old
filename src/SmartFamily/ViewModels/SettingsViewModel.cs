using Microsoft.Extensions.Logging;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

using SmartFamily.Contracts.Services;
using SmartFamily.Core.Contracts.Services;
using SmartFamily.Core.Models;

using System;
using System.Windows.Input;

namespace SmartFamily.ViewModels
{
    /// <summary>
    /// Settings view model.
    /// </summary>
    // TODO: Change the URL for your privacy policy in the appsettings.json file, currrently set to https://YourPrivacyUrlGoesHere/
    public class SettingsViewModel : BindableBase, INavigationAware
    {
        private readonly AppConfig _appConfig;
        private readonly IThemeSelectorService _themeSelectorService;
        private readonly ISystemService _systemService;
        private readonly IApplicationInfoService _applicationInfoService;
        private readonly IApplicationSettingsService _applicationSettingsService;
        private readonly ILogger<SettingsViewModel> _logger;

        private AppTheme _theme;
        private string _versionDescription;
        private bool _openLastClosedFile;
        private bool _askForBackup;
        private bool _addDateToBackup;
        private bool _checkForDuplicates;

        private ICommand _setThemeCommand;
        private ICommand _privacyStatmentCommand;

        /// <summary>
        /// App theme
        /// </summary>
        public AppTheme Theme
        {
            get { return _theme; }
            set { SetProperty(ref _theme, value); }
        }

        /// <summary>
        /// Version description.
        /// </summary>
        public string VersionDescription
        {
            get { return _versionDescription; }
            set { SetProperty(ref _versionDescription, value); }
        }

        /// <summary>
        /// Open last closed file.
        /// </summary>
        public bool OpenLastClosedFile
        {
            get { return _openLastClosedFile; }
            set
            {
                SetProperty(ref _openLastClosedFile, value);
                _applicationSettingsService.SetSetting("OpenLastClosedFile", value);
            }
        }

        /// <summary>
        /// Ask for backup.
        /// </summary>
        public bool AskForBackup
        {
            get { return _askForBackup; }
            set
            {
                SetProperty(ref _askForBackup, value);
                _applicationSettingsService.SetSetting("AskForBackup", value);
            }
        }

        /// <summary>
        /// Add date to backup.
        /// </summary>
        public bool AddDateToBackup
        {
            get { return _addDateToBackup; }
            set
            {
                SetProperty(ref _addDateToBackup, value);
                _applicationSettingsService.SetSetting("AddDateToBackup", value);
            }
        }

        /// <summary>
        /// Check for duplicates.
        /// </summary>
        public bool CheckForDuplicates
        {
            get { return _checkForDuplicates; }
            set
            {
                SetProperty(ref _checkForDuplicates, value);
                _applicationSettingsService.SetSetting("CheckForDuplicates", value);
            }
        }

        /// <summary>
        /// Set theme command.
        /// </summary>
        public ICommand SetThemeCommand => _setThemeCommand ?? (_setThemeCommand = new DelegateCommand<string>(OnSetTheme));

        /// <summary>
        /// Set privacy statement command.
        /// </summary>
        public ICommand PrivacyStatementCommand => _privacyStatmentCommand ?? (_privacyStatmentCommand = new DelegateCommand(OnPrivacyStatement));

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="appConfig">App config.</param>
        /// <param name="themeSelectorService">Theme selector service.</param>
        /// <param name="systemService">System service.</param>
        /// <param name="applicationInfoService">Application info service.</param>
        /// <param name="applicationSettingsService">Application settings service.</param>
        /// <param name="logger">Logger.</param>
        public SettingsViewModel(
            AppConfig appConfig,
            IThemeSelectorService themeSelectorService,
            ISystemService systemService,
            IApplicationInfoService applicationInfoService,
            IApplicationSettingsService applicationSettingsService,
            ILogger<SettingsViewModel> logger)
        {
            _appConfig = appConfig;
            _themeSelectorService = themeSelectorService;
            _systemService = systemService;
            _applicationInfoService = applicationInfoService;
            _applicationSettingsService = applicationSettingsService;
            _logger = logger;
        }

        /// <summary>
        /// Is navigation target.
        /// </summary>
        /// <param name="navigationContext">Navigation target.</param>
        /// <returns><c>true</c> if view can be navigated to, otherwise <c>false</c>.</returns>
        public bool IsNavigationTarget(NavigationContext navigationContext)
            => true;

        /// <summary>
        /// On navigated to.
        /// </summary>
        /// <param name="navigationContext">Navigation context.</param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            VersionDescription = $"SmartFamily - {_applicationInfoService.GetVersion()}";
            Theme = _themeSelectorService.GetCurrentTheme();
            OpenLastClosedFile = _applicationSettingsService.GetSetting<bool>("OpenLastClosedFile");
            AskForBackup = _applicationSettingsService.GetSetting<bool>("AskForBackup");
            AddDateToBackup = _applicationSettingsService.GetSetting<bool>("AddDateToBackup");
            CheckForDuplicates = _applicationSettingsService.GetSetting<bool>("CheckForDuplicates");
        }

        /// <summary>
        /// On navigated from.
        /// </summary>
        /// <param name="navigationContext">Navigation context.</param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        /// <summary>
        /// On set theme.
        /// </summary>
        /// <param name="themeName">Theme name.</param>
        private void OnSetTheme(string themeName)
        {
            var theme = (AppTheme)Enum.Parse(typeof(AppTheme), themeName);
            _themeSelectorService.SetTheme(theme);
        }

        /// <summary>
        /// On privacy statement.
        /// </summary>
        private void OnPrivacyStatement()
            => _systemService.OpenInWebBrowser(_appConfig.PrivacyStatement);
    }
}