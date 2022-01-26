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
                    Name = "Clinton, William Jefferson",
                    Sex = "M",
                    RecordNumber = "1",
                    GrandmaNumber = "",
                    BirthDate = "19 AUG 1946",
                    BirthPlace = "Hope, Hempstead, Arkansas, Unites States",
                    MarriageDate = "11 OCT 1975",
                    MarriagePlace = "",
                    Bio = "This is a temp bio. This is just to test to see what this will look like on the screen.",
                    PrimaryImagePath = "",
                },
                new SamplePerson()
                {
                    Name = "Rodham, Hillary",
                    Sex = "F",
                    RecordNumber = "2",
                    GrandmaNumber = "",
                    BirthDate = "26 OCT 1947",
                    BirthPlace = "Chicago, Cook, Illinois, United States",
                    MarriageDate = "11 OCT 1975",
                    MarriagePlace = "",
                    Bio = "This is a temp bio. This is just to test to see what this will look like on the screen.",
                    PrimaryImagePath = "",
                },
                new SamplePerson()
                {
                    Name = "Blythe, William Jefferson",
                    Sex = "M",
                    RecordNumber = "3",
                    GrandmaNumber = "",
                    BirthDate = "27 FEB 1918",
                    BirthPlace = "Sherman, Grayson, Texas, United States",
                    MarriageDate = "3 SEP 1943",
                    MarriagePlace = "Texarkana, Miller, Arkansas, United States",
                    DeathDate = "17 MAY 1946",
                    DeathPlace = "Sikeston, Scott, Missouri, United States",
                    Bio = "This is a temp bio. This is just to test to see what this will look like on the screen.",
                    PrimaryImagePath = "",
                },
                new SamplePerson()
                {
                    Name = "Cassidy, Virginia Dell",
                    Sex = "F",
                    RecordNumber = "4",
                    GrandmaNumber = "",
                    BirthDate = "6 JUN 1923",
                    BirthPlace = "Bodcaw, Nevada, Arkansas, United States",
                    MarriageDate = "3 SEP 1943",
                    MarriagePlace = "Texarkana, Miller, Arkansas, United States",
                    DeathDate = "JAN 1994",
                    DeathPlace = "",
                    Bio = "This is a temp bio. This is just to test to see what this will look like on the screen.",
                    PrimaryImagePath = "",
                },
                new SamplePerson()
                {
                    Name = "Clinton, Roger",
                    Sex = "M",
                    RecordNumber = "5",
                    GrandmaNumber = "",
                    BirthDate = "25 JUL 1909",
                    BirthPlace = "",
                    MarriageDate = "",
                    MarriagePlace = "",
                    DeathDate = "NOV 1967",
                    DeathPlace = "",
                    Bio = "This is a temp bio. This is just to test to see what this will look like on the screen.",
                    PrimaryImagePath = "",
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