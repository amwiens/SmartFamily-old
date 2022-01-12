using SmartFamily.Core.WPF.Contracts.Services;

using System.Windows.Forms;

namespace SmartFamily.Core.WPF.Dialogs
{
    /// <summary>
    /// Select folder dialog service.
    /// </summary>
    public class SelectFolderDialogService : ISelectFolderDialogService
    {
        private readonly FolderBrowserDialog _folderBrowserDialog = new();

        /// <inheritdoc/>
        public bool? ShowDialog(out string folder)
        {
            folder = string.Empty;
            var result = _folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                folder = _folderBrowserDialog.SelectedPath;
                return true;
            }
            return false;
        }
    }
}