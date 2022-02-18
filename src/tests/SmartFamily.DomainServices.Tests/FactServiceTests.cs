using SmarFamily.TestUtilities;

using SmartFamily.Core;
using SmartFamily.DomainServices.Services;

using System.Collections.Generic;

namespace SmartFamily.DomainServices.Tests
{
    public class FactServiceTests : EntityServiceBaseTests<Fact, FactService>
    {
        protected override IEnumerable<Fact> GetEntities(int count)
        {
            var facts = new List<Fact>();

            for (int i = 0; i < count; i++)
            {
                facts.Add(new Fact
                {
                    Id = i.ToString(),
                    Date = string.Format(TestConstants.EVN_Date, i),
                    Place = string.Format(TestConstants.EVN_Place, i),
                    TreeId = TestConstants.TREE_Id
                });
            }

            return facts;
        }

        protected override Fact NewEntity()
        {
            return new Fact { Date = "Foo", Place = "Bar" };
        }

        protected override Fact UpdateEntity()
        {
            return new Fact { Id = TestConstants.ID_Exists, Date = "Foo", Place = "Bar" };
        }
    }
}