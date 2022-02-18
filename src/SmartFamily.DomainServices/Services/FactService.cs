using SmartFamily.Core;
using SmartFamily.Core.Data;
using SmartFamily.DomainServices.Common;
using SmartFamily.DomainServices.Contracts.Services;

namespace SmartFamily.DomainServices.Services
{
    public class FactService : EntityService<Fact>, IFactService
    {
        /// <summary>
        /// Constructs a FAct Service that will use the specified
        /// <see cref="IUnitOfWork"/> to retrieve data.
        /// </summary>
        /// <param name="unitOfWork">The <see cref="IUnitOfWork"/>.</param>
        public FactService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}