using SmartFamily.Backend.ViewModels.Pages.SettingsDialog;

namespace SmartFamily.Backend.Messages;

public sealed class SettingsNavigationRequestedMessage : ValueMessage<BaseSettingsDialogPageViewModel>
{
    public SettingsNavigationRequestedMessage(BaseSettingsDialogPageViewModel value)
        : base(value)
    {
    }
}