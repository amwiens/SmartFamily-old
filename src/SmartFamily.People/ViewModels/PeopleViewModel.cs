using Microsoft.Extensions.Logging;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

using SmartFamily.Core.Constants;

using System.Windows.Input;

namespace SmartFamily.People.ViewModels
{
    /// <summary>
    /// People view model.
    /// </summary>
    public class PeopleViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly ILogger<PeopleViewModel> _logger;

        private IRegionNavigationService _navigationService;
        private DelegateCommand _goBackCommand;
        private ICommand _peopleListViewCommand;

        public DelegateCommand GoBackCommand => _goBackCommand ?? (_goBackCommand = new DelegateCommand(OnGoBack, CanGoBack));

        public ICommand PeopleListViewCommand => _peopleListViewCommand ?? (_peopleListViewCommand = new DelegateCommand(OnPeopleListView));

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="regionManager">Region manager.</param>
        /// <param name="logger">Logger.</param>
        public PeopleViewModel(IRegionManager regionManager,
            ILogger<PeopleViewModel> logger)
        {
            _regionManager = regionManager;
            _logger = logger;
        }

        /// <summary>
        /// Can go back.
        /// </summary>
        /// <returns><c>true</c> if can go back, otherwise <c>false</c>.</returns>
        private bool CanGoBack()
            => _navigationService != null && _navigationService.Journal.CanGoBack;

        /// <summary>
        /// Go back.
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
        /// On navigated to.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event args.</param>
        private void OnNavigated(object sender, RegionNavigationEventArgs e)
            => GoBackCommand.RaiseCanExecuteChanged();

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _navigationService = _regionManager.Regions[Regions.People].NavigationService;
            _navigationService.Navigated += OnNavigated;

            RequestNavigateAndCleanJournal(PageKeys.PeopleListView);
        }

        private void OnPeopleListView()
        {
            RequestNavigateAndCleanJournal(PageKeys.PeopleListView);
            //_regionManager.RequestNavigate(Regions.People, PageKeys.PeopleListView);
        }
    }
}