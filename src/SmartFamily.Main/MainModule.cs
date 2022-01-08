using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using SmartFamily.Core.Constants;
using SmartFamily.Main.ViewModels;
using SmartFamily.Main.Views;

namespace SmartFamily.Main
{
    public class MainModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(Regions.Main, typeof(MainPage));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainPage, MainViewModel>(PageKeys.Main);
            containerRegistry.RegisterForNavigation<DashboardPage, DashboardViewModel>(PageKeys.Dashboard);
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsViewModel>(PageKeys.Settings);
        }
    }
}