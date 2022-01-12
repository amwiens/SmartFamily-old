using MahApps.Metro.Controls;

using Prism.Regions;

using SmartFamily.Contracts.Services;
using SmartFamily.Core.Constants;

namespace SmartFamily.Views
{
    /// <summary>
    /// Interaction logic for ShellWindow.xaml
    /// </summary>
    public partial class ShellWindow : MetroWindow
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="regionManager">Region manager.</param>
        /// <param name="rightPaneService">Right pane service.</param>
        public ShellWindow(IRegionManager regionManager, IRightPaneService rightPaneService)
        {
            InitializeComponent();
            RegionManager.SetRegionName(menuContentControl, Regions.Main);
            RegionManager.SetRegionManager(menuContentControl, regionManager);
            rightPaneService.Initialize(splitView, rightPaneContentControl);
        }
    }
}