using SmartFamily.Core.Collections;

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
        /// <param name="predicate">The Linq predicate.</param>
        /// <returns>A list of items.</returns>
        IEnumerable<TModel> Find(Func<TModel, bool> predicate);

        /// <summary>
        /// Find a Page of items from the repository based on a Linq predicate.
        /// </summary>
        /// <param name="pageIndex">The page Index to fetch.</param>
        /// <param name="pageSize">The size of the page to fetch.</param>
        /// <param name="predicate">The Linq predicate.</param>
        /// <returns>A list of items.</returns>
        IPagedList<TModel> Find(int pageIndex, int pageSize, Func<TModel, bool> predicate);

        /// <summary>
        /// Returns all the items in the repository as an enumerable list.
        /// </summary>
        /// <returns>The list of items.</returns>
        IEnumerable<TModel> GetAll();

        /// <summary>
        /// Returns a page of items in the repository as a paged list.
        /// </summary>
        /// <param name="pageIndex">The page Index to fetch.</param>
        /// <param name="pageSize">The size of the page to fetch.</param>
        /// <returns>The list of items.</returns>
        IPagedList<TModel> GetPage(int pageIndex, int pageSize);

        /// <summary>
        /// Updates an Item in the repository.
        /// </summary>
        /// <param name="item">The item to be updated.</param>
        void Update(TModel item);
    }
}