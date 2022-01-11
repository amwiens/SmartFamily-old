namespace SmartFamily.Core.Contracts.Services
{
    public interface IDatabaseService
    {
        /// <summary>
        /// Creates a database with the given database name.
        /// </summary>
        /// <param name="databaseName">Database name.</param>
        /// <returns>Database path.</returns>
        string CreateDatabase(string databaseName);

        /// <summary>
        /// Opens a database with the given database name.
        /// </summary>
        /// <param name="databaseName">Database name.</param>
        /// <returns>Database path.</returns>
        string OpenDatabase(string databaseName);

        /// <summary>
        /// Backs up the given database.
        /// </summary>
        /// <param name="databasePath">Database path.</param>
        /// <returns><c>true</c> if backed up successfully, otherwise <c>false</c>.</returns>
        bool BackupDatabase(string databasePath);

        /// <summary>
        /// Optimizes the given database.
        /// </summary>
        /// <param name="databasePath">Database path.</param>
        /// <returns><c>true</c> if optimized successfully, otherwise <c>false</c>.</returns>
        bool OptimizeDatabase(string databasePath);
    }
}