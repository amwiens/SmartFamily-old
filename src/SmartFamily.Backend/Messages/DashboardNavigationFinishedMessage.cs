using SmartFamily.Backend.Models.Transitions;
using SmartFamily.Backend.ViewModels.Pages.DashboardPages;

namespace SmartFamily.Backend.Messages;

public sealed class DashboardNavigationFinishedMessage : ValueMessage<BaseDashboardPageViewModel>
{
    public TransitionModel? Transition { get; init; }

    public DashboardNavigationFinishedMessage(BaseDashboardPageViewModel value)
        : base(value)
    {
    }
}