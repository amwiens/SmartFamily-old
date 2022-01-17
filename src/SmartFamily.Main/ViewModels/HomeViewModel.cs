using Microsoft.Extensions.Logging;

using Prism.Mvvm;
using Prism.Regions;

using SmartFamily.Core.Constants;
using SmartFamily.Core.Models;

using System;
using System.Collections.ObjectModel;

namespace SmartFamily.Main.ViewModels
{
    public class HomeViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly ILogger<HomeViewModel> _logger;

        private IRegionNavigationService? _navigationService;

        /// <summary>
        /// Gets the recent files.
        /// </summary>
        public ObservableCollection<RecentFile> RecentFiles { get; } = new ObservableCollection<RecentFile>();

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
            _navigationService = _regionManager.Regions[Regions.Hamburger].NavigationService;
            _navigationService.Navigated += OnNavigated;

            RecentFiles.Add(new RecentFile { FileName = "test.sfdb", FilePath = "C:\\Code", LastOpened = DateTime.Today });
            RecentFiles.Add(new RecentFile { FileName = "test1.sfdb", FilePath = "C:\\Code", LastOpened = DateTime.Today });
        }

        /// <summary>
        /// On navigated from.
        /// </summary>
        /// <param name="navigationContext">Navigation context.</param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            _logger.LogInformation("HomeViewModel: Navigated from.");
        }
    }
}