using SmartFamily.Backend.ViewModels;

namespace SmartFamily.Backend.Messages;

public sealed class OpenDatabaseRequestedMessage : ValueMessage<DatabaseViewModel>
{
    public OpenDatabaseRequestedMessage(DatabaseViewModel value)
        : base(value)
    {
    }
}