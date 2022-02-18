using SmarFamily.TestUtilities;

using SmartFamily.Core;
using SmartFamily.DomainServices.Services;

using System.Collections.Generic;

namespace SmartFamily.DomainServices.Tests
{
    public class SourceServiceTests : EntityServiceBaseTests<Source, SourceService>
    {
        protected override IEnumerable<Source> GetEntities(int count)
        {
            var sources = new List<Source>();

            for (int i = 0; i < count; i++)
            {
                sources.Add(new Source
                {
                    Id = i.ToString(),
                    Author = string.Format(TestConstants.SRC_Author, i),
                    Title = string.Format(TestConstants.SRC_Title, i),
                    TreeId = TestConstants.TREE_Id
                });
            }

            return sources;
        }

        protected override Source NewEntity()
        {
            return new Source { Author = "Foo", Title = "Bar" };
        }

        protected override Source UpdateEntity()
        {
            return new Source { Id = TestConstants.ID_Exists, Author = "Foo", Title = "Bar" };
        }
    }
}