using SmartFamily.Backend.ViewModels.Pages;

namespace SmartFamily.Backend.Messages;

public sealed class NavigationFinishedMessage : ValueMessage<BasePageViewModel>
{
    public NavigationFinishedMessage(BasePageViewModel value)
        : base(value)
    {
    }
}