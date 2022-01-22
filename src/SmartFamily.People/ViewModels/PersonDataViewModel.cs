using Microsoft.Extensions.Logging;

using Prism.Mvvm;
using Prism.Regions;

using SmartFamily.Core.Constants;
using SmartFamily.Core.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFamily.People.ViewModels
{
    /// <summary>
    /// Person data view model.
    /// </summary>
    public class PersonDataViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly ILogger<PersonDataViewModel> _logger;

        private IRegionNavigationService _navigationService;
        private SamplePerson _person;

        /// <summary>
        /// Person
        /// </summary>
        public SamplePerson Person
        {
            get => _person;
            set => SetProperty(ref _person, value);
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="regionManager">Region manager.</param>
        /// <param name="logger">Logger.</param>
        public PersonDataViewModel(IRegionManager regionManager,
            ILogger<PersonDataViewModel> logger)
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
            _logger.LogInformation("PersonViewModel: Navigated to.");
            _navigationService = _regionManager.Regions[Regions.Person].NavigationService;
            _navigationService.Navigated += OnNavigated;

            Person = navigationContext.Parameters.GetValue<SamplePerson>("Person");
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