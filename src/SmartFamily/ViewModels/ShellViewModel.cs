using Microsoft.Extensions.Logging;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;

using SmartFamily.Contracts.Services;
using SmartFamily.Core;
using SmartFamily.Core.Constants;
using SmartFamily.Core.Contracts.Services;
using SmartFamily.Core.Exceptions;
using SmartFamily.Core.WPF.Contracts.Services;
using SmartFamily.Core.WPF.Dialogs;

using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace SmartFamily.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IRightPaneService _rightPaneService;
        private readonly IDialogService _dialogService;
        private readonly IApplicationSettingsService _applicationSettingsService;
        private readonly IOpenFileDialogService _openFileDialogService;
        private readonly IDatabaseService _databaseService;
        private readonly ILogger<ShellViewModel> _logger;

        private IRegionNavigationService _navigationService;
        private DelegateCommand _goBackCommand;
        private ICommand _menuFileOpenCommand;
        private ICommand _menuFileNewCommand;
        private ICommand _menuFileCloseDatabaseCommand;
        private ICommand _menuFileSettingsCommand;
        private ICommand _menuViewsDashboardCommand;
        private ICommand _loadedCommand;
        private ICommand _unloadedCommand;
        private ICommand _menuFileExitCommand;

        private bool _closeEnabled;

        public bool CloseEnabled
        {
            get => _closeEnabled;
            set => SetProperty(ref _closeEnabled, value);
        }

        public DelegateCommand GoBackCommand => _goBackCommand ?? (_goBackCommand = new DelegateCommand(OnGoBack, CanGoBack));

        public ICommand MenuFileOpenCommand => _menuFileOpenCommand ?? (_menuFileOpenCommand = new DelegateCommand(OnMenuFileOpen));

        public ICommand MenuFileNewCommand => _menuFileNewCommand ?? (_menuFileNewCommand = new DelegateCommand(OnMenuFileNew));

        public ICommand MenuFileCloseDatabaseCommand => _menuFileCloseDatabaseCommand ?? (_menuFileCloseDatabaseCommand = new DelegateCommand(OnMenuFileCloseDatabase, CanCloseDatabase));

        public ICommand MenuFileSettingsCommand => _menuFileSettingsCommand ?? (_menuFileSettingsCommand = new DelegateCommand(OnMenuFileSettings));

        public ICommand MenuViewsDashboardCommand => _menuViewsDashboardCommand ?? (_menuViewsDashboardCommand = new DelegateCommand(OnMenuViewsDashboard));

        public ICommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new DelegateCommand(OnLoaded));

        public ICommand UnloadedCommand => _unloadedCommand ?? (_unloadedCommand = new DelegateCommand(OnUnloaded));

        public ICommand MenuFileExitCommand => _menuFileExitCommand ?? (_menuFileExitCommand = new DelegateCommand(OnMenuFileExit));

        public ShellViewModel(IRegionManager regionManager,
            IRightPaneService rightPaneService,
            IDialogService dialogService,
            IApplicationSettingsService applicationSettingsService,
            IOpenFileDialogService openFileDialogService,
            IDatabaseService databaseService,
            ILogger<ShellViewModel> logger)
        {
            _regionManager = regionManager;
            _rightPaneService = rightPaneService;
            _dialogService = dialogService;
            _applicationSettingsService = applicationSettingsService;
            _openFileDialogService = openFileDialogService;
            _databaseService = databaseService;
            _logger = logger;
        }

        private void OnLoaded()
        {
            _navigationService = _regionManager.Regions[Regions.Main].NavigationService;
            _navigationService.Navigated += OnNavigated;

            if (_applicationSettingsService.GetSetting<bool>("OpenLastClosedFile") && !string.IsNullOrWhiteSpace(_applicationSettingsService.GetSetting<string>("LastOpenFile")))
            {
                OpenDatabaseFile(_applicationSettingsService.GetSetting<string>("LastOpenFile"));
            }

            _logger.LogInformation("Shell view model loaded.");
        }

        private void OnUnloaded()
        {
            _navigationService.Navigated -= OnNavigated;
            _regionManager.Regions.Remove(Regions.Main);
            _rightPaneService.CleanUp();
            _logger.LogInformation("Shell view model unloaded.");
        }

        private bool CanGoBack()
            => _navigationService != null && _navigationService.Journal.CanGoBack;

        private void OnGoBack()
            => _navigationService.Journal.GoBack();

        private bool RequestNavigate(string target)
        {
            if (_navigationService.CanNavigate(target))
            {
                _navigationService.RequestNavigate(target);
                return true;
            }

            return false;
        }

        private void RequestNavigateOnRightPane(string target)
            => _rightPaneService.OpenInRightPane(target);

        private void RequestNavigateAndCleanJournal(string target)
        {
            var navigated = RequestNavigate(target);
            if (navigated)
            {
                _navigationService.Journal.Clear();
            }
        }

        private void OnNavigated(object sender, RegionNavigationEventArgs e)
            => GoBackCommand.RaiseCanExecuteChanged();

        private void OnMenuFileExit()
        {
            if (_applicationSettingsService.GetSetting<bool>("OpenLastClosedFile"))
            {
                _applicationSettingsService.SetSetting("LastOpenFile", ApplicationSettings.OpenDatabase);
            }
            else
            {
                _applicationSettingsService.SetSetting("LastOpenFile", string.Empty);
            }

            CheckForBackup();
            Application.Current.Shutdown();
        }

        private void OnMenuViewsDashboard()
            => RequestNavigateAndCleanJournal(PageKeys.Dashboard);

        private void OnMenuFileSettings()
            => RequestNavigateOnRightPane(PageKeys.Settings);

        private void OnMenuFileOpen()
        {
            var result = _openFileDialogService.ShowOpenDatabaseDialog(out string fileName);
            if (result == true && !string.IsNullOrWhiteSpace(fileName))
            {
                OpenDatabaseFile(fileName);
            }
        }

        private void OnMenuFileNew()
        {
            var message = "this is the message";

            _dialogService.ShowNewFile(r =>
            {
                if (r.Result == ButtonResult.OK)
                {
                    var fileName = r.Parameters.GetValue<string>("FileName");
                    var fileLocation = r.Parameters.GetValue<string>("FileLocation");
                    var dbPath = Path.Combine(fileLocation, $"{fileName}.sfdb");
                    _databaseService.CreateDatabase(dbPath);
                    ApplicationSettings.OpenDatabase = dbPath;

                    RequestNavigateAndCleanJournal(PageKeys.Main);
                }
            });
        }

        private bool CanCloseDatabase()
            => !string.IsNullOrWhiteSpace(ApplicationSettings.OpenDatabase);

        private void OnMenuFileCloseDatabase()
        {
            ApplicationSettings.OpenDatabase = String.Empty;
            CloseEnabled = false;

            RequestNavigateAndCleanJournal(PageKeys.Home);
        }

        private void CheckForBackup()
        {
            var askForBackup = App.Current.Properties.Contains("AskForBackup") ? (bool)App.Current.Properties["AskForBackup"] : false;

            if ((_applicationSettingsService.GetSetting<bool>("AskForBackup") || askForBackup) && !string.IsNullOrWhiteSpace(ApplicationSettings.OpenDatabase))
            {
                var message = "Would you like to backup this database?";

                _dialogService.ShowNotification(message, r =>
                {
                    if (r.Result == ButtonResult.OK)
                    {
                    }
                });
            }
        }

        /// <summary>
        /// Opens the database file.
        /// </summary>
        /// <param name="databasePath">Path to the database file.</param>
        private void OpenDatabaseFile(string databasePath)
        {
            try
            {
                ApplicationSettings.OpenDatabase = _databaseService.OpenDatabase(databasePath);

                CloseEnabled = true;
                RequestNavigateAndCleanJournal(PageKeys.Main);
            }
            catch (DatabaseFormatException ex)
            {
                _logger.LogError(ex, ex.Message);
                _dialogService.ShowNotification(ex.Message, r =>
                {
                    if (r.Result == ButtonResult.OK)
                    {
                    }
                });
            }
        }
    }
}