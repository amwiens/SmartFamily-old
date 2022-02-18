using SmarFamily.TestUtilities;

using SmartFamily.Core;
using SmartFamily.DomainServices.Services;

using System.Collections.Generic;

namespace SmartFamily.DomainServices.Tests
{
    public class RepositoryServiceTests : EntityServiceBaseTests<Repository, RepositoryService>
    {
        protected override IEnumerable<Repository> GetEntities(int count)
        {
            var repositories = new List<Repository>();

            for (int i = 0; i < count; i++)
            {
                repositories.Add(new Repository
                {
                    Id = i.ToString(),
                    Name = string.Format(TestConstants.REP_Name, i),
                    Address = string.Format(TestConstants.REP_Address, i),
                    TreeId = TestConstants.TREE_Id
                });
            }

            return repositories;
        }

        protected override Repository NewEntity()
        {
            return new Repository { Name = "Foo", Address = "Bar" };
        }

        protected override Repository UpdateEntity()
        {
            return new Repository { Id = TestConstants.ID_Exists, Name = "Foo", Address = "Bar" };
        }
    }
}