using CommunityToolkit.Mvvm.Messaging;

using SmartFamily.Backend.Enums;
using SmartFamily.Backend.Messages;
using SmartFamily.Backend.Models;
using SmartFamily.Backend.Models.Transitions;
using SmartFamily.Backend.ViewModels.Dashboard.Navigation;
using SmartFamily.Backend.ViewModels.Pages.DashboardPages;

namespace SmartFamily.Backend.ViewModels.Pages;

public sealed class DatabaseDashboardPageViewModel : BasePageViewModel, IRecipient<DashboardNavigationFinishedMessage>
{
    public NavigationBreadcrumbViewModel NavigationBreadcrumbViewModel { get; }

    public DashboardNavigationModel DashboardNavigationModel { get; }

    public BaseDashboardPageViewModel? CurrentPage { get; private set; }

    public DatabaseDashboardPageViewModel(IMessenger messenger, DatabaseViewModel dabaseViewModel)
        : base(messenger, dabaseViewModel)
    {
        NavigationBreadcrumbViewModel = new();
        DashboardNavigationModel = new(Messenger);

        Messenger.Register<DashboardNavigationFinishedMessage>(this);
        Messenger.Register<DashboardNavigationFinishedMessage>(NavigationBreadcrumbViewModel);
        Messenger.Register<DashboardNavigationRequestedMessage>(DashboardNavigationModel);
    }

    public void InitializeWithRoutine() //IFinalizedDatabaseLoadRoutine finalizedDatabaseLoadRoutine)
    {
        //if (CurrentPage is DatabaseMainDashboardPageViewModel viewModel)
        //{
        //    finalizedDatabaseLoadRoutine = finalizedDatabaseLoadRoutine.ContinueWithOptionalRoutine()
        //        .EstablishOptionalRoutine()
        //        .Finalize();

        //    DatabaseViewModel.DatabaseInstance = finalizedDatabaseLoadRoutine.Deploy();
        //    AsyncExtensions.RunAndForget(() =>
        //    {
        //        DatabaseViewModel.DatabaseInstance
        //    });
        //}
    }

    public void StartNavigation()
    {
        Messenger.Send(new DashboardNavigationRequestedMessage(CurrentPage?.DatabaseDashboardPageType ?? DatabaseDashboardPageType.MainDashboardPage, DatabaseViewModel, CurrentPage)
        {
            Transition = CurrentPage == null ? new ContinuumTransitionModel() : new SuppressTransitionModel()
        });
    }

    public void Receive(DashboardNavigationFinishedMessage message)
    {
        CurrentPage = message.Value;
    }

    public override void Cleanup()
    {
        CurrentPage?.Cleanup();
        base.Cleanup();
    }

    public override void Dispose()
    {
        CurrentPage?.Dispose();
        base.Dispose();
    }
}