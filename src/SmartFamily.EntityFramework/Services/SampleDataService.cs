using SmartFamily.Core.Models;
using SmartFamily.EntityFramework.Contracts.Services;

namespace SmartFamily.EntityFramework.Services
{
    /// <summary>
    /// Sample data service.
    /// </summary>
    public class SampleDataService : ISampleDataService
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public SampleDataService()
        {
        }

        /// <summary>
        /// All people.
        /// </summary>
        /// <returns>All people.</returns>
        private static IEnumerable<SamplePerson> AllPeople()
        {
            return new List<SamplePerson>()
            {
                new SamplePerson()
                {
                    Name = "Wiens, Aaron Matthew",
                    Sex = "M",
                    RecordNumber = "1",
                    GrandmaNumber = "1",
                    BirthDate = "6 Nov 1976",
                    BirthPlace = "Langdon, Cavalier, North Dakota, United States",
                    MarriageDate = "26 Jul 2008",
                    MarriagePlace = "Mackinac Island, Mackinac, Michigan, United States",
                },
                new SamplePerson()
                {
                    Name = "Wiens, Addison Grace",
                    Sex = "F",
                    RecordNumber = "2",
                    GrandmaNumber = "2",
                    BirthDate = "27 Dec 2010",
                    BirthPlace = "Rochester, Olmsted, Minnesota, United States",
                    MarriageDate = "",
                    MarriagePlace = "",
                },
                new SamplePerson()
                {
                    Name = "Wiens, Chloe Monroe",
                    Sex = "F",
                    RecordNumber = "3",
                    GrandmaNumber = "3",
                    BirthDate = "16 Sep 2014",
                    BirthPlace = "Rochester, Olmsted, Minnesota, United States",
                    MarriageDate = "",
                    MarriagePlace = "",
                },
                new SamplePerson()
                {
                    Name = "Wiens, Allen Edwin",
                    Sex = "M",
                    RecordNumber = "4",
                    GrandmaNumber = "4",
                    BirthDate = "3 Dec 1951",
                    BirthPlace = "Langdon, Cavalier, North Dakota, United States",
                    MarriageDate = "11 Aug 1972",
                    MarriagePlace = "Whitewater, Butler, Kansas, United States",
                },
                new SamplePerson()
                {
                    Name = "Goedtel, Carissa Lee",
                    Sex = "F",
                    RecordNumber = "5",
                    GrandmaNumber = "5",
                    BirthDate = "2 Mar 1980",
                    BirthPlace = "Owatonna, Steele, Minnesota, United States",
                    MarriageDate = "26 Jul 2008",
                    MarriagePlace = "Mackinac Island, Mackinac, Michigan, United States",
                },
            };
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<SamplePerson>> GetContentGridDataAsync()
        {
            await Task.CompletedTask;
            return AllPeople();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<SamplePerson>> GetGridDataAsync()
        {
            await Task.CompletedTask;
            return AllPeople();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<SamplePerson>> GetListDetailsDataAsync()
        {
            await Task.CompletedTask;
            return AllPeople();
        }
    }
}