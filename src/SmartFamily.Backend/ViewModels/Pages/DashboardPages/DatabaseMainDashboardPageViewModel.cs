using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using SmartFamily.Backend.Enums;
using SmartFamily.Backend.Messages;
using SmartFamily.Backend.Models.Transitions;
using SmartFamily.Backend.Services;

namespace SmartFamily.Backend.ViewModels.Pages.DashboardPages;

public sealed class DatabaseMainDashboardPageViewModel : BaseDashboardPageViewModel
{
    private IFileExplorerService FileExplorerService { get; } = Ioc.Default.GetRequiredService<IFileExplorerService>();

    public IRelayCommand OpenDatabasePropertiesCommand { get; }

    public DatabaseMainDashboardPageViewModel(IMessenger messenger, DatabaseViewModel databaseViewModel)
        : base(messenger, databaseViewModel, DatabaseDashboardPageType.MainDashboardPage)
    {
        base.NavigationItemViewModel = new()
        {
            Index = 0,
            NavigationAction = first => Messenger.Send(new DashboardNavigationRequestedMessage(DatabaseDashboardPageType.MainDashboardPage, DatabaseViewModel) { Transition = new SlideTransitionModel(SlideTransitionDirection.ToRight) }),
            SectionName = databaseViewModel.DatabaseName
        };

        OpenDatabasePropertiesCommand = new RelayCommand(OpenDatabaseProperties);
    }

    private void OpenDatabaseProperties()
    {
        Messenger.Send(new DashboardNavigationRequestedMessage(DatabaseDashboardPageType.DashboardPropertiesPage, DatabaseViewModel) { Transition = new SlideTransitionModel(SlideTransitionDirection.ToLeft) });
    }
}