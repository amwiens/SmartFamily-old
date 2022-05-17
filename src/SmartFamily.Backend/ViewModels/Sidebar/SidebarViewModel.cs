using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFamily.Backend.ViewModels.Sidebar;

public sealed class SidebarViewModel : ObservableObject
{

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



    private async Task OpenNewDatabase()
    {

    }

    private async Task OpenSettings()
    {

    }

    private void SearchQueryChanged(string? query)
    {
        //NoItemsFoundLoad = !_sidebarSearchModel.SubmitQuery(query);
    }
}