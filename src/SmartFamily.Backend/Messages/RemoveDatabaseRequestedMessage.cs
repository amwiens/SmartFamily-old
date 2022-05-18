using SmartFamily.Backend.Models;

namespace SmartFamily.Backend.Messages;

public sealed class RemoveDatabaseRequestedMessage : ValueMessage<DatabaseIdModel>
{
    public RemoveDatabaseRequestedMessage(DatabaseIdModel value)
        : base(value)
    {
    }
}