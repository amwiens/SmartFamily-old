using Microsoft.Extensions.Logging;

using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

using SmartFamily.Core;
using SmartFamily.Core.Constants;
using SmartFamily.Core.WPF.Events;

namespace SmartFamily.People.ViewModels
{
    /// <summary>
    /// Person view model.
    /// </summary>
    public class PersonViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly ILogger<PersonViewModel> _logger;

        private IRegionNavigationService _navigationService;
        private Individual _person;

        /// <summary>
        /// Person
        /// </summary>
        public Individual Person
        {
            get => _person;
            set => SetProperty(ref _person, value);
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="regionManager">Region manager.</param>
        /// <param name="logger">Logger.</param>
        public PersonViewModel(IRegionManager regionManager,
            IEventAggregator eventAggregator,
            ILogger<PersonViewModel> logger)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _logger = logger;
        }

        /// <summary>
        /// Request navigate.
        /// </summary>
        /// <param name="target">Target page.</param>
        /// <param name="parameters">Navigation parameters.</param>
        /// <returns><c>true</c> if page can be navigated to, otherwise <c>false</c>.</returns>
        private bool RequestNavigate(string target, NavigationParameters parameters)
        {
            if (_navigationService.CanNavigate(target))
            {
                _navigationService.RequestNavigate(target, parameters);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Navigate to the target page.
        /// </summary>
        /// <param name="target">Target page.</param>
        private void RequestNavigateAndCleanJournal(string target, NavigationParameters parameters = null)
        {
            var navigated = RequestNavigate(target, parameters);
            //if (navigated)
            //{
            //    _navigationService.Journal.Clear();
            //}
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
            _logger.LogInformation("PersonViewModel: Navigated to.");
            _navigationService = _regionManager.Regions[Regions.People].NavigationService;
            _navigationService.Navigated += OnNavigated;

            Person = navigationContext.Parameters.GetValue<Individual>("Person");
            _eventAggregator.GetEvent<SetPeoplePageTitleEvent>().Publish(Person.Name);

            //var navigationParameters = new NavigationParameters();
            //navigationParameters.Add("Person", Person);

            //RequestNavigateAndCleanJournal(PageKeys.PersonData, navigationParameters);
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