using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using SmartFamily.Backend.Messages;
using SmartFamily.Backend.Services;

namespace SmartFamily.Backend.ViewModels.Sidebar;

public sealed class SidebarItemViewModel : ObservableObject
{
    private IFileExplorerService FileExplorerService { get; } = Ioc.Default.GetRequiredService<IFileExplorerService>();

    public DatabaseViewModel DatabaseViewModel { get; }

    private string? _databaseName;
    public string? DatabaseName
    {
        get => _databaseName;
        set => SetProperty(ref _databaseName, value);
    }

    private bool _canRemoveDatabase;
    public bool CanRemoveDatabase
    {
        get => _canRemoveDatabase;
        set => SetProperty(ref _canRemoveDatabase, value);
    }

    public IAsyncRelayCommand ShowInFileExplorerCommand { get; }

    public IRelayCommand RemoveDatabaseCommand { get; }

    public SidebarItemViewModel(DatabaseViewModel databaseViewModel)
    {
        DatabaseViewModel = databaseViewModel;
        _databaseName = databaseViewModel.DatabaseName;
        _canRemoveDatabase = true;

        ShowInFileExplorerCommand = new AsyncRelayCommand(ShowInFileExplorer);
        RemoveDatabaseCommand = new RelayCommand(RemoveDatabase);
    }

    private async Task ShowInFileExplorer()
    {
        // TODO: Check if exists (hide the option if doesn't)
        await FileExplorerService.OpenPathInFileExplorerAsync(DatabaseViewModel.DatabaseRootPath);
    }

    private void RemoveDatabase()
    {
        WeakReferenceMessenger.Default.Send(new RemoveDatabaseRequestedMessage(DatabaseViewModel.DatabaseIdModel));
    }
}