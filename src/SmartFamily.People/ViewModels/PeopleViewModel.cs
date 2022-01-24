using Microsoft.Extensions.Logging;

using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

using SmartFamily.Core.Constants;
using SmartFamily.Core.WPF.Events;

namespace SmartFamily.People.ViewModels
{
    /// <summary>
    /// People view model.
    /// </summary>
    public class PeopleViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly ILogger<PeopleViewModel> _logger;

        private IRegionNavigationService _navigationService;
        private DelegateCommand _goBackCommand;
        private string _title;

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        /// <summary>
        /// Go back command.
        /// </summary>
        public DelegateCommand GoBackCommand => _goBackCommand ?? (_goBackCommand = new DelegateCommand(OnGoBack, CanGoBack));

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="regionManager">Region manager.</param>
        /// <param name="eventAggregator">Event aggregator.</param>
        /// <param name="logger">Logger.</param>
        public PeopleViewModel(IRegionManager regionManager,
            IEventAggregator eventAggregator,
            ILogger<PeopleViewModel> logger)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _logger = logger;

            eventAggregator.GetEvent<SetPeoplePageTitleEvent>().Subscribe(SetPageTitle, ThreadOption.UIThread);
        }

        /// <summary>
        /// Can go back.
        /// </summary>
        /// <returns><c>true</c> if the user can go back, otherwise <c>false</c>.</returns>
        private bool CanGoBack()
            => _navigationService != null && _navigationService.Journal.CanGoBack;

        /// <summary>
        /// On go back.
        /// </summary>
        private void OnGoBack()
            => _navigationService.Journal.GoBack();

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
            _logger.LogInformation("PeopleViewModel: Navigated to.");
            _navigationService = _regionManager.Regions[Regions.People].NavigationService;
            _navigationService.Navigated += OnNavigated;

            RequestNavigate(PageKeys.PeopleListView);
        }

        /// <summary>
        /// On navigated from.
        /// </summary>
        /// <param name="navigationContext">Navigation context.</param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            _logger.LogInformation("PeopleViewModel: Navigated from.");
        }

        /// <summary>
        /// Sets the page title.
        /// </summary>
        /// <param name="title">Title</param>
        private void SetPageTitle(string title)
        {
            Title = title;
            GoBackCommand.RaiseCanExecuteChanged();
        }
    }
}