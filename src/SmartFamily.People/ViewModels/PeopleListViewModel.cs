using Microsoft.Extensions.Logging;

using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

using SmartFamily.Core;
using SmartFamily.Core.Constants;
using SmartFamily.Core.WPF.Events;
using SmartFamily.EntityFramework.Contracts.Services;

using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SmartFamily.People.ViewModels
{
    /// <summary>
    /// People list view module.
    /// </summary>
    public class PeopleListViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly ILogger<PeopleListViewModel> _logger;
        private readonly ISampleDataService _sampleDataService;

        private IRegionNavigationService _navigationService;
        private Individual _selectedPerson;

        private ICommand? _selectPersonCommand;

        /// <summary>
        /// Source
        /// </summary>
        public ObservableCollection<Individual> People { get; } = new ObservableCollection<Individual>();

        public Individual SelectedPerson
        {
            get => _selectedPerson;
            set => SetProperty(ref _selectedPerson, value);
        }

        /// <summary>
        /// Select person command.
        /// </summary>
        public ICommand SelectPersonCommand => _selectPersonCommand ?? (_selectPersonCommand = new DelegateCommand<Individual>(OnPersonSelect));

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="regionManager">Region manager.</param>
        /// <param name="eventAggregator">Event aggregator.</param>
        /// <param name="sampleDataService">Sample data service.</param>
        /// <param name="logger">Logger.</param>
        public PeopleListViewModel(IRegionManager regionManager,
            IEventAggregator eventAggregator,
            ISampleDataService sampleDataService,
            ILogger<PeopleListViewModel> logger)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _sampleDataService = sampleDataService;
            _logger = logger;
        }

        /// <summary>
        /// Request navigate.
        /// </summary>
        /// <param name="target">Target page.</param>
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
        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            _logger.LogInformation("PeopleListViewModel: Navigated to.");
            _eventAggregator.GetEvent<SetPeoplePageTitleEvent>().Publish("People list");

            _navigationService = _regionManager.Regions[Regions.People].NavigationService;
            _navigationService.Navigated += OnNavigated;

            People.Clear();

            // TODO: Replace this with your actual data
            var data = await _sampleDataService.GetGridDataAsync();

            foreach (var item in data)
            {
                People.Add(item);
            }
        }

        /// <summary>
        /// On navigated from.
        /// </summary>
        /// <param name="navigationContext">Navigation context.</param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            _logger.LogInformation("PeopleListViewModel: Navigated from.");
        }

        /// <summary>
        /// Opens the person view with the selected person in focus.
        /// </summary>
        /// <param name="person">Selected person.</param>
        private void OnPersonSelect(Individual person)
        {
            _logger.LogInformation($"{person.Name} selected.");

            var navigationParameters = new NavigationParameters();
            navigationParameters.Add("Person", person);

            RequestNavigate(PageKeys.Person, navigationParameters);
        }
    }
}