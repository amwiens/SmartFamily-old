﻿using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

using SmartFamily.Backend.ViewModels.Pages.SettingsDialog;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SmartFamily.WinUI.Views.Settings;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class GeneralSettingsPage : Page
{
    public GeneralSettingsPageViewModel ViewModel
    {
        get => (GeneralSettingsPageViewModel)DataContext;
        set => DataContext = value;
    }

    public GeneralSettingsPage()
    {
        this.InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        if (e.Parameter is GeneralSettingsPageViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        base.OnNavigatedTo(e);
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        _ = ViewModel.ConfigureUpdates();
    }
}