using CommunityToolkit.Mvvm.Messaging;

using SmartFamily.Backend.Enums;
using SmartFamily.Backend.Messages;
using SmartFamily.Backend.ViewModels;
using SmartFamily.Backend.ViewModels.Pages.DashboardPages;
using SmartFamily.Shared.Extensions;

namespace SmartFamily.Backend.Models;

public sealed class DashboardNavigationModel : IRecipient<DashboardNavigationRequestedMessage>
{
    private IMessenger Messenger { get; }

    private Dictionary<DatabaseDashboardPageType, BaseDashboardPageViewModel?> NavigationDestinations { get; }

    public DashboardNavigationModel(IMessenger messenger)
    {
        Messenger = messenger;
        NavigationDestinations = new();
    }

    private BaseDashboardPageViewModel? NavigateToPage(DatabaseDashboardPageType databaseDashboardPageType, DatabaseViewModel databaseViewModel)
    {
        BaseDashboardPageViewModel? baseDashboardPageViewModel;
        switch (databaseDashboardPageType)
        {
            case DatabaseDashboardPageType.MainDashboardPage:
                NavigationDestinations.SetAndGet(databaseDashboardPageType, out baseDashboardPageViewModel, () => new DatabaseMainDashboardPageViewModel(Messenger, databaseViewModel));
                break;

            case DatabaseDashboardPageType.DashboardPropertiesPage:
                NavigationDestinations.SetAndGet(databaseDashboardPageType, out baseDashboardPageViewModel, () => new DatabaseDashboardPropertiesPageViewModel(Messenger, databaseViewModel));
                break;

            case DatabaseDashboardPageType.Undefined:
            default:
                throw new ArgumentOutOfRangeException(nameof(databaseDashboardPageType));
        }

        return baseDashboardPageViewModel;
    }

    private void NavigateToPage(DatabaseDashboardPageType databaseDashboardPageType, BaseDashboardPageViewModel baseDashboardPageViewModel)
    {
        if (!NavigationDestinations.SetAndGet(databaseDashboardPageType, out _, () => baseDashboardPageViewModel))
        {
            // Wasn't updated, do it manually...
            NavigationDestinations[databaseDashboardPageType] = baseDashboardPageViewModel;
        }
    }

    public void Receive(DashboardNavigationRequestedMessage message)
    {
        BaseDashboardPageViewModel? baseDashboardPageViewModel;
        if (message.Value == null)
        {
            baseDashboardPageViewModel = NavigateToPage(message.DatabaseDashboardPageType, message.DatabaseViewModel);
        }
        else
        {
            NavigateToPage(message.DatabaseDashboardPageType, message.Value);
            baseDashboardPageViewModel = message.Value;
        }

        Messenger.Send(new DashboardNavigationFinishedMessage(baseDashboardPageViewModel!) { Transition = message.Transition });
    }
}