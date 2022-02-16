using SmartFamily.Core;
using SmartFamily.Core.Common;
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
        private static IEnumerable<Individual> AllPeople()
        {
            return new List<Individual>()
            {
                new Individual()
                {
                    FirstName = "William Jefferson",
                    LastName = "Clinton",
                    Sex = Sex.Male,
                    Id = "1",
                    Facts = new List<Fact>
                    {
                        new Fact { Id = "1", Date = "19 AUG 1946", FactType = FactType.Birth, Place = "Hope, Hempstead, Arkansas, Unites States" },
                        new Fact { Id = "2", Date = "11 OCT 1975", FactType = FactType.Marriage, Place = "" }
                    }
                    //Bio = "This is a temp bio. This is just to test to see what this will look like on the screen.",
                    //PrimaryImagePath = "",
                },
                new Individual()
                {
                    FirstName = "Hillary",
                    LastName = "Rodham",
                    Sex = Sex.Female,
                    Id = "2",
                    Facts = new List<Fact>
                    {
                        new Fact { Id = "3", Date = "26 OCT 1947", FactType = FactType.Birth, Place = "Chicago, Cook, Illinois, United States" },
                        new Fact { Id = "4", Date = "11 OCT 1975", FactType = FactType.Marriage, Place = "" }
                    }
                    //Bio = "This is a temp bio. This is just to test to see what this will look like on the screen.",
                    //PrimaryImagePath = "",
                },
                new Individual()
                {
                    FirstName = "William Jefferson",
                    LastName = "Blythe",
                    Sex = Sex.Male,
                    Id = "3",
                    Facts = new List<Fact>
                    {
                        new Fact { Id = "5", Date = "27 FEB 1918", FactType = FactType.Birth, Place = "Sherman, Grayson, Texas, United States" },
                        new Fact { Id = "6", Date = "3 SEP 1943", FactType = FactType.Marriage, Place = "Texarkana, Miller, Arkansas, United States" },
                        new Fact { Id = "7", Date = "17 MAY 1946", FactType = FactType.Death, Place = "Sikeston, Scott, Missouri, United States" },
                    }
                    //Bio = "This is a temp bio. This is just to test to see what this will look like on the screen.",
                    //PrimaryImagePath = "",
                },
                new Individual()
                {
                    FirstName = "Virginia Dell",
                    LastName = "Cassidy",
                    Sex = Sex.Female,
                    Id = "4",
                    Facts = new List<Fact>
                    {
                        new Fact { Id = "8", Date = "6 JUN 1923", FactType = FactType.Birth, Place = "Bodcaw, Nevada, Arkansas, United States" },
                        new Fact { Id = "9", Date = "3 SEP 1943", FactType = FactType.Marriage, Place = "Texarkana, Miller, Arkansas, United States" },
                        new Fact { Id = "10", Date = "JAN 1994", FactType = FactType.Death, Place = "" },
                    }
                    //Bio = "This is a temp bio. This is just to test to see what this will look like on the screen.",
                    //PrimaryImagePath = "",
                },
                new Individual()
                {
                    FirstName = "Roger",
                    LastName = "Clinton",
                    Sex = Sex.Male,
                    Id = "5",
                    Facts = new List<Fact>
                    {
                        new Fact { Id = "11", Date = "25 JUL 1909", FactType = FactType.Birth, Place = "" },
                        new Fact { Id = "12", Date = "3 SEP 1943", FactType = FactType.Marriage, Place = "Texarkana, Miller, Arkansas, United States" },
                        new Fact { Id = "13", Date = "NOV 1967", FactType = FactType.Death, Place = "" },
                    }
                    //Bio = "This is a temp bio. This is just to test to see what this will look like on the screen.",
                    //PrimaryImagePath = "",
                },
            };
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Individual>> GetContentGridDataAsync()
        {
            await Task.CompletedTask;
            return AllPeople();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Individual>> GetGridDataAsync()
        {
            await Task.CompletedTask;
            return AllPeople();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Individual>> GetListDetailsDataAsync()
        {
            await Task.CompletedTask;
            return AllPeople();
        }
    }
}