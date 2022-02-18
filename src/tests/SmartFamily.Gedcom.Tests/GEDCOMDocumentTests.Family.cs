using SmartFamily.TestUtilities.Common;

using System;
using System.Linq;

using Xunit;

namespace SmartFamily.Gedcom.Tests
{
    public partial class GEDCOMDocumentTests : GEDCOMTestBase
    {
        #region SelectChildsFamilyRecord

        [Theory]
        [InlineData("OneFamily", null)]
        [InlineData("OneFamily", "")]
        public void GEDCOMDocument_SelectChildsFamilyRecord_Throws_On_Null_Or_Empty_ChildId(string fileName, string childId)
        {
            // Arrange
            var document = new GEDCOMDocument();
            document.LoadGEDCOM(GetEmbeddedFileString(fileName));

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => document.SelectChildsFamilyRecord(childId));
        }

        [Theory]
        [InlineData("OneFamily", "@I3@", "@F1@")]
        [InlineData("TwoFamilies", "@I5@", "@F1@")]
        [InlineData("ThreeFamilies", "@I6@", "@F2@")]
        public void GEDCOMDocument_SelectChildsFamilyRecord_Returns_Family_When_Given_Valid_ChildId(string fileName, string childId, string familyId)
        {
            // Arrange
            var document = new GEDCOMDocument();
            document.LoadGEDCOM(GetEmbeddedFileString(fileName));

            // Act
            var record = document.SelectChildsFamilyRecord(childId);

            // Assert
            Assert.NotNull(record);
            Assert.Equal(record.Id, familyId);
        }

        [Theory]
        [InlineData("OneFamily", "@I4@")]
        [InlineData("TwoFamilies", "@I6@")]
        [InlineData("ThreeFamilies", "@I7@")]
        public void GEDCOMDocument_SelectChildsFamilyRecord_Returns_Null_When_Given_InValid_ChildId(string fileName, string childId)
        {
            // Arrange
            var document = new GEDCOMDocument();
            document.LoadGEDCOM(GetEmbeddedFileString(fileName));

            // Act
            var record = document.SelectChildsFamilyRecord(childId);

            // Assert
            Assert.Null(record);
        }

        #endregion SelectChildsFamilyRecord

        #region SelectFamilyRecord

        [Theory]
        [InlineData("OneFamily", "@F1@")]
        [InlineData("TwoFamilies", "@F1@")]
        [InlineData("TwoFamilies", "@F2@")]
        public void GEDCOMDocument_SelectFamilyRecord_Returns_Family_When_Given_Valid_Id(string fileName, string familyId)
        {
            // Arrange
            var document = new GEDCOMDocument();
            document.LoadGEDCOM(GetEmbeddedFileString(fileName));

            // Act
            var record = document.SelectFamilyRecord(familyId);

            // Assert
            Assert.NotNull(record);
            Assert.Equal(record.Id, familyId);
        }

        [Theory]
        [InlineData("OneFamily", "@F2@")]
        [InlineData("TwoFamilies", "@F3@")]
        public void GEDCOMDocument_SelectFamilyRecord_Returns_Null_When_Given_Invalid_Id(string fileName, string familyId)
        {
            // Arrange
            var document = new GEDCOMDocument();
            document.LoadGEDCOM(GetEmbeddedFileString(fileName));

            // Act
            var record = document.SelectFamilyRecord(familyId);

            // Assert
            Assert.Null(record);
        }

