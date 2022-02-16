using SmartFamily.Core;
using SmartFamily.Core.Data;
using SmartFamily.DomainServices.Common;
using SmartFamily.DomainServices.Contracts.Services;

namespace SmartFamily.DomainServices.Services
{
    /// <summary>
    /// The MultimediaLinkService provides a Facade to the MultimediaLink store,
    /// allowing for additional business logic to be applied.
    /// </summary>
    public class MultimediaLinkService : EntityService<MultimediaLink>, IMultimediaLinkService
    {
        /// <summary>
        /// Constructs a MultimediaLink Service to manage Multimedia Links.
        /// </summary>
        /// <param name="unitOfWork">The Unit of Work to use to retrieve data.</param>
        public MultimediaLinkService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}