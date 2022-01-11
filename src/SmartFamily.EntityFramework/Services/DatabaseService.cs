using Microsoft.EntityFrameworkCore;

using SmartFamily.Core.Contracts.Services;

namespace SmartFamily.EntityFramework.Services
{
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
        public string OpenDatabase(string databasePath)
        {
            if (!File.Exists(databasePath))
            {
                throw new FileNotFoundException("Database not found", databasePath);
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