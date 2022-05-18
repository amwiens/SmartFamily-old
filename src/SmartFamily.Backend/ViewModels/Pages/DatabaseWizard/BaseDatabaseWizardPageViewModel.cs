using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

using SmartFamily.Backend.ViewModels.Dialogs;

namespace SmartFamily.Backend.ViewModels.Pages.DatabaseWizard;

public abstract class BaseDatabaseWizardPageViewModel : ObservableObject, IDisposable
{
    public DatabaseWizardDialogViewModel DialogViewModel { get; }

    public IMessenger Messenger { get; }

    public bool CanGoBack { get; protected init; }

    protected BaseDatabaseWizardPageViewModel(IMessenger messenger, DatabaseWizardDialogViewModel dialogViewModel)
    {
        Messenger = messenger;
        DialogViewModel = dialogViewModel;
        CanGoBack = true;
    }

    public virtual void ReattachCommands() { }

    public virtual void Dispose() { }
}