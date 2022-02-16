using SmartFamily.Core.Common;
using SmartFamily.Core.Data;
using SmartFamily.Core.Guards;

namespace SmartFamily.DomainServices.Common
{
    /// <summary>
    /// Abstract base service for the domain services. This class handles the "core" CRUD operations.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class EntityService<TEntity> : IEntityService<TEntity> where TEntity : BaseEntity
    {
        private readonly IUnitOfWork _unitOfWork;

        protected EntityService(IUnitOfWork unitOfWork)
        {
            Guard.Argument(unitOfWork, nameof(unitOfWork)).NotNull();

            _unitOfWork = unitOfWork;
            Repository = _unitOfWork.GetRepository<TEntity>();
        }

        protected IRepository<TEntity> Repository { get; private set; }

        /// <summary>
        /// Adds an entity to the data store and sets the <see cref="BaseEntity.Id"/> property
        /// of the <paramref name="entity"/> to the id of the new entity.
        /// </summary>
        /// <param name="entity">The entity to add to the data store.</param>
        public virtual void Add(TEntity entity)
        {
            Guard.Argument(entity, nameof(entity)).NotNull();

            Repository.Add(entity);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// Deletes an entity from the data store.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <remarks>
        ///     The delete operation takes effect immediately.
        /// </remarks>
        public virtual void Delete(TEntity entity)
        {
            Guard.Argument(entity, nameof(entity)).NotNull();

            Repository.Delete(entity);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// Retrieves a single TEntity.
        /// </summary>
        /// <param name="id">The Id of the entity to retrieve.</param>
        /// <param name="treeId">The Id of the tree.</param>
        /// <returns>A <see cref="TEntity"/>.</returns>
        public virtual TEntity Get(string id, string treeId)
        {
            Guard.Argument(id, nameof(id)).NotNull().NotEmpty();

            return Get(treeId).SingleOrDefault(n => n.Id == id);
        }

        /// <summary>
        /// Retrieves all the entities of this type for a Tree.
        /// </summary>
        /// <param name="treeId">The Id of the Tree.</param>
        /// <returns>A collection of <see cref="TEntity"/>.</returns>
        public virtual IEnumerable<TEntity> Get(string treeId)
        {
            Guard.Argument(treeId, nameof(treeId)).NotNull().NotEmpty();

            return Repository.Find(t => t.TreeId == treeId);
        }

        /// <summary>
        /// Gets a list of entities based on a predicate.
        /// </summary>
        /// <param name="treeId">The Id of the tree.</param>
        /// <param name="predicate">The predicate to use.</param>
        /// <returns>List of entities.</returns>
        public IEnumerable<TEntity> Get(string treeId, Func<TEntity, bool> predicate)
        {
            return Get(treeId).Where(predicate);
        }

        /// <summary>
        /// Updates an entity in the data store.
        /// </summary>
        /// <param name="entity">The entity to update in the data store.</param>
        public virtual void Update(TEntity entity)
        {
            Guard.Argument(entity, nameof(entity)).NotNull();

            Repository.Update(entity);
            _unitOfWork.Commit();
        }
    }
}