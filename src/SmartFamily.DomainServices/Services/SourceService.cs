using SmartFamily.Core;
using SmartFamily.Core.Data;
using SmartFamily.DomainServices.Common;
using SmartFamily.DomainServices.Contracts.Services;

namespace SmartFamily.DomainServices.Services
{
    /// <summary>
    /// The SourceService provides a Facade to the Sources store,
    /// allowing for additional business logic to be applied.
    /// </summary>
    public class SourceService : EntityService<Source>, ISourceService
    {
        /// <summary>
        /// Constructs a Source Service that will use the specified
        /// <see cref="IUnitOfWork"/> to retrieve data.
        /// </summary>
        /// <param name="unitOfWork"></param>
        public SourceService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}