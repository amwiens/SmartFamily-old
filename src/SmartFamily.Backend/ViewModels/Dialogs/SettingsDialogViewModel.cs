using CommunityToolkit.Mvvm.Messaging;

namespace SmartFamily.Backend.ViewModels.Dialogs;

public sealed class SettingsDialogViewModel : BaseDialogViewModel
{
    public IMessenger Messenger { get; }

    public SettingsDialogViewModel()
    {
        this.Messenger = new WeakReferenceMessenger();
    }
}