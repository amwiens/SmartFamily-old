using SmartFamily.Core;

namespace SmartFamily.EntityFramework.Contracts.Services
{
    /// <summary>
    /// Sample data service
    /// </summary>
    public interface ISampleDataService
    {
        /// <summary>
        /// Get content grid data.
        /// </summary>
        /// <returns>Content grid data.</returns>
        Task<IEnumerable<Individual>> GetContentGridDataAsync();

        /// <summary>
        /// Get grid data.
        /// </summary>
        /// <returns>Grid data.</returns>
        Task<IEnumerable<Individual>> GetGridDataAsync();

        /// <summary>
        /// Get list details data.
        /// </summary>
        /// <returns>List details data.</returns>
        Task<IEnumerable<Individual>> GetListDetailsDataAsync();
    }
}