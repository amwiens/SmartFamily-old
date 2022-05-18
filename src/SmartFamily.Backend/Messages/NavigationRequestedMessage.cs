using SmartFamily.Backend.Models.Transitions;
using SmartFamily.Backend.ViewModels;
using SmartFamily.Backend.ViewModels.Pages;

namespace SmartFamily.Backend.Messages;

public sealed class NavigationRequestedMessage : ValueMessage<BasePageViewModel?>
{
    public TransitionModel? Transition { get; init; }

    public DatabaseViewModel DatabaseViewModel { get; }

    public NavigationRequestedMessage(DatabaseViewModel databaseModel, BasePageViewModel? value = null)
        : base(value)
    {
        DatabaseViewModel = databaseModel;
    }
}