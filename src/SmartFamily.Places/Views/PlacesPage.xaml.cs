using Prism.Regions;

using SmartFamily.Core.Constants;

using System.Windows.Controls;

namespace SmartFamily.Places.Views
{
    /// <summary>
    /// Interaction logic for PlacesPage.xaml
    /// </summary>
    public partial class PlacesPage : UserControl
    {
        public PlacesPage(IRegionManager regionManager)
        {
            InitializeComponent();
            RegionManager.SetRegionName(placesViewContentControl, Regions.Places);
            RegionManager.SetRegionManager(placesViewContentControl, regionManager);
        }
    }
}