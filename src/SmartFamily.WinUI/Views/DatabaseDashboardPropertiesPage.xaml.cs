using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

using SmartFamily.Backend.ViewModels.Pages.DashboardPages;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SmartFamily.WinUI.Views;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class DatabaseDashboardPropertiesPage : Page
{
    public DatabaseDashboardPropertiesPageViewModel ViewModel
    {
        get => (DatabaseDashboardPropertiesPageViewModel)DataContext;
        set => DataContext = value;
    }

    public DatabaseDashboardPropertiesPage()
    {
        this.InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        if (e.Parameter is DatabaseDashboardPropertiesPageViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        base.OnNavigatedTo(e);
    }

    protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
    {
        ViewModel.Cleanup();
        base.OnNavigatingFrom(e);
    }
}