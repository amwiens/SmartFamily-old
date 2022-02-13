using SmartFamily.Core;
using SmartFamily.Core.Data;
using SmartFamily.Core.Guards;

namespace SmartFamily.Data
{
    public class GEDCOMUnitOfWork : IUnitOfWork
    {
        private IGEDCOMStore _database;

        public GEDCOMUnitOfWork(string path)
        {
            Guard.Argument(path, nameof(path))
                .NotNull()
                .NotEmpty();

            Initialize(new GEDCOMStore(path));
        }

        public GEDCOMUnitOfWork(IGEDCOMStore database)
        {
            Guard.Argument(database, nameof(database))
                .NotNull();

            Initialize(database);
        }

        private void Initialize(IGEDCOMStore database)
        {
            _database = database;
        }

        public void BeginTransaction()
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        public void Commit()
        {
            _database.SaveChanges();
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            if (typeof(T) == typeof(Individual))
            {
                return new GEDCOMIndividualRepository(_database) as IRepository<T>;
            }
            if (typeof(T) == typeof(Family))
            {
                return new GGEDCOMFamilyRepository(_database) as IRepository<T>;
            }
            throw new NotImplementedException();
        }

        public void RollbackTransaction()
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }
    }
}