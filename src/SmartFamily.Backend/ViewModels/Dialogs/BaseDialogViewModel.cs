using CommunityToolkit.Mvvm.ComponentModel;

using System.Windows.Input;

namespace SmartFamily.Backend.ViewModels.Dialogs;

/// <summary>
/// Serves as the base dialog view model containing reusable,
/// and optional boilerplate code for every dialog.
/// </summary>
public abstract class BaseDialogViewModel : ObservableObject
{
    private string? _title;
    public string? Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    private bool _primaryButtonEnabled;
    public bool PrimaryButtonEnabled
    {
        get => _primaryButtonEnabled;
        set => SetProperty(ref _primaryButtonEnabled, value);
    }

    private bool _secondaryButtonEnabled;
    public bool SecondaryButtonEnabled
    {
        get => _secondaryButtonEnabled;
        set => SetProperty(ref _secondaryButtonEnabled, value);
    }

    private string? _primaryButtonText;
    public string? PrimaryButtonText
    {
        get => _primaryButtonText;
        set => SetProperty(ref _primaryButtonText, value);
    }

    private string? _secondaryButtonText;
    public string? SecondaryButtonText
    {
        get => _secondaryButtonText;
        set => SetProperty(ref _secondaryButtonText, value);
    }

    private string? _closeButtonText;
    public string? CloseButtonText
    {
        get => _closeButtonText;
        set => SetProperty(ref _closeButtonText, value);
    }

    public ICommand? PrimaryButtonClickCommand { get; protected init; }

    public ICommand? SecondaryButtonClickCommand { get; protected init; }

    public ICommand? CloseButtonClickCommand { get; protected init; }
}