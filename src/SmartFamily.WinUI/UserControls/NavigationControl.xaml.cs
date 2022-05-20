using CommunityToolkit.Mvvm.Messaging;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;

using SmartFamily.Backend.Messages;
using SmartFamily.Backend.Models;
using SmartFamily.Backend.Models.Transitions;
using SmartFamily.Backend.ViewModels;
using SmartFamily.Backend.ViewModels.Pages;
using SmartFamily.Shared.Extensions;
using SmartFamily.WinUI.Helpers;
using SmartFamily.WinUI.Views;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SmartFamily.WinUI.UserControls;

public sealed partial class NavigationControl : UserControl,
    IRecipient<NavigationRequestedMessage>,
    IRecipient<RemoveDatabaseRequestedMessage>,
    IRecipient<OpenDatabaseRequestedMessage>
{
    private Dictionary<DatabaseIdModel, BasePageViewModel?> NavigationDestinations { get; }

    public NavigationControl()
    {
        this.InitializeComponent();

        NavigationDestinations = new();

        WeakReferenceMessenger.Default.Register<NavigationRequestedMessage>(this);
        WeakReferenceMessenger.Default.Register<RemoveDatabaseRequestedMessage>(this);
        WeakReferenceMessenger.Default.Register<OpenDatabaseRequestedMessage>(this);
    }

    public async void Receive(NavigationRequestedMessage message)
    {
        await Navigate(message.DatabaseViewModel, message.Value, message.Transition);
    }

    public void Receive(RemoveDatabaseRequestedMessage message)
    {
        NavigationDestinations.Remove(message.Value, out var viewModel);
        viewModel?.Dispose();

        if (NavigationDestinations.IsEmpty())
        {
            // TODO: Navigate to the start page
        }
    }

    public void Receive(OpenDatabaseRequestedMessage message)
    {
        NavigationDestinations.AddOrSet(message.Value.DatabaseIdModel);
    }

    private async Task Navigate(DatabaseIdModel databaseIdModel, TransitionModel? transition = null)
    {
        NavigationDestinations.SetAndGet(databaseIdModel, out var basePageViewModel, () => throw new InvalidOperationException("Could not navigate - insufficient parameters."));
        PageViewModel = basePageViewModel!;

        await Navigate(PageViewModel, transition);

        WeakReferenceMessenger.Default.Send(new NavigationFinishedMessage(PageViewModel));
    }

    private async Task Navigate(DatabaseViewModel databaseViewModel, BasePageViewModel? basePageViewModel, TransitionModel? transition = null)
    {
        if (basePageViewModel == null)
        {
            PageViewModel = basePageViewModel!;
        }
        else
        {
            if (!NavigationDestinations.SetAndGet(databaseViewModel.DatabaseIdModel, out _, () => basePageViewModel))
            {
                // Wasn't updated, do it manually..
                NavigationDestinations[databaseViewModel.DatabaseIdModel] = basePageViewModel;
                PageViewModel = basePageViewModel;
            }
        }

        await Navigate(PageViewModel, transition);

        WeakReferenceMessenger.Default.Send(new NavigationFinishedMessage(PageViewModel));
    }

    private async Task Navigate(BasePageViewModel basePageViewModel, TransitionModel? transition = null)
    {
        var transitionInfo = ConversionHelpers.ToNavigationTransitionInfo(transition) ?? new EntranceNavigationTransitionInfo();

        if (transition?.IsCustom ?? false)
        {
            if (transition is DrillOutTransitionModel)
            {
                DrillOutAnimationStoryboard.Begin();
                await Task.Delay(200);
                DrillOutAnimationStoryboard.Stop();
                transitionInfo = new EntranceNavigationTransitionInfo();
            }
        }

        switch (basePageViewModel)
        {
            case DatabaseDashboardPageViewModel:
                ContentFrame.Navigate(typeof(DatabaseDashboardPage), basePageViewModel, transitionInfo);
                break;

            default:
                ContentFrame.Navigate(null, null);
                break;
        }
    }

    public BasePageViewModel PageViewModel
    {
        get => (BasePageViewModel)GetValue(PageViewModelProperty);
        set => SetValue(PageViewModelProperty, value);
    }
    public static readonly DependencyProperty PageViewModelProperty =
        DependencyProperty.Register("PageViewModel", typeof(BasePageViewModel), typeof(NavigationControl), new PropertyMetadata(null));
}