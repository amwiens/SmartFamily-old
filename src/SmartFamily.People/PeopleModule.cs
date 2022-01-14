using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using SmartFamily.Core.Constants;
using SmartFamily.People.ViewModels;
using SmartFamily.People.Views;

namespace SmartFamily.People
{
    public class PeopleModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            //regionManager.RegisterViewWithRegion(Regions.People, PageKeys.PeopleListView);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<PeoplePage, PeopleViewModel>(PageKeys.People);
            containerRegistry.RegisterForNavigation<PeopleListPage, PeopleListViewModel>(PageKeys.PeopleListView);
        }
    }
}