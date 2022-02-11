using SmartFamily.Core;
using SmartFamily.Core.Data;
using SmartFamily.Core.Guards;

namespace SmartFamily.Data
{
    public class GEDCOMIndividualRepository : IRepository<Individual>
    {
        private readonly IGEDCOMStore _database;

        public GEDCOMIndividualRepository(IGEDCOMStore database)
        {
            Guard.Argument(database, nameof(database)).NotNull();

            _database = database;
        }

        public void Add(Individual item)
        {
            Guard.Argument(item, nameof(item)).NotNull();

            _database.AddIndividual(item);
        }

        public void Delete(Individual item)
        {
            Guard.Argument(item, nameof(item)).NotNull();

            _database.DeleteIndividual(item);
        }

        public IEnumerable<Individual> Find(Func<Individual, bool> predicate)
        {
            return GetAll().Where(predicate);
        }

        IEnumerable<Individual> IRepository<Individual>.GetAll()
        {
            return GetAll();
        }

        public IEnumerable<Individual> GetAll()
        {
            return _database.Individuals;
        }

        public void Update(Individual item)
        {
            Guard.Argument(item, nameof(item)).NotNull();

            _database.UpdateIndividual(item);
        }
    }
}