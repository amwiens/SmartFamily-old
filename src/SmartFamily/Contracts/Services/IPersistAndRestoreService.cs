namespace SmartFamily.Contracts.Services
{
    /// <summary>
    /// Persist and restore service interface.
    /// </summary>
    public interface IPersistAndRestoreService
    {
        /// <summary>
        /// Restore data for the App.Properties
        /// </summary>
        void RestoreData();

        /// <summary>
        /// Persist data from App.Properties
        /// </summary>
        void PersistData();
    }
}