using Microsoft.Extensions.Logging;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;

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
using System.Windows.Input;

namespace SmartFamily.Main.ViewModels
{
    /// <summary>
    /// Home view model.
    /// </summary>
    public class HomeViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly IOpenFileDialogService _openFileDialogService;
        private readonly IDatabaseService _databaseService;
        private readonly IDialogService _dialogService;
        private readonly ILogger<HomeViewModel> _logger;

        private IRegionNavigationService? _navigationService;
        private List<RecentFile> _recentFiles;

        private ICommand? _menuFileOpenCommand;

        /// <summary>
        /// Gets the recent files.
        /// </summary>
        public List<RecentFile> RecentFiles
        {
            get => _recentFiles;
            set
            {
                SetProperty(ref _recentFiles, value);
            }
        }

        /// <summary>
        /// File -> open menu command.
        /// </summary>
        public ICommand MenuFileOpenCommand => _menuFileOpenCommand ?? (_menuFileOpenCommand = new DelegateCommand<RecentFile>(OnMenuFileOpen));

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="regionManager">Region manager.</param>
        /// <param name="logger">Logger.</param>
        public HomeViewModel(IRegionManager regionManager,
            IOpenFileDialogService openFileDialogService,
            IDatabaseService databaseService,
            IDialogService dialogService,
            ILogger<HomeViewModel> logger)
        {
            _regionManager = regionManager;
            _openFileDialogService = openFileDialogService;
            _databaseService = databaseService;
            _dialogService = dialogService;
            _logger = logger;

            GetRecentFiles();
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
        /// On navigated.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event args.</param>
        private void OnNavigated(object? sender, RegionNavigationEventArgs e)
        {
        }

        /// <summary>
        /// Is navigation target.
        /// </summary>
        /// <param name="navigationContext">Navigation context.</param>
        /// <returns><c>true</c> if it can be navigated to, otherwise <c>false</c>.</returns>
        public bool IsNavigationTarget(NavigationContext navigationContext)
            => true;

        /// <summary>
        /// On navigated to.
        /// </summary>
        /// <param name="navigationContext">Navigation context.</param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _navigationService = _regionManager.Regions[Regions.Main].NavigationService;
            _navigationService.Navigated += OnNavigated;
            _logger.LogInformation("HomeViewModel: Navigated to.");

            GetRecentFiles();
        }

        /// <summary>
        /// On navigated from.
        /// </summary>
        /// <param name="navigationContext">Navigation context.</param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            _logger.LogInformation("HomeViewModel: Navigated from.");
        }

        /// <summary>
        /// Get the recent files that have been opened.
        /// </summary>
        private void GetRecentFiles()
        {
            RecentFiles = ApplicationSettings.RecentFiles.OrderByDescending(x => x.LastOpened).Take(10).ToList();
        }

        /// <summary>
        /// Open a file.
        /// </summary>
        private void OnMenuFileOpen(RecentFile recentFile)
        {
            var fileName = Path.Combine(recentFile.FilePath, recentFile.FileName);
            if (File.Exists(fileName))
                OpenDatabaseFile(fileName);
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

                //CloseEnabled = true;
                _logger.LogInformation("HomeViewModel: {databasePath} opened.", databasePath);
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
    }
}