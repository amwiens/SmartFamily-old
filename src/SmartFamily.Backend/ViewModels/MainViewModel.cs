using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;

using SmartFamily.Backend.ViewModels.Pages;
using SmartFamily.Backend.ViewModels.Sidebar;

namespace SmartFamily.Backend.ViewModels;

public sealed class MainViewModel : ObservableObject
{


    public SidebarViewModel SidebarViewModel { get; }


    private BasePageViewModel? _activePageViewModel;
    public BasePageViewModel? ActivePageViewModel
    {
        get => _activePageViewModel;
        set => SetProperty(ref _activePageViewModel, value);
    }

    public MainViewModel()
    {
        SidebarViewModel = new();
    }

    public void EnsureLateApplication()
    {
    }
}