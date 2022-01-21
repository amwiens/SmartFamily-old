using Prism.Ioc;
using Prism.Modularity;

using SmartFamily.Core.Constants;
using SmartFamily.Places.ViewModels;
using SmartFamily.Places.Views;

namespace SmartFamily.Places
{
    /// <summary>
    /// Places module
    /// </summary>
    public class PlacesModule : IModule
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
            containerRegistry.RegisterForNavigation<PlacesPage, PlacesViewModel>(PageKeys.Places);
        }
    }
}