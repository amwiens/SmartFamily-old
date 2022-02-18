using SmartFamily.Core;
using SmartFamily.Core.Data;
using SmartFamily.DomainServices.Common;
using SmartFamily.DomainServices.Contracts.Services;

namespace SmartFamily.DomainServices.Services
{
    /// <summary>
    /// The IndividualService provides a Facade to the Individuals store,
    /// allowing for additional business logic to be applied.
    /// </summary>
    public class IndividualService : EntityService<Individual>, IIndividualService
    {
        /// <summary>
        /// Constructs an Individuals Service to manage Individuals.
        /// </summary>
        /// <param name="unitOfWork">The Unit Of Work to use to retrieve data.</param>
        public IndividualService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}