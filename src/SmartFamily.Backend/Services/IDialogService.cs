using SmartFamily.Backend.Dialogs;
using SmartFamily.Backend.Enums;

using System.ComponentModel;

namespace SmartFamily.Backend.Services;

public interface IDialogService
{
    IDialog<TViewModel> GetDialog<TViewModel>(TViewModel viewModel) where TViewModel : INotifyPropertyChanged;

    Task<DialogResult> ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : INotifyPropertyChanged;
}