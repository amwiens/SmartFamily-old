using SmartFamily.Core;

namespace SmartFamily.DomainServices.Contracts.Services
{
    /// <summary>
    /// An interface that represents the Tree Service.
    /// </summary>
    public interface ITreeService
    {
        /// <summary>
        /// Adds a tree to the data store and sets the <see cref="Tree.TreeId"/> property
        /// of the <paramref name="tree"/> to the id of the new tree.
        /// </summary>
        /// <param name="tree">The tree to add to the data store.</param>
        void Add(Tree tree);

        /// <summary>
        /// Deletes a tree from the data store.
        /// </summary>
        /// <param name="tree">The tree to delete.</param>
        /// <remarks>
        ///     The delete operation takes effect immediately.
        /// </remarks>
        void Delete(Tree tree);

        /// <summary>
        /// Retrieves a single tree.
        /// </summary>
        /// <param name="treeId">The Id of the tree.</param>
        /// <returns>A <see cref="Tree"/>.</returns>
        Tree Get(string treeId);

        /// <summary>
        /// Retrieves all the trees.
        /// </summary>
        /// <returns>A collection of <see cref="Tree"/> objects.</returns>
        IEnumerable<Tree> Get();

        /// <summary>
        /// Updates a tree in the data store.
        /// </summary>
        /// <param name="tree">The tree to update in the data store.</param>
        void Update(Tree tree);
    }
}