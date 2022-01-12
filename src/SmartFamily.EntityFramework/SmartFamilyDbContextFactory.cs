using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using SmartFamily.Core;

namespace SmartFamily.EntityFramework
{
    /// <summary>
    /// Database context factory.
    /// </summary>
    public class SmartFamilyDbContextFactory : IDesignTimeDbContextFactory<SmartFamilyDbContext>
    {
        /// <summary>
        /// Creates the database context.
        /// </summary>
        /// <param name="args">Arguments to pass in.</param>
        /// <returns>Database context.</returns>
        public SmartFamilyDbContext CreateDbContext(string[] args = null)
        {
            var dbPath = string.Empty;
            var options = new DbContextOptionsBuilder<SmartFamilyDbContext>();
            if (string.IsNullOrWhiteSpace(ApplicationSettings.OpenDatabase))
            {
                dbPath = Path.Combine(Environment.CurrentDirectory, "smartFamily.db");
            }
            else
            {
                dbPath = ApplicationSettings.OpenDatabase;
            }
            options.UseSqlite(dbPath);
            _ = SmartFamilyDbContext.Create(dbPath);

            return new SmartFamilyDbContext(options.Options, dbPath);
        }
    }
}