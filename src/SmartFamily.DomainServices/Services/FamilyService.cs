using SmartFamily.Core;
using SmartFamily.Core.Data;
using SmartFamily.DomainServices.Common;
using SmartFamily.DomainServices.Contracts.Services;

namespace SmartFamily.DomainServices.Services
{
    /// <summary>
    /// The FamilyService provides a Facade to the Families store,
    /// allowing for additional business logic to be applied.
    /// </summary>
    public class FamilyService : EntityService<Family>, IFamilyService
    {
        /// <summary>
        /// Constructs a Family Service that will use the specified
        /// <see cref="IUnitOfWork"/> to retrieve data.
        /// </summary>
        /// <param name="unitOfWork">The <see cref="IUnitOfWork"/> to use to retrieve data.</param>
        public FamilyService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}