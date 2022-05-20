using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

using SmartFamily.Backend.Enums;
using SmartFamily.Backend.ViewModels.Dashboard.Navigation;
using SmartFamily.Shared.Utils;

namespace SmartFamily.Backend.ViewModels.Pages.DashboardPages;

public abstract class BaseDashboardPageViewModel : ObservableObject, ICleanable, IDisposable
{
    protected IMessenger Messenger { get; }

    protected DatabaseViewModel DatabaseViewModel { get; }

    public DatabaseDashboardPageType DatabaseDashboardPageType { get; }

    public NavigationItemViewModel? NavigationItemViewModel { get; protected init; }

    protected BaseDashboardPageViewModel(IMessenger messenger, DatabaseViewModel databaseViewModel, DatabaseDashboardPageType databaseDashboardPageType)
    {
        Messenger = messenger;
        DatabaseViewModel = databaseViewModel;
        DatabaseDashboardPageType = databaseDashboardPageType;
    }

    public virtual void Cleanup() { }

    public virtual void Dispose()
    {
        DatabaseViewModel.DatabaseInstance?.Dispose();
        DatabaseViewModel.DatabaseInstance = null;
    }
}