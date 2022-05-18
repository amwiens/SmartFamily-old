using SmartFamily.Backend.Enums;

using System.ComponentModel;

namespace SmartFamily.Backend.Dialogs;

public interface IDialog<TViewModel>
    where TViewModel : INotifyPropertyChanged
{
    TViewModel ViewModel { get; set; }

    Task<DialogResult> ShowAsync();

    void Hide();
}