        [Theory]
        [InlineData("OneFamily", null, "@I2@")]
        public void GEDCOMDocument_SelectFamilyRecord_Throws_On_Null_HusbandId(string fileName, string husbandId, string wifeId)
        {
            // Arrange
            var document = new GEDCOMDocument();
            document.LoadGEDCOM(GetEmbeddedFileString(fileName));

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => document.SelectFamilyRecord(husbandId, wifeId));
        }

        [Theory]
        [InlineData("OneFamily", "@I2@", null)]
        public void GEDCOMDocument_SelectFamilyRecord_Throws_On_Null_WifeId(string fileName, string husbandId, string wifeId)
        {
            // Arrange
            var document = new GEDCOMDocument();
            document.LoadGEDCOM(GetEmbeddedFileString(fileName));

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => document.SelectFamilyRecord(husbandId, wifeId));
        }

        [Theory]
        [InlineData("OneFamily", "@I1@", "@I2@", "@F1@")]
        [InlineData("TwoFamilies", "@I1@", "@I2@", "@F1@")]
        [InlineData("TwoFamilies", "@I3@", "@I4@", "@F2@")]
        public void GEDCOMDocument_SelectFamilyRecord_Returns_Family_When_Given_Valid_HusbandId_And_WifeId(string fileName, string husbandId, string wifeId, string familyId)
        {
            // Arrange
            var document = new GEDCOMDocument();
            document.LoadGEDCOM(GetEmbeddedFileString(fileName));

            // Act
            var record = document.SelectFamilyRecord(husbandId, wifeId);

            // Assert
            Assert.NotNull(record);
            Assert.Equal(record.Id, familyId);
        }

        [Theory]
        [InlineData("OneFamily", "@I3@", "@I4@")]
        [InlineData("TwoFamilies", "@I3@", "@I2@")]
        public void GEDCOMDocument_SelectFamilyRecord_Returns_Null_When_Given_Invalid_HusbandId_Or_WifeId(string fileName, string husbandId, string wifeId)
        {
            // Arrange
            var document = new GEDCOMDocument();
            document.LoadGEDCOM(GetEmbeddedFileString(fileName));

            // Act
            var record = document.SelectFamilyRecord(husbandId, wifeId);

            // Assert
            Assert.Null(record);
        }

        #endregion SelectFamilyRecord

        #region SelectFamilyRecords

        [Theory]
        [InlineData("OneFamily", null)]
        [InlineData("OneFamily", "")]
        public void GEDCOMDocument_SelectFamilyRecords_Throws_On_Null_Or_Empty_IndividualId(string fileName, string individualId)
        {
            // Arrange
            var document = new GEDCOMDocument();
            document.LoadGEDCOM(GetEmbeddedFileString(fileName));

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => document.SelectFamilyRecords(individualId));
        }

        [Theory]
        [InlineData("OneFamily", "@I1@", 1)]
        [InlineData("OneFamily", "@I2@", 1)]
        [InlineData("OneFamily", "@I3@", 0)]
        [InlineData("TwoFamilies", "@I3@", 1)]
        [InlineData("TwoFamilies", "@I6@", 0)]
        [InlineData("ThreeFamilies", "@I2@", 2)]
        public void GEDCOMDocument_SelectFamilyRecords_Returns_List_Of_Families(string fileName, string individualId, int recordCount)
        {
            // Arrange
            var document = new GEDCOMDocument();
            document.LoadGEDCOM(GetEmbeddedFileString(fileName));

            // Act
            var records = document.SelectFamilyRecords(individualId);

            // Assert
            Assert.Equal(recordCount, records.Count());
        }

        [Theory]
        [InlineData("OneFamily", "@I1@", "@F1@")]
        [InlineData("TwoFamilies", "@I1@", "@F1@")]
        [InlineData("TwoFamilies", "@I3@", "@F2@")]
        public void GEDCOMDocument_SelectFamilyRecords_Returns_Family_When_Given_Valid_HusbandId(string fileName, string husbandId, string familyId)
        {
            // Arrange
            var document = new GEDCOMDocument();
            document.LoadGEDCOM(GetEmbeddedFileString(fileName));

            // Act
            var record = document.SelectFamilyRecords(husbandId).SingleOrDefault();

            // Assert
            Assert.NotNull(record);
            Assert.Equal(record.Id, familyId);
        }

        [Theory]
        [InlineData("OneFamily", "@I2@", "@F1@")]
        [InlineData("TwoFamilies", "@I2@", "@F1@")]
        [InlineData("TwoFamilies", "@I4@", "@F2@")]
        public void GEDCOMDocument_SelectFamilyRecords_Returns_Family_When_Given_Valid_WifeId(string fileName, string wifeId, string familyId)
        {
            // Arrange
            var document = new GEDCOMDocument();
            document.LoadGEDCOM(GetEmbeddedFileString(fileName));
            string husbandId = string.Empty;

            // Act
            var record = document.SelectFamilyRecords(wifeId).SingleOrDefault();

            // Assert
            Assert.NotNull(record);
            Assert.Equal(record.Id, familyId);
        }

        #endregion SelectFamilyRecords
    }
}