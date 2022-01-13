using Prism.Regions;

using SmartFamily.Core.Constants;

using System.Windows.Controls;

namespace SmartFamily.People.Views
{
    /// <summary>
    /// Interaction logic for PeoplePage.xaml
    /// </summary>
    public partial class PeoplePage : UserControl
    {
        public PeoplePage(IRegionManager regionManager)
        {
            InitializeComponent();
            RegionManager.SetRegionName(peopleViewContentControl, Regions.People);
            RegionManager.SetRegionManager(peopleViewContentControl, regionManager);
        }
    }
}