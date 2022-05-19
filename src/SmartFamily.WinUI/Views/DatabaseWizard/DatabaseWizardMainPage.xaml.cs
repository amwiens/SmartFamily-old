using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

using SmartFamily.Backend.ViewModels.Pages.DatabaseWizard;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SmartFamily.WinUI.Views.DatabaseWizard;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class DatabaseWizardMainPage : Page, IDisposable
{
    public DatabaseWizardMainPageViewModel ViewModel
    {
        get => (DatabaseWizardMainPageViewModel)DataContext;
        set => DataContext = value;
    }

    public DatabaseWizardMainPage()
    {
        this.InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        if (e.Parameter is DatabaseWizardMainPageViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        base.OnNavigatedTo(e);
    }

    public void Dispose()
    {
        ViewModel.Dispose();
    }
}