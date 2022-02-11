namespace SmartFamily.Core.Data
{
    public interface IRepository<TModel> where TModel : class
    {
        /// <summary>
        /// Add an Item into the repository.
        /// </summary>
        /// <param name="item">The item to be added.</param>
        void Add(TModel item);

        /// <summary>
        /// Delete an Item from the repository.
        /// </summary>
        /// <param name="item">The item to be deleted.</param>
        void Delete(TModel item);

        /// <summary>
        /// Find items from the repository based on a Linq predicate.
        /// </summary>
        /// <param name="predicate">The Linq predicate</param>
        /// <returns>A list of items.</returns>
        IEnumerable<TModel> Find(Func<TModel, bool> predicate);

        /// <summary>
        /// Returns all the items in the repository as an enumerable list.
        /// </summary>
        /// <returns>The list of items.</returns>
        IEnumerable<TModel> GetAll();

        /// <summary>
        /// Updates an Item in the repository.
        /// </summary>
        /// <param name="item">The item to be updated.</param>
        void Update(TModel item);
    }
}