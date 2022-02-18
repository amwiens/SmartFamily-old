using SmarFamily.TestUtilities;

using SmartFamily.Core;
using SmartFamily.DomainServices.Services;

using System.Collections.Generic;

namespace SmartFamily.DomainServices.Tests
{
    public class IndividualServiceTests : EntityServiceBaseTests<Individual, IndividualService>
    {
        protected override IEnumerable<Individual> GetEntities(int count)
        {
            var individuals = new List<Individual>();

            for (int i = 0; i < count; i++)
            {
                individuals.Add(new Individual
                {
                    Id = i.ToString(),
                    FirstName = string.Format(TestConstants.IND_FirstName, i),
                    LastName = (i <= TestConstants.IND_LastNameCount) ? TestConstants.IND_LastName : TestConstants.IND_AltLastName,
                    TreeId = TestConstants.TREE_Id,
                    FatherId = (i < 5 && i > 2) ? TestConstants.ID_FatherId : string.Empty,
                    MotherId = (i < 5 && i > 2) ? TestConstants.ID_MotherId : string.Empty
                });
            }

            return individuals;
        }

        protected override Individual NewEntity()
        {
            return new Individual { FirstName = "Foo", LastName = "Bar" };
        }

        protected override Individual UpdateEntity()
        {
            return new Individual { Id = TestConstants.ID_Exists, FirstName = "Foo", LastName = "Bar" };
        }
    }
}