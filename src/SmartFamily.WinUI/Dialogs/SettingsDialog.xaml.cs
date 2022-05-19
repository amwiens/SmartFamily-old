using CommunityToolkit.Mvvm.Messaging;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;

using SmartFamily.Backend.Dialogs;
using SmartFamily.Backend.Enums;
using SmartFamily.Backend.Messages;
using SmartFamily.Backend.ViewModels.Dialogs;
using SmartFamily.Backend.ViewModels.Pages.SettingsDialog;
using SmartFamily.WinUI.Views.Settings;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SmartFamily.WinUI.Dialogs;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SettingsDialog : ContentDialog, IDialog<SettingsDialogViewModel>, IRecipient<SettingsNavigationRequestedMessage>
{
    public SettingsDialogViewModel ViewModel
    {
        get => (SettingsDialogViewModel)DataContext;
        set => DataContext = value;
    }

    public SettingsDialog()
    {
        this.InitializeComponent();
    }

    public new async Task<DialogResult> ShowAsync() => (DialogResult)await base.ShowAsync();

    public void Receive(SettingsNavigationRequestedMessage message)
    {
        Navigate(message.Value);
    }

    private void Navigate(BaseSettingsDialogPageViewModel viewModel)
    {
        switch (viewModel)
        {


            case AboutSettingsPageViewModel:
                ContentFrame.Navigate(typeof(AboutSettingsPage), viewModel, new EntranceNavigationTransitionInfo());
                break;
        }
    }

    private void SettingsDialog_Loaded(object sender, RoutedEventArgs e)
    {
        ViewModel.Messenger.Register<SettingsNavigationRequestedMessage>(this);
    }

    private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        var tag = Convert.ToInt32((args.SelectedItem as NavigationViewItem)?.Tag);

        switch (tag)
        {
            default:
            case 0:
                //Navigate(new GeneralSettingsPageViewModel());
                break;

            case 1:
                //Navigate(new PreferencesSettingsPageViewModel());
                break;

            case 2:
                Navigate(new AboutSettingsPageViewModel());
                break;
        }
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Hide();
    }
}