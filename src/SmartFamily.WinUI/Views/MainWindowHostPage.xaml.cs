using CommunityToolkit.Mvvm.Messaging;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using SmartFamily.Backend.Messages;
using SmartFamily.Backend.Models.Transitions;
using SmartFamily.Backend.ViewModels;
using SmartFamily.Backend.ViewModels.Sidebar;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SmartFamily.WinUI.Views;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
internal sealed partial class MainWindowHostPage : Page, IRecipient<RemoveDatabaseRequestedMessage>
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

        WeakReferenceMessenger.Default.Register<RemoveDatabaseRequestedMessage>(this);
    }

    public void Receive(RemoveDatabaseRequestedMessage message)
    {
        ViewModel.SidebarViewModel.SelectedItem = ViewModel.SidebarViewModel.SidebarItems.FirstOrDefault();
    }

    private void Sidebar_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.SelectedItem is SidebarItemViewModel itemViewModel)
        {
            WeakReferenceMessenger.Default.Send(new NavigationRequestedMessage(itemViewModel.DatabaseViewModel) { Transition = new EntranceTransitionModel() });
        }
    }

    private void MainWindowHostPage_Loaded(object sender, RoutedEventArgs e)
    {
        ViewModel.EnsureLateApplication();
    }
}