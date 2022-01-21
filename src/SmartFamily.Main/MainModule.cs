using Prism.Ioc;
using Prism.Modularity;

using SmartFamily.Core.Constants;
using SmartFamily.Main.ViewModels;
using SmartFamily.Main.Views;

namespace SmartFamily.Main
{
    /// <summary>
    /// Main module.
    /// </summary>
    public class MainModule : IModule
    {
        /// <summary>
        /// On initialized.
        /// </summary>
        /// <param name="containerProvider">Container provider.</param>
        public void OnInitialized(IContainerProvider containerProvider)
        {
            //var regionManager = containerProvider.Resolve<IRegionManager>();
            //regionManager.RegisterViewWithRegion(Regions.Main, typeof(HomePage));
        }

        /// <summary>
        /// Register types.
        /// </summary>
        /// <param name="containerRegistry">Container registry.</param>
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<DashboardPage, DashboardViewModel>(PageKeys.Dashboard);
            containerRegistry.RegisterForNavigation<FileSettingsPage, FileSettingsViewModel>(PageKeys.FileSettings);
            containerRegistry.RegisterForNavigation<HomePage, HomeViewModel>(PageKeys.Home);
            containerRegistry.RegisterForNavigation<MainPage, MainViewModel>(PageKeys.Main);
        }
    }
}