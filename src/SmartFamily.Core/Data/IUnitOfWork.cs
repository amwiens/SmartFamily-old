namespace SmartFamily.Core.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();

        IRepository<T> GetRepository<T>() where T : class;
    }
}