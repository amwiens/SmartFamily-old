using Prism.Regions;

using SmartFamily.Core.Constants;

using System.Windows.Controls;

namespace SmartFamily.People.Views
{
    /// <summary>
    /// Interaction logic for PersonView.xaml
    /// </summary>
    public partial class PersonPage : UserControl
    {
        public PersonPage(IRegionManager regionManager)
        {
            InitializeComponent();
            RegionManager.SetRegionName(personViewContentControl, Regions.Person);
            RegionManager.SetRegionManager(personViewContentControl, regionManager);
        }
    }
}