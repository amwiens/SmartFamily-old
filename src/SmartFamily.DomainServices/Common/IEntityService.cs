using SmartFamily.Core.Common;

namespace SmartFamily.DomainServices.Common
{
    public interface IEntityService<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Adds an entity to the data store and sets the <see cref="BaseEntity.Id"/> property
        /// of the <paramref name="entity"/> to the id of the new entity.
        /// </summary>
        /// <param name="entity">The entity to add to the data store.</param>
        void Add(TEntity entity);

        /// <summary>
        /// Deletes an entity from the data store.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <remarks>
        ///     The delete operation takes effect immediately.
        /// </remarks>
        void Delete(TEntity entity);

        /// <summary>
        /// Retrieves a single TEntity.
        /// </summary>
        /// <param name="id">The Id of the entity to retrieve.</param>
        /// <param name="treeId">The Id of the Tree.</param>
        /// <returns>A <see cref="TEntity"/>.</returns>
        TEntity Get(string id, string treeId);

        /// <summary>
        /// Retrieves all the entities of this type for a Tree.
        /// </summary>
        /// <param name="treeId">The Id of the Tree.</param>
        /// <returns>A collection of <see cref="TEntity"/>.</returns>
        IEnumerable<TEntity> Get(string treeId);

        /// <summary>
        /// Gets a list of entities base on a predicate.
        /// </summary>
        /// <param name="treeId">The Id of the tree.</param>
        /// <param name="predicate">The predicate to use.</param>
        /// <returns>List of entities.</returns>
        IEnumerable<TEntity> Get(string treeId, Func<TEntity, bool> predicate);

        /// <summary>
        /// Updates an entity in the data store.
        /// </summary>
        /// <param name="entity">The entity to update in the data store.</param>
        void Update(TEntity entity);
    }
}