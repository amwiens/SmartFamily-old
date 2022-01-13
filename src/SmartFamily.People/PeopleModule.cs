using Prism.Ioc;
using Prism.Modularity;

using SmartFamily.Core.Constants;
using SmartFamily.People.ViewModels;
using SmartFamily.People.Views;

using System;

namespace SmartFamily.People
{
    public class PeopleModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<PeoplePage, PeopleViewModel>(PageKeys.People);
        }
    }
}
