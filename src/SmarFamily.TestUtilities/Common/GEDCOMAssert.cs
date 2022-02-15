using SmartFamily.Gedcom.Common;
using SmartFamily.Gedcom.Records;
using SmartFamily.Gedcom.Structures;

using System.Text;

using Xunit;

namespace SmartFamily.TestUtilities.Common
{
    public class GEDCOMAssert
    {
        public static void EventStructureIsEqual(GEDCOMEventStructure expectedEventStructure, GEDCOMEventStructure actualEventStructure)
        {
            Assert.Equal(expectedEventStructure.Date, actualEventStructure.Date);
            Assert.IsType<GEDCOMPlaceStructure>(actualEventStructure.Place);
            Assert.Equal(expectedEventStructure.Place.Place, actualEventStructure.Place.Place);
        }

        public static void FamilyIsEqual(GEDCOMFamilyRecord expectedRecord, GEDCOMFamilyRecord actualRecord)
        {
            Assert.Equal(expectedRecord.Id, actualRecord.Id);

            // Assert values for FamilyRecord
            Assert.Equal(expectedRecord.Husband, actualRecord.Husband);
            Assert.Equal(expectedRecord.Wife, actualRecord.Wife);
            Assert.Equal(expectedRecord.NoChildren, actualRecord.NoChildren);
            Assert.Equal(expectedRecord.Events.Count, actualRecord.Events.Count);

            for (int i = 0; i < expectedRecord.Events.Count; i++)
            {
                EventStructureIsEqual(expectedRecord.Events[i], actualRecord.Events[i]);
            }
        }

        public static void HeaderIsEqual(GEDCOMHeaderRecord expectedRecord, GEDCOMHeaderRecord actualRecord)
        {
            HeaderSourceStructureIsEqual(expectedRecord.Source, actualRecord.Source);
            Assert.Equal(expectedRecord.Destination, actualRecord.Destination);
            Assert.Equal(expectedRecord.TransmissionDate, actualRecord.TransmissionDate);
            Assert.Equal(expectedRecord.CharacterSet, actualRecord.CharacterSet);
            Assert.Equal(expectedRecord.FileName, actualRecord.FileName);
            Assert.Equal(expectedRecord.GEDCOMForm, actualRecord.GEDCOMForm);
            Assert.Equal(expectedRecord.GEDCOMVersion, actualRecord.GEDCOMVersion);
            Assert.Equal(expectedRecord.Submitter, actualRecord.Submitter);
        }

        public static void HeaderSourceStructureIsEqual(GEDCOMHeaderSourceStructure expectedSourceStructure, GEDCOMHeaderSourceStructure actualSourceStructure)
        {
            Assert.Equal(expectedSourceStructure.SystemId, actualSourceStructure.SystemId);
            Assert.Equal(expectedSourceStructure.Version, actualSourceStructure.Version);
            Assert.Equal(expectedSourceStructure.ProductName, actualSourceStructure.ProductName);
            Assert.Equal(expectedSourceStructure.Company, actualSourceStructure.Company);
            Assert.Equal(expectedSourceStructure.Address.Address, actualSourceStructure.Address.Address);
        }

        public static void IndividualIsEqual(GEDCOMIndividualRecord expectedRecord, GEDCOMIndividualRecord actualRecord)
        {
            Assert.Equal(expectedRecord.Id, actualRecord.Id);
            //Assert.True(actualRecord.HasChildren);

            // Assert values for IndividualRecord
            Assert.Equal(expectedRecord.Name.GivenName, actualRecord.Name.GivenName);
            Assert.Equal(expectedRecord.Name.LastName, actualRecord.Name.LastName);
            Assert.Equal(expectedRecord.Sex, actualRecord.Sex);
            Assert.Equal(expectedRecord.Events.Count, actualRecord.Events.Count);

            for (int i = 0; i < expectedRecord.Events.Count; i++)
            {
                EventStructureIsEqual(expectedRecord.Events[i], actualRecord.Events[i]);
            }
        }

        public static void IsValidHeader(GEDCOMHeaderRecord actualRecord)
        {
            Assert.Equal(GEDCOMTag.HEAD, actualRecord.TagName);
            Assert.Equal(0, actualRecord.Level);
            Assert.True(actualRecord.HasChildren);
        }

        public static void IsValidFamily(GEDCOMFamilyRecord actualRecord)
        {
            Assert.Equal(GEDCOMTag.FAM, actualRecord.TagName);
            Assert.Equal(0, actualRecord.Level);
        }

        public static void IsValidIndividual(GEDCOMIndividualRecord actualRecord)
        {
            Assert.Equal(GEDCOMTag.INDI, actualRecord.TagName);
            Assert.Equal(0, actualRecord.Level);
        }

        public static void IsValidRecord(GEDCOMRecord record, int level, string data, bool hasChildren, int childCount)
        {
            Assert.NotNull(record);
            Assert.Equal(level, record.Level);
            Assert.Equal(data, record.Data);

            if (hasChildren)
            {
                Assert.True(record.HasChildren);
                Assert.Equal(childCount, record.ChildRecords.Count);
            }
            else
            {
                Assert.False(record.HasChildren);
            }
        }

        public static void IsValidOutput(string expectedString, StringBuilder actualSb)
        {
            IsValidOutput(expectedString, actualSb.ToString());
        }

        public static void IsValidOutput(string expectedString, string actualString)
        {
            string[] actual = actualString.Split('\n');
            string[] expected = expectedString.Split('\n');

            Assert.Equal(expected.Length, actual.Length);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }
    }
}