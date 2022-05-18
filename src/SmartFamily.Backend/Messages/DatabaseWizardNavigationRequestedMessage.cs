using SmartFamily.Backend.ViewModels.Pages.DatabaseWizard;

namespace SmartFamily.Backend.Messages;

public sealed class DatabaseWizardNavigationRequestedMessage : ValueMessage<BaseDatabaseWizardPageViewModel>
{
    public DatabaseWizardNavigationRequestedMessage(BaseDatabaseWizardPageViewModel value)
        : base(value)
    {
    }
}