using Microsoft.Extensions.Logging;

using Prism.Mvvm;
using Prism.Regions;

using SmartFamily.Core.Constants;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFamily.Places.ViewModels
{
    /// <summary>
    /// Places view model.
    /// </summary>
    public class PlacesViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly ILogger<PlacesViewModel> _logger;

        private IRegionNavigationService _navigationService;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="regionManager">Region manager.</param>
        /// <param name="logger">Logger.</param>
        public PlacesViewModel(IRegionManager regionManager,
            ILogger<PlacesViewModel> logger)
        {
            _regionManager = regionManager;
            _logger = logger;
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
            _logger.LogInformation("PlacesViewModel: Navigated to.");
            _navigationService = _regionManager.Regions[Regions.Places].NavigationService;
            _navigationService.Navigated += OnNavigated;

            //RequestNavigateAndCleanJournal(PageKeys.PeopleListView);
        }

        /// <summary>
        /// On navigated from.
        /// </summary>
        /// <param name="navigationContext">Navigation context.</param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            _logger.LogInformation("PlacesViewModel: Navigated from.");
        }
    }
}