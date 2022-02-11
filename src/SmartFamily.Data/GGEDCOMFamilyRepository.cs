using SmartFamily.Core;
using SmartFamily.Core.Data;
using SmartFamily.Core.Guards;

namespace SmartFamily.Data
{
    public class GGEDCOMFamilyRepository : IRepository<Family>
    {
        private readonly IGEDCOMStore _database;

        public GGEDCOMFamilyRepository(IGEDCOMStore database)
        {
            Guard.Argument(database, nameof(database)).NotNull();

            _database = database;
        }

        public void Add(Family item)
        {
            Guard.Argument(item, nameof(item)).NotNull();

            _database.AddFamily(item);
        }

        public void Delete(Family item)
        {
            Guard.Argument(item, nameof(item)).NotNull();

            _database.DeleteFamily(item);
        }

        public IEnumerable<Family> Find(string sqlCondition, params object[] args)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Family> Find(Func<Family, bool> predicate)
        {
            return GetAll().Where(predicate);
        }

        public IEnumerable<Family> Get<TScopeType>(TScopeType scopeValue)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Family> IRepository<Family>.GetAll()
        {
            return GetAll();
        }

        public Family GetById<TProperty>(TProperty id)
        {
            throw new NotImplementedException();
        }

        public Family GetById<TProperty, TScopeType>(TProperty id, TScopeType scopeValue)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Family> GetAll()
        {
            return _database.Families;
        }

        public void Update(Family item)
        {
            Guard.Argument(item, nameof(item)).NotNull();

            _database.UpdateFamily(item);
        }

        public IEnumerable<Family> GetByProperty<TProperty>(string propertyName, TProperty propertyValue)
        {
            throw new NotImplementedException();
        }
    }
}