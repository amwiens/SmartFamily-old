using SmartFamily.TestUtilities.Common;

using System;
using System.IO;
using System.Linq;
using System.Reflection;

using Xunit;

namespace SmartFamily.Data.Tests
{
    public partial class GEDCOMStoreTests : GEDCOMTestBase
    {
        private readonly string _treeId = IndividualsResources.TreeId;

        #region Protected Properties

        protected override string EmbeddedFilePath => "SmartFamily.Data.Tests.TestFiles";

        protected override string FilePath => SharedResources.GEDCOMTestFilePath;

        #endregion

        [Fact]
        public void GEDCOMStore_Constructor_Throws_On_Empty_Path()
        {
            Assert.Throws<ArgumentException>(() => { new GEDCOMStore(""); });
        }

        [Theory]
        [InlineData("NoRecords", 0)]
        [InlineData("OneIndividual", 1)]
        [InlineData("TwoIndividuals", 2)]
        public void GEDCOMStore_Constructor_Loads_Individuals_Property(string fileName, int recordCount)
        {
            // Arrange
            const string testFile = "Constructor.ged";
            var db = CreateStore($"{fileName}.ged", testFile);

            var inds = db.Individuals;
            Assert.Equal(recordCount, inds.Count);
        }

        [Theory]
        [InlineData("NoRecords", 0)]
        [InlineData("OneFamily", 1)]
        [InlineData("TwoFamilies", 2)]
        public void GEDCOMStore_Constructor_Loads_Families_Property(string fileName, int recordCount)
        {
            // Arrange
            const string testFile = "Constructor.ged";
            var db = CreateStore($"{fileName}.ged", testFile);

            var families = db.Families;
            Assert.Equal(recordCount, families.Count);
        }

        [Fact]
        public void GEDCOMStore_Constructor_Creates_Family_Links()
        {
            // Arrange
            const string testFile = "Constructor.ged";
            const string fileName = "BindingTest";
            var db = CreateStore($"{fileName}.ged", testFile);

            // Act
            var testIndividual = db.Individuals.SingleOrDefault(ind => ind.Id == "1");

            // Assert
            if (testIndividual != null)
            {
                Assert.Equal("John", testIndividual.FirstName);
                Assert.Equal("Smith", testIndividual.LastName);
                Assert.Equal("2", testIndividual.FatherId);
                Assert.Equal("3", testIndividual.MotherId);
            }
        }

        #region Other Helpers

        protected override Stream GetEmbeddedFileStream(string fileName)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(GetEmbeddedFileName(fileName));
        }

        private GEDCOMStore CreateStore(string file, string test)
        {
            string fileName = Path.Combine(FilePath, file);
            string testFile = Path.Combine(FilePath, test);
            File.Copy(fileName, testFile, true);

            return new GEDCOMStore(testFile);
        }

        #endregion
    }
}