using SmartFamily.Core;
using SmartFamily.Core.Data;
using SmartFamily.DomainServices.Common;
using SmartFamily.DomainServices.Contracts.Services;

namespace SmartFamily.DomainServices.Services
{
    /// <summary>
    /// The RepositoryService provides a Facade to the Repositories(GEDCOM) store,
    /// allowing for additional business logic to be applied.
    /// </summary>
    public class RepositoryService : EntityService<Repository>, IRepositoryService
    {
        /// <summary>
        /// Constructs a Repository Service that will use the specified
        /// <see cref="IUnitOfWork"/> to retrieve data.
        /// </summary>
        /// <param name="unitOfWork">The <see cref="IUnitOfWork"/> to use to retrieve data.</param>
        public RepositoryService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}