using CommunityToolkit.Mvvm.Messaging;

using SmartFamily.Backend.Enums;
using SmartFamily.Backend.Messages;
using SmartFamily.Backend.Models.Transitions;

namespace SmartFamily.Backend.ViewModels.Pages.DashboardPages;

public sealed class DatabaseDashboardPropertiesPageViewModel : BaseDashboardPageViewModel
{
    public DatabaseDashboardPropertiesPageViewModel(IMessenger messenger, DatabaseViewModel databaseViewModel)
        : base(messenger, databaseViewModel, DatabaseDashboardPageType.DashboardPropertiesPage)
    {
        base.NavigationItemViewModel = new()
        {
            Index = 1,
            NavigationAction = first => Messenger.Send(new DashboardNavigationRequestedMessage(DatabaseDashboardPageType.DashboardPropertiesPage, DatabaseViewModel) { Transition = new SuppressTransitionModel() }),
            SectionName = "Properties"
        };
    }
}