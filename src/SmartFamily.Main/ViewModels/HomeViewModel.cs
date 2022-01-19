﻿using Microsoft.Extensions.Logging;

using Prism.Mvvm;
using Prism.Regions;

using SmartFamily.Core;
using SmartFamily.Core.Models;

using System.Collections.Generic;
using System.Linq;

namespace SmartFamily.Main.ViewModels
{
    /// <summary>
    /// Home view model.
    /// </summary>
    public class HomeViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly ILogger<HomeViewModel> _logger;

        private IRegionNavigationService? _navigationService;
        private List<RecentFile> _recentFiles;

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
        /// Ctor
        /// </summary>
        /// <param name="regionManager">Region manager.</param>
        /// <param name="logger">Logger.</param>
        public HomeViewModel(IRegionManager regionManager,
            ILogger<HomeViewModel> logger)
        {
            _regionManager = regionManager;
            _logger = logger;

            GetRecentFiles();
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
    }
}