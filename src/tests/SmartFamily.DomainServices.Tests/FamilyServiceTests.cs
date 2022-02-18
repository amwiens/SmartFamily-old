using SmarFamily.TestUtilities;

using SmartFamily.Core;
using SmartFamily.DomainServices.Services;

using System.Collections.Generic;

namespace SmartFamily.DomainServices.Tests
{
    public class FamilyServiceTests : EntityServiceBaseTests<Family, FamilyService>
    {
        protected override IEnumerable<Family> GetEntities(int count)
        {
            var families = new List<Family>();

            for (int i = 0; i < count; i++)
            {
                families.Add(new Family
                {
                    Id = i.ToString(),
                    WifeId = (i < 5 && i > 2) ? TestConstants.ID_WifeId : string.Empty,
                    HusbandId = (i < 5 && i > 2) ? TestConstants.ID_HusbandId : string.Empty,
                    TreeId = TestConstants.TREE_Id
                });
            }

            return families;
        }

        protected override Family NewEntity()
        {
            return new Family
            {
                WifeId = TestConstants.ID_WifeId,
                HusbandId = TestConstants.ID_HusbandId
            };
        }

        protected override Family UpdateEntity()
        {
            return new Family
            {
                Id = TestConstants.ID_Exists,
                WifeId = TestConstants.ID_WifeId,
                HusbandId = TestConstants.ID_HusbandId
            };
        }
    }
}