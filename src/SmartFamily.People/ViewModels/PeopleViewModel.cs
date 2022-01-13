using Microsoft.Extensions.Logging;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

using SmartFamily.Core.Constants;

using System.Windows.Input;

namespace SmartFamily.People.ViewModels
{
    public class PeopleViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly ILogger<PeopleViewModel> _logger;

        private IRegionNavigationService _navigationService;
        private ICommand _peopleListViewCommand;


        public ICommand PeopleListViewCommand => _peopleListViewCommand ?? (_peopleListViewCommand = new DelegateCommand(OnPeopleListView));

        public PeopleViewModel(IRegionManager regionManager,
            ILogger<PeopleViewModel> logger)
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
        /// On navigated to.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event args.</param>
        private void OnNavigated(object sender, RegionNavigationEventArgs e)
        {
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {

        }

        private void OnPeopleListView()
        {
            _regionManager.RequestNavigate(Regions.People, PageKeys.PeopleListView);
        }
    }
}