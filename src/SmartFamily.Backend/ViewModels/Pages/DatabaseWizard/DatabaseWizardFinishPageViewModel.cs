using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using SmartFamily.Backend.Messages;
using SmartFamily.Backend.Utils;
using SmartFamily.Backend.ViewModels.Dialogs;

namespace SmartFamily.Backend.ViewModels.Pages.DatabaseWizard;

public sealed class DatabaseWizardFinishPageViewModel : BaseDatabaseWizardPageViewModel
{
    public string DatabaseName { get; }

    public DatabaseWizardFinishPageViewModel(IMessenger messenger, DatabaseWizardDialogViewModel dialogViewModel)
        : base(messenger, dialogViewModel)
    {
        DatabaseName = DialogViewModel.DatabaseViewModel!.DatabaseName;
        base.CanGoBack = false;

        DialogViewModel.PrimaryButtonEnabled = true;
        DialogViewModel.PrimaryButtonClickCommand = new RelayCommand<IHandledFlag?>(_ => { }); // Override the previous action

        WeakReferenceMessenger.Default.Send(new OpenDatabaseRequestedMessage(DialogViewModel.DatabaseViewModel));
    }
}