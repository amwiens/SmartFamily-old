using SmartFamily.Core.Models;
using SmartFamily.EntityFramework.Contracts.Services;

namespace SmartFamily.EntityFramework.Services
{
    public class SampleDataService : ISampleDataService
    {
        public SampleDataService()
        {
        }

        private static IEnumerable<SamplePerson> AllPeople()
        {
            return new List<SamplePerson>()
            {
                new SamplePerson()
                {
                    Name = "Wiens, Aaron Matthew",
                    Sex = "M",
                    RecordNumber = "1",
                    GrandmaNumber = "1"
                },
                new SamplePerson()
                {
                    Name = "Wiens, Addison Grace",
                    Sex = "F",
                    RecordNumber = "2",
                    GrandmaNumber = "2"
                },
                new SamplePerson()
                {
                    Name = "Wiens, Chloe Monroe",
                    Sex = "F",
                    RecordNumber = "3",
                    GrandmaNumber = "3"
                },
                new SamplePerson()
                {
                    Name = "Wiens, Allen Edwin",
                    Sex = "M",
                    RecordNumber = "4",
                    GrandmaNumber = "4"
                },
                new SamplePerson()
                {
                    Name = "Goedtel, Carissa Lee",
                    Sex = "F",
                    RecordNumber = "5",
                    GrandmaNumber = "5"
                },
            };
        }

        public async Task<IEnumerable<SamplePerson>> GetContentGridDataAsync()
        {
            await Task.CompletedTask;
            return AllPeople();
        }

        public async Task<IEnumerable<SamplePerson>> GetGridDataAsync()
        {
            await Task.CompletedTask;
            return AllPeople();
        }

        public async Task<IEnumerable<SamplePerson>> GetListDetailsDataAsync()
        {
            await Task.CompletedTask;
            return AllPeople();
        }
    }
}