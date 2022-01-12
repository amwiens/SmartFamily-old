namespace SmartFamily.Core.WPF.Contracts.Services
{
    public interface IOpenFileDialogService
    {
        /// <summary>
        /// Opens the open database file dialog window.
        /// </summary>
        /// <param name="fileName">File name to pass back to open.</param>
        /// <returns><c>true</c> if file is selected, otherwise <c>false</c>.</returns>
        bool? ShowOpenDatabaseDialog(out string fileName);
    }
}