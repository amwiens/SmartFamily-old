using Microsoft.Extensions.Logging;

using Prism.Mvvm;
using Prism.Regions;

using SmartFamily.Core.Constants;
using SmartFamily.Core.Models;
using SmartFamily.EntityFramework.Contracts.Services;

using System.Collections.ObjectModel;

namespace SmartFamily.People.ViewModels
{
    /// <summary>
    /// People list view module.
    /// </summary>
    public class PeopleListViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly ILogger<PeopleListViewModel> _logger;
        private readonly ISampleDataService _sampleDataService;

        private IRegionNavigationService _navigationService;

        /// <summary>
        /// Source
        /// </summary>
        public ObservableCollection<SamplePerson> Source { get; } = new ObservableCollection<SamplePerson>();

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="sampleDataService">Sample data service.</param>
        /// <param name="logger">Logger.</param>
        public PeopleListViewModel(IRegionManager regionManager,
            ISampleDataService sampleDataService,
            ILogger<PeopleListViewModel> logger)
        {
            _regionManager = regionManager;
            _sampleDataService = sampleDataService;
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
        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            _navigationService = _regionManager.Regions[Regions.People].NavigationService;
            _navigationService.Navigated += OnNavigated;

            Source.Clear();

            // Replace this with your actual data
            var data = await _sampleDataService.GetGridDataAsync();

            foreach (var item in data)
            {
                Source.Add(item);
            }
        }

        /// <summary>
        /// On navigated from.
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}