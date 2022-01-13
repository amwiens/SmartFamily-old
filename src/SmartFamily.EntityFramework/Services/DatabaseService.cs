using Microsoft.EntityFrameworkCore;

using SmartFamily.Core.Contracts.Services;
using SmartFamily.Core.Extensions;

namespace SmartFamily.EntityFramework.Services
{
    /// <summary>
    /// Database service.
    /// </summary>
    public class DatabaseService : IDatabaseService
    {
        /// <inheritdoc/>
        public string CreateDatabase(string databasePath)
        {
            if (!File.Exists(databasePath))
            {
                var fs = File.Create(databasePath);
                fs.Close();
            }
            SmartFamilyDbContext ctx = new SmartFamilyDbContext(databasePath);
            ctx.Database.Migrate();
            return databasePath;
        }

        /// <inheritdoc/>
        /// <exception cref="ApplicationException">Thrown when a file selected isn't a sqlite database.</exception>
        public string OpenDatabase(string databasePath)
        {
            if (!File.Exists(databasePath))
            {
                throw new FileNotFoundException("Database not found", databasePath);
            }
            if (!databasePath.IsSQLiteDatabase())
            {
                throw new ApplicationException("Not a valid database format.");
            }
            SmartFamilyDbContext ctx = new SmartFamilyDbContext(databasePath);
            ctx.Database.Migrate();
            return databasePath;
        }

        /// <inheritdoc/>
        public bool BackupDatabase(string databasePath)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool OptimizeDatabase(string databasePath)
        {
            throw new NotImplementedException();
        }
    }
}