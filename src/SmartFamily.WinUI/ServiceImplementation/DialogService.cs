using Microsoft.UI.Xaml.Controls;

using SmartFamily.Backend.Dialogs;
using SmartFamily.Backend.Enums;
using SmartFamily.Backend.Services;
using SmartFamily.Backend.ViewModels.Dialogs;
using SmartFamily.WinUI.Dialogs;
using SmartFamily.WinUI.WindowViews;

using System.ComponentModel;

namespace SmartFamily.WinUI.ServiceImplementation;

internal sealed class DialogService : IDialogService
{
    private readonly Dictionary<Type, Func<ContentDialog>> _dialogs;

    public DialogService()
    {
        this._dialogs = new()
        {
            //{ typeof(SettingsDialogViewModel), () => new SettingsDialog() },
            { typeof(DatabaseWizardDialogViewModel), () => new DatabaseWizardDialog() }
        };
    }

    public IDialog<TViewModel> GetDialog<TViewModel>(TViewModel viewModel)
        where TViewModel : INotifyPropertyChanged
    {
        if (!_dialogs.TryGetValue(typeof(TViewModel), out var initializer))
        {
            throw new ArgumentException($"{typeof(TViewModel)} does not have an appropriate dialog associated with it.");
        }

        var contentDialog = initializer();

        if (contentDialog is not IDialog<TViewModel> dialog)
        {
            throw new NotSupportedException($"The dialog does not implement {typeof(IDialog<TViewModel>)}.");
        }

        dialog.ViewModel = viewModel;
        contentDialog.XamlRoot = MainWindow.Instance!.Content.XamlRoot;

        return dialog;
    }

    public Task<DialogResult> ShowDialog<TViewModel>(TViewModel viewModel)
        where TViewModel : INotifyPropertyChanged
    {
        return GetDialog<TViewModel>(viewModel).ShowAsync();
    }
}