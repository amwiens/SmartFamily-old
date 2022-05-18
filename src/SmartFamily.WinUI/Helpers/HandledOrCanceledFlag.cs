using SmartFamily.Backend.Utils;

namespace SmartFamily.WinUI.Helpers;

internal sealed class HandledOrCanceledFlag : IHandledFlag, ICanceledFlag
{
    private readonly Action<bool> _flagCallback;

    public HandledOrCanceledFlag(Action<bool> flagCallback)
    {
        _flagCallback = flagCallback;
    }

    public void Cancel()
    {
        Handle();
    }

    public void Handle()
    {
        Handle(true);
    }

    public void Handle(bool value)
    {
        _flagCallback(value);
    }
}