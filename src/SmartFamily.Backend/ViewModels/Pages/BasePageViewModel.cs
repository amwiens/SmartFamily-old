using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

using SmartFamily.Shared.Utils;

namespace SmartFamily.Backend.ViewModels.Pages;

public abstract class BasePageViewModel : ObservableObject, ICleanable, IDisposable
{
    public IMessenger Messenger { get; }

    public DatabaseViewModel DatabaseViewModel { get; }

    protected BasePageViewModel(IMessenger messenger, DatabaseViewModel databaseViewModel)
    {
        Messenger = messenger;
        DatabaseViewModel = databaseViewModel;
    }

    public virtual void Cleanup() { }

    public virtual void Dispose() { }
}