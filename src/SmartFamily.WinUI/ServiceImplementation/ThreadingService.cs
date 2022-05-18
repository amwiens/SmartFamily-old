using CommunityToolkit.WinUI;

using Microsoft.UI.Dispatching;

using SmartFamily.Backend.Services;

namespace SmartFamily.WinUI.ServiceImplementation;

internal sealed class ThreadingService : IThreadingService
{
    private readonly DispatcherQueue _dispatcherQueue;

    public ThreadingService()
    {
        _dispatcherQueue = DispatcherQueue.GetForCurrentThread();
    }

    public Task ExecuteOnUiThreadAsync(Action action)
    {
        return _dispatcherQueue.EnqueueAsync(action);
    }

    public Task<TResult?> ExecuteOnUiThreadAsync<TResult>(Func<TResult?> func)
    {
        return _dispatcherQueue.EnqueueAsync<TResult?>(func);
    }
}