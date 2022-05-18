using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using SmartFamily.Backend.Messages;
using SmartFamily.Backend.Models;
using SmartFamily.Backend.Models.Transitions;
using SmartFamily.Backend.Services;
using SmartFamily.Backend.Utils;

using System.Collections.ObjectModel;

namespace SmartFamily.Backend.ViewModels.Sidebar;

public sealed class SidebarViewModel : ObservableObject, IInitializableSource<IDictionary<DatabaseIdModel, DatabaseViewModel>>, IRecipient<RemoveDatabaseRequestedMessage>, IRecipient<OpenDatabaseRequestedMessage>
{
    private readonly SearchModel<SidebarItemViewModel> _sidebarSearchModel;

    private IDialogService DialogService { get; } = Ioc.Default.GetRequiredService<IDialogService>();

    private IThreadingService ThreadingService { get; } = Ioc.Default.GetRequiredService<IThreadingService>();

    public ObservableCollection<SidebarItemViewModel> SidebarItems { get; }

    private SidebarItemViewModel? _selectedItem;
    public SidebarItemViewModel? SelectedItem
    {
        get => _selectedItem;
        set => SetProperty(ref _selectedItem, value);
    }

    private string? _searchQuery;
    public string? SearchQuery
    {
        get => _searchQuery;
        set
        {
            if (SetProperty(ref _searchQuery, value))
            {
                SearchQueryChanged(value);
            }
        }
    }

    private bool _noItemsFoundLoad;
    public bool NoItemsFoundLoad
    {
        get => _noItemsFoundLoad;
        set => SetProperty(ref _noItemsFoundLoad, value);
    }

    public IAsyncRelayCommand OpenNewDatabaseCommand { get; }

    public IAsyncRelayCommand OpenSettingsCommand { get; }

    public SidebarViewModel()
    {
        this.SidebarItems = new();
        this.OpenNewDatabaseCommand = new AsyncRelayCommand(OpenNewDatabase);
        this.OpenSettingsCommand = new AsyncRelayCommand(OpenSettings);
    }

    public void Receive(RemoveDatabaseRequestedMessage message)
    {
        var itemToRemove = SidebarItems.FirstOrDefault(item => item.DatabaseViewModel.DatabaseIdModel == message.Value);
        if (itemToRemove != null)
        {
            SidebarItems.Remove(itemToRemove);
        }
    }

    public void Receive(OpenDatabaseRequestedMessage message)
    {
        SidebarItems.Add(new(message.Value));
    }

    void IInitializableSource<IDictionary<DatabaseIdModel, DatabaseViewModel>>.Initialize(IDictionary<DatabaseIdModel, DatabaseViewModel> param)
    {
        _ = ThreadingService.ExecuteOnUiThreadAsync(() =>
        {
            foreach (var item in param.Values)
            {
                SidebarItems.Add(new(item));
            }

            if (SidebarItems.FirstOrDefault() is SidebarItemViewModel itemToSelect)
            {
                SelectedItem = itemToSelect;
                WeakReferenceMessenger.Default.Send(new NavigationRequestedMessage(itemToSelect.DatabaseViewModel) { Transition = new SuppressTransitionModel() });
            }
        });
    }

    private async Task OpenNewDatabase()
    {
        SearchQuery = string.Empty;


    }

    private async Task OpenSettings()
    {

    }

    private void SearchQueryChanged(string? query)
    {
        NoItemsFoundLoad = !_sidebarSearchModel.SubmitQuery(query);
    }
}