using Microsoft.EntityFrameworkCore;

using SmartFamily.Core.Models;

namespace SmartFamily.EntityFramework
{
    public class SmartFamilyDbContext : DbContext
    {
        private readonly string _dbPath;

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

        public SmartFamilyDbContext(string dbPath)
        {
            _dbPath = dbPath;
        }

        public SmartFamilyDbContext(DbContextOptions<SmartFamilyDbContext> options, string dbPath)
            : base(options)
        {
            _dbPath = dbPath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlite($"Filename={_dbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<FileSettings> FileSettings { get; set; }
    }
}