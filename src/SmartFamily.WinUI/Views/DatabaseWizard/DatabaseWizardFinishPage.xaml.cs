using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

using SmartFamily.Backend.ViewModels.Pages.DatabaseWizard;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SmartFamily.WinUI.Views.DatabaseWizard;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class DatabaseWizardFinishPage : Page, IDisposable
{
    public DatabaseWizardFinishPageViewModel ViewModel
    {
        get => (DatabaseWizardFinishPageViewModel)DataContext;
        set => DataContext = value;
    }

    public DatabaseWizardFinishPage()
    {
        this.InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        if (e.Parameter is DatabaseWizardFinishPageViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        base.OnNavigatedTo(e);
    }

    private async void VisualPlayer_Loaded(object sender, RoutedEventArgs e)
    {
        CheckVisualSource.SetColorProperty("Foreground", ((SolidColorBrush)Application.Current.Resources["SolidBackgroundFillColorBaseBrush"]).Color);
        VisualPlayer.Visibility = Visibility.Collapsed;
        await Task.Delay(600);
        _ = VisualPlayer.PlayAsync(CheckVisualSource.Markers["NormalOffToNormalOn_Start"], CheckVisualSource.Markers["NormalOffToNormalOn_End"], false);
        await Task.Delay(20);
        VisualPlayer.Visibility = Visibility.Visible;
    }

    public void Dispose()
    {

        ViewModel.Dispose();
    }
}