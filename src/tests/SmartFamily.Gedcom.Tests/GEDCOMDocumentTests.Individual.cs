using SmartFamily.TestUtilities.Common;

using Xunit;

namespace SmartFamily.Gedcom.Tests
{
    public partial class GEDCOMDocumentTests : GEDCOMTestBase
    {
        #region SelectIndividualRecord

        [Theory]
        [InlineData("OneIndividual", 1)]
        [InlineData("TwoIndividuals", 1)]
        [InlineData("TwoIndividuals", 2)]
        public void GEDCOMDocument_SelectIndividualRecord_Returns_Individual_When_Given_Valid_Id(string fileName, int recordNo)
        {
            // Arrange
            var document = new GEDCOMDocument();
            document.LoadGEDCOM(GetEmbeddedFileString(fileName));
            string id = $"@I{recordNo}@";

            // Act
            var record = document.SelectIndividualRecord(id);

            // Assert
            Assert.NotNull(record);
            Assert.Equal(record.Id, id);
        }

        [Theory]
        [InlineData("OneIndividual", 2)]
        [InlineData("TwoIndividuals", 3)]
        public void GEDCOMDocument_SelectIndividualRecord_Returns_Null_When_Given_Invalid_Id(string fileName, int recordNo)
        {
            // Arrange
            var document = new GEDCOMDocument();
            document.LoadGEDCOM(GetEmbeddedFileString(fileName));
            string id = $"@I{recordNo}@";

            // Act
            var record = document.SelectIndividualRecord(id);

            // Assert
            Assert.Null(record);
        }

        #endregion SelectIndividualRecord
    }
}