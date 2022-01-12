namespace SmartFamily.Core.WPF.Contracts.Services
{
    /// <summary>
    /// Select folder dialog service.
    /// </summary>
    public interface ISelectFolderDialogService
    {
        /// <summary>
        /// Show select folder dialog.
        /// </summary>
        /// <param name="folder">Folder name</param>
        /// <returns><c>true</c> if a folder was selected, otherwise <c>false</c>.</returns>
        bool? ShowDialog(out string folder);
    }
}