using Prism.Ioc;
using Prism.Modularity;

using SmartFamily.Core.Constants;
using SmartFamily.People.ViewModels;
using SmartFamily.People.Views;

namespace SmartFamily.People
{
    /// <summary>
    /// People module.
    /// </summary>
    public class PeopleModule : IModule
    {
        /// <summary>
        /// On initialized.
        /// </summary>
        /// <param name="containerProvider">Container provider.</param>
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        /// <summary>
        /// Register types.
        /// </summary>
        /// <param name="containerRegistry">Container registry.</param>
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<PeoplePage, PeopleViewModel>(PageKeys.People);
            containerRegistry.RegisterForNavigation<PeopleListPage, PeopleListViewModel>(PageKeys.PeopleListView);
        }
    }
}