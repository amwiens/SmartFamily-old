using CommunityToolkit.Mvvm.Messaging;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using SmartFamily.Backend.ViewModels;

using System.Linq;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SmartFamily.WinUI.Views;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
internal sealed partial class MainWindowHostPage : Page
{
    public MainViewModel ViewModel
    {
        get => (MainViewModel)DataContext;
        set => DataContext = value;
    }

    public MainWindowHostPage()
    {
        this.InitializeComponent();

        this.ViewModel = new();
    }

    private void Sidebar_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
    }

    private void MainWindowHostPage_Loaded(object sender, RoutedEventArgs e)
    {
        ViewModel.EnsureLateApplication();
    }
}