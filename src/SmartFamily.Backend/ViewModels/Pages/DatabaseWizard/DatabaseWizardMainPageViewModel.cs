using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using SmartFamily.Backend.Messages;
using SmartFamily.Backend.ViewModels.Dialogs;

namespace SmartFamily.Backend.ViewModels.Pages.DatabaseWizard;

public sealed class DatabaseWizardMainPageViewModel : BaseDatabaseWizardPageViewModel
{
    public IRelayCommand AddExistingDatabaseCommand { get; }

    public IRelayCommand CreateNewDatabaseCommand { get; }

    public DatabaseWizardMainPageViewModel(IMessenger messenger, DatabaseWizardDialogViewModel dialogViewModel)
        : base(messenger, dialogViewModel)
    {
        AddExistingDatabaseCommand = new RelayCommand(AddExistingDatabase);
        CreateNewDatabaseCommand = new RelayCommand(CreateNewDatabase);
    }

    private void AddExistingDatabase()
    {
        Messenger.Send(new DatabaseWizardNavigationRequestedMessage(new AddExistingDatabasePageViewModel(Messenger, DialogViewModel)));
    }

    private void CreateNewDatabase()
    {

    }
}