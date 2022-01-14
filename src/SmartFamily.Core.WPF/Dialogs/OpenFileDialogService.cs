using Microsoft.Win32;

using SmartFamily.Core.WPF.Contracts.Services;

namespace SmartFamily.Core.WPF.Dialogs
{
    /// <summary>
    /// Open file dialog service.
    /// </summary>
    public class OpenFileDialogService : IOpenFileDialogService
    {
        private readonly OpenFileDialog _openFileDialog = new();

        /// <inheritdoc/>
        public bool? ShowOpenDatabaseDialog(out string fileName)
        {
            fileName = string.Empty;
            _openFileDialog.Filter = "SmartFamily database (*.sfdb)|*.sfdb|All files (*.*)|*.*";
            _openFileDialog.Multiselect = false;
            var openFile = _openFileDialog.ShowDialog();
            if (_openFileDialog.FileName != null)
            {
                fileName = _openFileDialog.FileName;
            }
            return openFile;
        }
    }
}