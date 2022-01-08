using Prism.Regions;

using SmartFamily.Core.Constants;

using System.Windows.Controls;

namespace SmartFamily.Main.Views
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : UserControl
    {
        public MainPage(IRegionManager regionManager)
        {
            InitializeComponent();
            RegionManager.SetRegionName(hamburgerMenuContentControl, Regions.Hamburger);
            RegionManager.SetRegionManager(hamburgerMenuContentControl, regionManager);
        }
    }
}