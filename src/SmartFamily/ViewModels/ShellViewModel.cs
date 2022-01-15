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
    /// <summary>
    /// Shell view model.
    /// </summary>
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
        private ICommand _menuFileOpenCommand;
        private ICommand _menuFileNewCommand;
        private ICommand _menuFileCloseDatabaseCommand;
        private ICommand _menuFileSettingsCommand;
        private ICommand _menuViewsDashboardCommand;
        private ICommand _loadedCommand;
        private ICommand _unloadedCommand;
        private ICommand _menuFileExitCommand;

        private bool _closeEnabled;

        /// <summary>
        /// Checks if the close databasse menu item is enabled.
        /// </summary>
        public bool CloseEnabled
        {
            get => _closeEnabled;
            set => SetProperty(ref _closeEnabled, value);
        }

        /// <summary>
        /// File -> new menu command.
        /// </summary>
        public ICommand MenuFileNewCommand => _menuFileNewCommand ?? (_menuFileNewCommand = new DelegateCommand(OnMenuFileNew));

        /// <summary>
        /// File -> open menu command.
        /// </summary>
        public ICommand MenuFileOpenCommand => _menuFileOpenCommand ?? (_menuFileOpenCommand = new DelegateCommand(OnMenuFileOpen));

        /// <summary>
        /// File -> close menu command.
        /// </summary>
        public ICommand MenuFileCloseDatabaseCommand => _menuFileCloseDatabaseCommand ?? (_menuFileCloseDatabaseCommand = new DelegateCommand(OnMenuFileCloseDatabase, CanCloseDatabase));

        /// <summary>
        /// File -> settings menu command.
        /// </summary>
        public ICommand MenuFileSettingsCommand => _menuFileSettingsCommand ?? (_menuFileSettingsCommand = new DelegateCommand(OnMenuFileSettings));

        /// <summary>
        /// File -> exit menu command.
        /// </summary>
        public ICommand MenuFileExitCommand => _menuFileExitCommand ?? (_menuFileExitCommand = new DelegateCommand(OnMenuFileExit));

        /// <summary>
        /// Views -> dashboard menu command.
        /// </summary>
        public ICommand MenuViewsDashboardCommand => _menuViewsDashboardCommand ?? (_menuViewsDashboardCommand = new DelegateCommand(OnMenuViewsDashboard));

        /// <summary>
        /// Loaded command.
        /// </summary>
        public ICommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new DelegateCommand(OnLoaded));

        /// <summary>
        /// Unloaded command.
        /// </summary>
        public ICommand UnloadedCommand => _unloadedCommand ?? (_unloadedCommand = new DelegateCommand(OnUnloaded));

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="regionManager">Region manager.</param>
        /// <param name="rightPaneService">Right pane service.</param>
        /// <param name="dialogService">Dialog service.</param>
        /// <param name="applicationSettingsService">Application settings service.</param>
        /// <param name="openFileDialogService">Open file dialog service.</param>
        /// <param name="databaseService">Database service.</param>
        /// <param name="logger">Logger.</param>
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

        /// <summary>
        /// On loaded.
        /// </summary>
        private void OnLoaded()
        {
            _navigationService = _regionManager.Regions[Regions.Main].NavigationService;
            _navigationService.Navigated += OnNavigated;

            if (_applicationSettingsService.GetSetting<bool>("OpenLastClosedFile") && !string.IsNullOrWhiteSpace(_applicationSettingsService.GetSetting<string>("LastOpenFile")))
            {
                OpenDatabaseFile(_applicationSettingsService.GetSetting<string>("LastOpenFile"));
            }

            _logger.LogInformation("ShellViewModel: Loaded.");
        }

        /// <summary>
        /// On unloaded.
        /// </summary>
        private void OnUnloaded()
        {
            _navigationService.Navigated -= OnNavigated;
            _regionManager.Regions.Remove(Regions.Main);
            _rightPaneService.CleanUp();
            _logger.LogInformation("ShellViewModel: Unloaded.");
        }

        /// <summary>
        /// Request navigate.
        /// </summary>
        /// <param name="target">Target page.</param>
        /// <returns><c>true</c> if page can be navigated to, otherwise <c>false</c>.</returns>
        private bool RequestNavigate(string target)
        {
            if (_navigationService.CanNavigate(target))
            {
                _navigationService.RequestNavigate(target);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Open target in right pane.
        /// </summary>
        /// <param name="target">Target page.</param>
        private void RequestNavigateOnRightPane(string target)
            => _rightPaneService.OpenInRightPane(target);

        /// <summary>
        /// Navigate to the target page.
        /// </summary>
        /// <param name="target">Target page.</param>
        private void RequestNavigateAndCleanJournal(string target)
        {
            var navigated = RequestNavigate(target);
            if (navigated)
            {
                _navigationService.Journal.Clear();
            }
        }

        /// <summary>
        /// On navigated to.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event args.</param>
        private void OnNavigated(object? sender, RegionNavigationEventArgs e)
        {
        }

        /// <summary>
        /// New file.
        /// </summary>
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

        /// <summary>
        /// Open a file.
        /// </summary>
        private void OnMenuFileOpen()
        {
            var result = _openFileDialogService.ShowOpenDatabaseDialog(out string fileName);
            if (result == true && !string.IsNullOrWhiteSpace(fileName))
            {
                OpenDatabaseFile(fileName);
            }
        }

        /// <summary>
        /// Close the database.
        /// </summary>
        private void OnMenuFileCloseDatabase()
        {
            ApplicationSettings.OpenDatabase = String.Empty;
            CloseEnabled = false;

            RequestNavigateAndCleanJournal(PageKeys.Home);
        }

        /// <summary>
        /// Settings menu item.
        /// </summary>
        private void OnMenuFileSettings()
            => RequestNavigateOnRightPane(PageKeys.Settings);

        /// <summary>
        /// Exit the program.
        /// </summary>
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

        /// <summary>
        /// Open dashboard view.
        /// </summary>
        private void OnMenuViewsDashboard()
            => RequestNavigateAndCleanJournal(PageKeys.Dashboard);

        /// <summary>
        /// Can the database be closed.
        /// </summary>
        /// <returns><c>true</c> if a database can be closed, otherwise <c>false</c>.</returns>
        private bool CanCloseDatabase()
            => !string.IsNullOrWhiteSpace(ApplicationSettings.OpenDatabase);

        /// <summary>
        /// Check if a backup needs to be created.
        /// </summary>
        private void CheckForBackup()
        {
            var askForBackup = App.Current.Properties.Contains("AskForBackup") ? (bool)App.Current.Properties["AskForBackup"] : false;

            if ((_applicationSettingsService.GetSetting<bool>("AskForBackup") || askForBackup) && !string.IsNullOrWhiteSpace(ApplicationSettings.OpenDatabase))
            {
                var message = "Would you like to backup this database?";

                _dialogService.ShowNotification(message, r => { });
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
                _dialogService.ShowNotification(ex.Message, r => { });
            }
        }
    }
}