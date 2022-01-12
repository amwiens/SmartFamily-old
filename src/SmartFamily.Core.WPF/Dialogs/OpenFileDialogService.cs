using Microsoft.Win32;

using SmartFamily.Core.WPF.Contracts.Services;

using System;

namespace SmartFamily.Core.WPF.Dialogs
{
    public class OpenFileDialogService : IOpenFileDialogService
    {
        private readonly OpenFileDialog _openFileDialog = new OpenFileDialog();

        public bool? ShowDialog(Action<OpenFileDialog> callback)
        {
            _openFileDialog.Filter = "SmartFamily database (*.sfdb)|*.sfdb|All files (*.*)|*.*";
            var openFile = _openFileDialog.ShowDialog();
            return openFile;
        }
    }
}