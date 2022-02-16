using SmartFamily.Core;
using SmartFamily.Core.Data;
using SmartFamily.DomainServices.Common;
using SmartFamily.DomainServices.Contracts.Services;

namespace SmartFamily.DomainServices.Services
{

    public class NoteService : EntityService<Note>, INoteService
    {
        /// <summary>
        /// Constructs a Fact Service that will use the specified
        /// <see cref="IUnitOfWork"/> to retrieve data.
        /// </summary>
        /// <param name="unitOfWork">The <see cref="IUnitOfWork"/>.</param>
        public NoteService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}