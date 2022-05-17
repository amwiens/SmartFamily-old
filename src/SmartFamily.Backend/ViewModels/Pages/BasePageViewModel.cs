using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace SmartFamily.Backend.ViewModels.Pages;

public abstract class BasePageViewModel : ObservableObject, IDisposable
{
    public IMessenger Messenger { get; }



    protected BasePageViewModel(IMessenger messenger)
    {
        Messenger = messenger;
    }

    public virtual void Cleanup() { }

    public virtual void Dispose() { }
}