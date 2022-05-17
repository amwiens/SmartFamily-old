using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SmartFamily.Backend.ViewModels.Sidebar;

public sealed class SidebarItemViewModel : ObservableObject
{


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

    }

    private void RemoveDatabase()
    {

    }
}