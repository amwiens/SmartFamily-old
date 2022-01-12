using Microsoft.Win32;

using System;

namespace SmartFamily.Core.WPF.Contracts.Services
{
    public interface IOpenFileDialogService
    {
        bool? ShowDialog(Action<OpenFileDialog> callback);
    }
}