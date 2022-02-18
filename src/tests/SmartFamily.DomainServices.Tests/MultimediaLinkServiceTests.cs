using SmarFamily.TestUtilities;

using SmartFamily.Core;
using SmartFamily.DomainServices.Services;

using System.Collections.Generic;

namespace SmartFamily.DomainServices.Tests
{
    public class MultimediaLinkServiceTests : EntityServiceBaseTests<MultimediaLink, MultimediaLinkService>
    {
        protected override IEnumerable<MultimediaLink> GetEntities(int count)
        {
            var multimediaLinks = new List<MultimediaLink>();

            for (int i = 0; i < count; i++)
            {
                multimediaLinks.Add(new MultimediaLink
                {
                    Id = i.ToString(),
                    File = "Foo",
                    TreeId = TestConstants.TREE_Id
                });
            }

            return multimediaLinks;
        }

        protected override MultimediaLink NewEntity()
        {
            return new MultimediaLink { File = "Foo" };
        }

        protected override MultimediaLink UpdateEntity()
        {
            return new MultimediaLink { Id = TestConstants.ID_Exists, File = "Foo" };
        }
    }
}