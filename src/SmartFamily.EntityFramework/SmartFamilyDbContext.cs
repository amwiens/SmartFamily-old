using Microsoft.EntityFrameworkCore;

using SmartFamily.Core.Models;

namespace SmartFamily.EntityFramework
{
    /// <summary>
    /// Database context
    /// </summary>
    public class SmartFamilyDbContext : DbContext
    {
        private readonly string _dbPath;

        /// <summary>
        /// Creates the database context
        /// </summary>
        /// <param name="dbPath">Path to the databse.</param>
        /// <returns>Database context.</returns>
        public static SmartFamilyDbContext Create(string dbPath)
        {
            if (!File.Exists(dbPath))
            {
                var fs = File.Create(dbPath);
                fs.Close();
            }
            SmartFamilyDbContext ctx = new SmartFamilyDbContext(dbPath);
            ctx.Database.Migrate();
            return ctx;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="dbPath">Path to the database.</param>
        public SmartFamilyDbContext(string dbPath)
        {
            _dbPath = dbPath;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="options">Database context options.</param>
        /// <param name="dbPath">Path to the database.</param>
        public SmartFamilyDbContext(DbContextOptions<SmartFamilyDbContext> options, string dbPath)
            : base(options)
        {
            _dbPath = dbPath;
        }

        /// <summary>
        /// Configures the database context.
        /// </summary>
        /// <param name="optionsBuilder">Database context options builder.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlite($"Filename={_dbPath}");

        /// <summary>
        /// Creates the model.
        /// </summary>
        /// <param name="modelBuilder">Model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// File settings.
        /// </summary>
        public DbSet<FileSettings> FileSettings { get; set; }
    }
}