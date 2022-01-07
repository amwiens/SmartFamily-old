using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using SmartFamily.Core;
using SmartFamily.Main.Views;

namespace SmartFamily.Main
{
    public class MainModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(DashboardView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.RegisterForNavigation<DashboardView>();
        }
    }
}