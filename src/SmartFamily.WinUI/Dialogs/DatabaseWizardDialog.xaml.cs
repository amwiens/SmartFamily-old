using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.WinUI.UI.Animations;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;

using SmartFamily.Backend.Dialogs;
using SmartFamily.Backend.Enums;
using SmartFamily.Backend.Messages;
using SmartFamily.Backend.ViewModels.Dialogs;
using SmartFamily.Backend.ViewModels.Pages.DatabaseWizard;
using SmartFamily.WinUI.Helpers;
using SmartFamily.WinUI.Views.DatabaseWizard;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SmartFamily.WinUI.Dialogs;

public sealed partial class DatabaseWizardDialog : ContentDialog, IDialog<DatabaseWizardDialogViewModel>, IRecipient<DatabaseWizardNavigationRequestedMessage>
{
    private bool _hasNavigationAnimatedOnLoaded;

    private bool _isBackAnimationState;

    public DatabaseWizardDialogViewModel ViewModel
    {
        get => (DatabaseWizardDialogViewModel)DataContext;
        set => DataContext = value;
    }

    public DatabaseWizardDialog()
    {
        this.InitializeComponent();
    }

    public new async Task<DialogResult> ShowAsync() => (DialogResult)await base.ShowAsync();

    public async void Receive(DatabaseWizardNavigationRequestedMessage message)
    {
        if (message.Value is DatabaseWizardMainPageViewModel)
        {
            await NavigateAsync(message.Value, new SuppressNavigationTransitionInfo());
        }
        else
        {
            await NavigateAsync(message.Value, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }
    }

    private async Task NavigateAsync(BaseDatabaseWizardPageViewModel viewModel, NavigationTransitionInfo transition)
    {
        switch (viewModel)
        {
            case DatabaseWizardMainPageViewModel:
                ContentFrame.Navigate(typeof(DatabaseWizardMainPage), viewModel, transition);
                break;

            case AddExistingDatabasePageViewModel:
                ContentFrame.Navigate(typeof(AddExistingDatabasePage), viewModel, transition);
                break;



            case DatabaseWizardFinishPageViewModel:
                ContentFrame.Navigate(typeof(DatabaseWizardFinishPage), viewModel, transition);
                break;
        }

        await FinalizeNavigationAnimationAsync(viewModel);
    }

    private async Task FinalizeNavigationAnimationAsync(BaseDatabaseWizardPageViewModel viewModel)
    {
        switch (viewModel)
        {
            case DatabaseWizardMainPageViewModel:
                TitleText.Text = "Open new database";
                PrimaryButtonText = String.Empty;
                break;

            case AddExistingDatabasePageViewModel:
                TitleText.Text = "Add existing database";
                PrimaryButtonText = "Continue";
                break;



            case DatabaseWizardFinishPageViewModel:
                TitleText.Text = "Summary";
                PrimaryButtonText = "Close";
                SecondaryButtonText = String.Empty;
                break;
        }

        if (!_hasNavigationAnimatedOnLoaded)
        {
            _hasNavigationAnimatedOnLoaded = true;
            GoBack.Visibility = Visibility.Collapsed;
            return;
        }

        if (!_isBackAnimationState && viewModel.CanGoBack && ContentFrame.CanGoBack)
        {
            _isBackAnimationState = true;
            GoBack.Visibility = Visibility.Visible;
            await ShowBackButtonStoryboard.BeginAsync();
            ShowBackButtonStoryboard.Stop();
        }
        else if (_isBackAnimationState && !(viewModel.CanGoBack && ContentFrame.CanGoBack))
        {
            _isBackAnimationState = false;
            await HideBackButtonStoryboard.BeginAsync();
            HideBackButtonStoryboard.Stop();
            GoBack.Visibility = Visibility.Collapsed;
        }

        GoBack.Visibility = viewModel.CanGoBack && ContentFrame.CanGoBack ? Visibility.Visible : Visibility.Collapsed;
    }

    private void DatabaseWizardDialog_Loaded(object sender, RoutedEventArgs e)
    {
        ViewModel.Messenger.Register<DatabaseWizardNavigationRequestedMessage>(this);
        ViewModel.StartNavigation();
    }

    private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        var handledCallback = new HandledOrCanceledFlag(value => args.Cancel = value);
        ViewModel.PrimaryButtonClickCommand?.Execute(handledCallback);
    }

    private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        var handledCallback = new HandledOrCanceledFlag(value => args.Cancel = value);
        ViewModel.SecondaryButtonClickCommand?.Execute(handledCallback);
    }

    private async void GoBack_Click(object sender, RoutedEventArgs e)
    {
        ContentFrame.GoBack();

        if ((ContentFrame.Content as Page)?.DataContext is BaseDatabaseWizardPageViewModel viewModel)
        {
            viewModel.ReattachCommands();

            await FinalizeNavigationAnimationAsync(viewModel);
        }
    }

    private void ContentDialog_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
    {
        (ContentFrame.Content as IDisposable)?.Dispose();
    }
}