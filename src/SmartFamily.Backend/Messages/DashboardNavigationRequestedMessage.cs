using SmartFamily.Backend.Enums;
using SmartFamily.Backend.Models.Transitions;
using SmartFamily.Backend.ViewModels;
using SmartFamily.Backend.ViewModels.Pages.DashboardPages;

namespace SmartFamily.Backend.Messages;

public sealed class DashboardNavigationRequestedMessage : ValueMessage<BaseDashboardPageViewModel>
{
    public TransitionModel? Transition { get; init; }

    public DatabaseDashboardPageType DatabaseDashboardPageType { get; }

    public DatabaseViewModel DatabaseViewModel { get; }

    public DashboardNavigationRequestedMessage(DatabaseDashboardPageType databaseDashboardPageType, DatabaseViewModel databaseViewModel)
        : this(databaseDashboardPageType, databaseViewModel, null)
    {
    }

    public DashboardNavigationRequestedMessage(DatabaseDashboardPageType databaseDashboardPageType, DatabaseViewModel databaseViewModel, BaseDashboardPageViewModel? value)
        : base(value)
    {
        DatabaseDashboardPageType = databaseDashboardPageType;
        DatabaseViewModel = databaseViewModel;
    }
}