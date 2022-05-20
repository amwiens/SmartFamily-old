using CommunityToolkit.Mvvm.ComponentModel;

using SmartFamily.Backend.Enums;

namespace SmartFamily.Backend.ViewModels.Controls;

public class InfoBarViewModel : ObservableObject
{
    private string? _title;
    public string? Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    private string? _message;
    public string? Message
    {
        get => _message;
        set => SetProperty(ref _message, value);
    }

    private bool _isOpen;
    public bool IsOpen
    {
        get => _isOpen;
        set => SetProperty(ref _isOpen, value);
    }

    private bool _canBeClosed;
    public bool CanBeClosed
    {
        get => _canBeClosed;
        set => SetProperty(ref _canBeClosed, value);
    }

    private InfoBarSeverityType _infoBarSeverity;
    public InfoBarSeverityType InfoBarSeverity
    {
        get => _infoBarSeverity;
        set => SetProperty(ref _infoBarSeverity, value);
    }

    public InfoBarViewModel()
    {
        _canBeClosed = true;
        _infoBarSeverity = InfoBarSeverityType.Information;
    }
}