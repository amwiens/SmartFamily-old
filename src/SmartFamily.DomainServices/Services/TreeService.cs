using SmartFamily.Core;
using SmartFamily.Core.Collections;
using SmartFamily.Core.Data;
using SmartFamily.Core.Guards;
using SmartFamily.DomainServices.Contracts.Services;

namespace SmartFamily.DomainServices.Services
{
    public class TreeService : ITreeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Tree> _repository;

        public TreeService(IUnitOfWork unitOfWork)
        {
            Guard.Argument(unitOfWork, nameof(unitOfWork)).NotNull();

            _unitOfWork = unitOfWork;

            _repository = _unitOfWork.GetRepository<Tree>();
        }

        /// <summary>
        /// Adds a tree to the data store and sets the <see cref="Tree.TreeId"/> property
        /// of the <paramref name="tree"/> to the id of the new tree.
        /// </summary>
        /// <param name="tree">The tree to add to the data store.</param>
        public void Add(Tree tree)
        {
            // Contract
            Guard.Argument(tree, nameof(tree)).NotNull();

            _repository.Add(tree);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// Deletes a tree from the data store.
        /// </summary>
        /// <param name="tree">The tree to delete.</param>
        /// <remarks>
        ///     The delete operation takes effect immediately.
        /// </remarks>
        public void Delete(Tree tree)
        {
            // Contract
            Guard.Argument(tree, nameof(tree)).NotNull();

            _repository.Delete(tree);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// Retrieves a single tree.
        /// </summary>
        /// <param name="treeId">The Id of the tree.</param>
        /// <returns>A <see cref="Tree"/>.</returns>
        public Tree Get(string treeId)
        {
            Guard.Argument(treeId, nameof(treeId)).NotNull().NotEmpty();

            return Get().SingleOrDefault(t => t.TreeId == treeId);
        }

        /// <summary>
        /// Retrieves all the trees.
        /// </summary>
        /// <returns>A collection of <see cref="Tree"/> objects.</returns>
        public IEnumerable<Tree> Get()
        {
            return _repository.GetAll();
        }

        /// <summary>
        /// Gets a page of trees based on a predicate.
        /// </summary>
        /// <param name="predicate">The predicate to use.</param>
        /// <param name="pageIndex">The page index to return.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>List of trees.</returns>
        public IPagedList<Tree> Get(Func<Tree, bool> predicate, int pageIndex, int pageSize)
        {
            return new PagedList<Tree>(Get().Where(predicate), pageIndex, pageSize);
        }

        /// <summary>
        /// Updates a tree in the data store.
        /// </summary>
        /// <param name="tree">The tree to update in the data store.</param>
        public void Update(Tree tree)
        {
        }
    }
}