using SmarFamily.TestUtilities;

using SmartFamily.Core;
using SmartFamily.DomainServices.Services;

using System.Collections.Generic;

namespace SmartFamily.DomainServices.Tests
{
    public class CitationServiceTests : EntityServiceBaseTests<Citation, CitationService>
    {
        protected override IEnumerable<Citation> GetEntities(int count)
        {
            var citations = new List<Citation>();

            for (int i = 0; i < count; i++)
            {
                citations.Add(new Citation
                {
                    Id = i.ToString(),
                    Text = string.Format(TestConstants.CIT_Text, i),
                    Page = string.Format(TestConstants.CIT_Page, i),
                    TreeId = TestConstants.TREE_Id
                });
            }

            return citations;
        }

        protected override Citation NewEntity()
        {
            return new Citation { Text = "Foo", Page = "Bar" };
        }

        protected override Citation UpdateEntity()
        {
            return new Citation { Id = TestConstants.ID_Exists, Text = "Foo", Page = "Bar" };
        }
    }
}