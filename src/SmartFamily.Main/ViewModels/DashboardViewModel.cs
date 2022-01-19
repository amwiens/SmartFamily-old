using Microsoft.Extensions.Logging;

using Prism.Mvvm;
using Prism.Regions;

using SmartFamily.Core.Constants;

namespace SmartFamily.Main.ViewModels
{
    /// <summary>
    /// Dashboard view model.
    /// </summary>
    public class DashboardViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly ILogger<DashboardViewModel> _logger;

        private IRegionNavigationService _navigationService;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="regionManager">Region manager.</param>
        /// <param name="logger">Logger.</param>
        public DashboardViewModel(IRegionManager regionManager,
            ILogger<DashboardViewModel> logger)
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
            _logger.LogInformation("DashboardViewModel: Navigated to.");

            _navigationService = _regionManager.Regions[Regions.Hamburger].NavigationService;
            _navigationService.Navigated += OnNavigated;
        }

        /// <summary>
        /// On navigated from.
        /// </summary>
        /// <param name="navigationContext">Navigation context.</param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            _logger.LogInformation("DashboardViewModel: Navigated from.");
        }
    }
}