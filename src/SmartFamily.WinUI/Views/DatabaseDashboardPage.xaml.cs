using CommunityToolkit.Mvvm.Messaging;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;

using SmartFamily.Backend.Messages;
using SmartFamily.Backend.Models.Transitions;
using SmartFamily.Backend.ViewModels.Dashboard.Navigation;
using SmartFamily.Backend.ViewModels.Pages;
using SmartFamily.Backend.ViewModels.Pages.DashboardPages;
using SmartFamily.Shared.Utils;
using SmartFamily.WinUI.Helpers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SmartFamily.WinUI.Views;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class DatabaseDashboardPage : Page, IRecipient<DashboardNavigationFinishedMessage>
{
    public DatabaseDashboardPageViewModel ViewModel
    {
        get => (DatabaseDashboardPageViewModel)DataContext;
        set => DataContext = value;
    }

    public DatabaseDashboardPage()
    {
        this.InitializeComponent();
    }

    public void Receive(DashboardNavigationFinishedMessage message)
    {
        NavigatePage(message.Value, message.Transition);
    }

    private void NavigatePage(BaseDashboardPageViewModel baseDashboardPageViewModel, TransitionModel? transition = null)
    {
        var transitionInfo = ConversionHelpers.ToNavigationTransitionInfo(transition);
        switch (baseDashboardPageViewModel)
        {
            case DatabaseMainDashboardPageViewModel:
                ContentFrame.Navigate(typeof(DatabaseMainDashboardPage), baseDashboardPageViewModel, transitionInfo ?? new SuppressNavigationTransitionInfo());
                break;

            case DatabaseDashboardPropertiesPageViewModel:
                ContentFrame.Navigate(typeof(DatabaseDashboardPropertiesPage), baseDashboardPageViewModel, transitionInfo ?? new SuppressNavigationTransitionInfo());
                break;
        }
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        if (e.Parameter is DatabaseDashboardPageViewModel viewModel)
        {
            ViewModel = viewModel;
            ViewModel.Messenger.Register<DashboardNavigationFinishedMessage>(this);
            ViewModel.StartNavigation();
        }

        base.OnNavigatedTo(e);
    }

    protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
    {
        (ViewModel as ICleanable)?.Cleanup();

        ViewModel.Messenger.Unregister<DashboardNavigationFinishedMessage>(this);

        base.OnNavigatingFrom(e);
    }

    private void BreadcrumbBar_ItemClicked(BreadcrumbBar sender, BreadcrumbBarItemClickedEventArgs args)
    {
        if (args.Item is NavigationItemViewModel itemViewModel)
        {
            itemViewModel.NavigationAction?.Invoke(ViewModel.NavigationBreadcrumbViewModel.DashboardNavigationItems.FirstOrDefault());
        }
    }
}