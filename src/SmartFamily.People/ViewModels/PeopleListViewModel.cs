using Microsoft.Extensions.Logging;

using Prism.Mvvm;
using Prism.Regions;

using SmartFamily.Core.Models;
using SmartFamily.EntityFramework.Contracts.Services;

using System.Collections.ObjectModel;

namespace SmartFamily.People.ViewModels
{
    internal class PeopleListViewModel : BindableBase, INavigationAware
    {
        private readonly ILogger<PeopleListViewModel> _logger;
        private readonly ISampleDataService _sampleDataService;

        public ObservableCollection<SamplePerson> Source { get; } = new ObservableCollection<SamplePerson>();

        public PeopleListViewModel(ISampleDataService sampleDataService,
            ILogger<PeopleListViewModel> logger)
        {
            _sampleDataService = sampleDataService;
            _logger = logger;
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            Source.Clear();

            // Replace this with your actual data
            var data = await _sampleDataService.GetGridDataAsync();

            foreach (var item in data)
            {
                Source.Add(item);
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
            => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}