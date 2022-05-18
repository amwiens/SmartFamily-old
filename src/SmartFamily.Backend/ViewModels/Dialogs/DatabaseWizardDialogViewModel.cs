using CommunityToolkit.Mvvm.Messaging;

using SmartFamily.Backend.Messages;
using SmartFamily.Backend.ViewModels.Pages.DatabaseWizard;

using System.Windows.Input;

namespace SmartFamily.Backend.ViewModels.Dialogs;

public sealed class DatabaseWizardDialogViewModel : BaseDialogViewModel
{
    public IMessenger Messenger { get; }

    public DatabaseViewModel? DatabaseViewModel { get; set; }

    public new ICommand? PrimaryButtonClickCommand { get; set; }

    public new ICommand? SecondaryButtonClickCommand { get; set; }

    public DatabaseWizardDialogViewModel()
    {
        Messenger = new WeakReferenceMessenger();
    }

    public void StartNavigation()
    {
        Messenger.Send(new DatabaseWizardNavigationRequestedMessage(new DatabaseWizardMainPageViewModel(Messenger, this)));
    }
}