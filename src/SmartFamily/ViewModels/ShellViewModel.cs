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
using SmartFamily.Core.Models;
using SmartFamily.Core.WPF.Contracts.Services;
using SmartFamily.Core.WPF.Dialogs;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SmartFamily.ViewModels
{
    /// <summary>
    /// Shell view model.
    /// </summary>
    public class ShellViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly IRightPaneService _rightPaneService;
        private readonly IDialogService _dialogService;
        private readonly IApplicationSettingsService _applicationSettingsService;
        private readonly IOpenFileDialogService _openFileDialogService;
        private readonly IDatabaseService _databaseService;
        private readonly ILogger<ShellViewModel> _logger;

        private IRegionNavigationService? _navigationService;
        private ICommand? _menuFileOpenCommand;
        private ICommand? _menuFileNewCommand;
        private ICommand? _menuFileCloseDatabaseCommand;
        private ICommand? _menuFileSettingsCommand;
        private ICommand? _menuViewsDashboardCommand;
        private ICommand? _loadedCommand;
        private ICommand? _unloadedCommand;
        private ICommand? _menuFileExitCommand;

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
        /// Is navigation target.
        /// </summary>
        /// <param name="navigationContext">Navigation target.</param>
        /// <returns><c>true</c> if view can be navigated to, otherwise <c>false</c>.</returns>
        public bool IsNavigationTarget(NavigationContext navigationContext)
            => false;

        /// <summary>
        /// On navigated to.
        /// </summary>
        /// <param name="navigationContext">Navigation context.</param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _logger.LogInformation("ShellViewModel: Navigated to.");
        }

        /// <summary>
        /// On navigated from.
        /// </summary>
        /// <param name="navigationContext">Navigation context.</param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            _logger.LogInformation("ShellViewModel: Navigated from.");
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
            else
            {
                RequestNavigateAndCleanJournal(PageKeys.Home);
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
            _dialogService.ShowNewFile(r =>
            {
                if (r.Result == ButtonResult.OK)
                {
                    var fileName = r.Parameters.GetValue<string>("FileName");
                    var fileLocation = r.Parameters.GetValue<string>("FileLocation");
                    var dbPath = Path.Combine(fileLocation, $"{fileName}.sfdb");
                    if (File.Exists(dbPath))
                    {
                        _dialogService.ShowNotification("Database already exists!", r => { });
                    }
                    else
                    {
                        _databaseService.CreateDatabase(dbPath);

                        _logger.LogInformation("ShellViewModel: {dbPath} database created.", dbPath);
                        OpenDatabaseFile(dbPath);
                    }
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
            CloseDatabaseFile();
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
        /// Opens the database file.
        /// </summary>
        /// <param name="databasePath">Path to the database file.</param>
        private void OpenDatabaseFile(string databasePath)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(ApplicationSettings.OpenDatabase))
                {
                    CloseDatabaseFile();
                }
                ApplicationSettings.OpenDatabase = _databaseService.OpenDatabase(databasePath);

                if (ApplicationSettings.RecentFiles is null)
                {
                    ApplicationSettings.RecentFiles = new List<RecentFile>();
                }
                var recentFile = new RecentFile
                {
                    FileName = databasePath.Substring(databasePath.LastIndexOf('\\') + 1),
                    FilePath = databasePath.Substring(0, databasePath.LastIndexOf('\\')),
                    LastOpened = DateTime.Now,
                };

                var file = ApplicationSettings.RecentFiles.Where(x => x.FileName == recentFile.FileName && x.FilePath == recentFile.FilePath);

                if (file.Any())
                {
                    file.FirstOrDefault().LastOpened = DateTime.Now;
                }
                else
                {
                    ApplicationSettings.RecentFiles.Add(recentFile);
                }

                CloseEnabled = true;
                _logger.LogInformation("ShellViewModel: {databasePath} opened.", databasePath);
                RequestNavigateAndCleanJournal(PageKeys.Main);
            }
            catch (DatabaseFormatException ex)
            {
                _logger.LogError(ex, ex.Message);
                _dialogService.ShowNotification(ex.Message, r => { });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _dialogService.ShowNotification(ex.Message, r => { });
            }
        }

        /// <summary>
        /// Close the database file.
        /// </summary>
        private void CloseDatabaseFile()
        {
            CheckForBackup();
            _logger.LogInformation("ShellViewModel: {ApplicationSettings.OpenDatabase} closed.", ApplicationSettings.OpenDatabase);

            ApplicationSettings.OpenDatabase = String.Empty;
            CloseEnabled = false;

            RequestNavigateAndCleanJournal(PageKeys.Home);
        }

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
    }
}