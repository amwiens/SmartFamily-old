namespace SmartFamily.Contracts.Services
{
    /// <summary>
    /// Recent files service interface.
    /// </summary>
    public interface IRecentFilesService
    {
        /// <summary>
        /// Restore data for the recent files.
        /// </summary>
        void RestoreData();

        /// <summary>
        /// Persist data from recent files.
        /// </summary>
        void PersistData();
    }
}