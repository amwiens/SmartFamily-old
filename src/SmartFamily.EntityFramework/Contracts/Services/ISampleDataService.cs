using SmartFamily.Core.Models;

namespace SmartFamily.EntityFramework.Contracts.Services
{
    public interface ISampleDataService
    {
        Task<IEnumerable<SamplePerson>> GetContentGridDataAsync();

        Task<IEnumerable<SamplePerson>> GetGridDataAsync();

        Task<IEnumerable<SamplePerson>> GetListDetailsDataAsync();
    }
}