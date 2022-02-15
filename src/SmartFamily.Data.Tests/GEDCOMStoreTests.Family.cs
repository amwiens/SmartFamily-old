using SmartFamily.Core;
using SmartFamily.Gedcom;
using SmartFamily.TestUtilities.Common;

using System;
using System.IO;
using System.Linq;

using Xunit;

namespace SmartFamily.Data.Tests
{
    public partial class GEDCOMStoreTests
    {
        #region AddFamily

        [Fact]
        public void GEDCOMStore_AddFamily_Should_Throw_On_Null_Family()
        {
            // Arrange
            string testFile = "AddFamily.ged";
            var db = CreateStore($"NoRecords.ged", testFile);

            // Assert
            Assert.Throws<ArgumentNullException>(() => db.AddFamily(null));
        }

        [Theory]
        [InlineData("NoRecords", 1)]
        [InlineData("OneFamily", 2)]
        [InlineData("TwoFamilies", 3)]
        public void GEDCOMStore_AddFamily_Should_Insert_The_Family_Into_The_Document(string fileName, int recordCount)
        {
            // Arrange
            string testFile = "AddFamily.ged";
            var db = CreateStore($"{fileName}.ged", testFile);
            Family newFamily = CreateTestFamily();

            // Act
            db.AddFamily(newFamily);
            db.SaveChanges();

            // Assert
            Assert.Equal(recordCount, GetFamilyCount(testFile));
        }

        [Theory]
        [InlineData("NoRecords", "1")]
        [InlineData("OneFamily", "2")]
        [InlineData("TwoFamilies", "3")]
        public void GEDCOMStore_AddFamily_Should_Return_The_Id_Of_The_Family(string fileName, string recordId)
        {
            // Arrange
            string testFile = "AddFamily.ged";
            var db = CreateStore($"{fileName}.ged", testFile);
            Family newFamily = CreateTestFamily();

            // Act
            db.AddFamily(newFamily);
            db.SaveChanges();

            // Assert
            Assert.Equal(recordId, newFamily.Id);
        }

        [Theory]
        [InlineData("TwoIndividuals", "1", "", "TwoIndividuals_AddFamilyAddHusband")]
        [InlineData("TwoIndividuals", "", "2", "TwoIndividuals_AddFamilyAddWife")]
        [InlineData("TwoIndividuals", "1", "2", "TwoIndividuals_AddFamilyAddHusbandAndWife")]
        public void GEDCOMStore_AddFamily_Should_Add_Husband_And_Wife(string fileName, string husbandId, string wifeId, string updatedFileName)
        {
            // Arrange
            const string testFile = "AddFamily.ged";
            var db = CreateStore($"{fileName}.ged", testFile);
            Family newFamily = CreateTestFamily();
            newFamily.HusbandId = husbandId;
            newFamily.WifeId = wifeId;

            // Act
            db.AddFamily(newFamily);
            db.SaveChanges();

            // Assert
            GEDCOMAssert.IsValidOutput(GetEmbeddedFileString(updatedFileName), GetFileString(testFile));
        }

        [Theory]
        [InlineData("TwoIndividuals", "1", "TwoIndividuals_AddFamilyAddChild1")]
        [InlineData("TwoIndividuals", "2", "TwoIndividuals_AddFamilyAddChild2")]
        public void GEDCOMStore_AddFamily_Should_Add_Child(string fileName, string childId, string updatedFileName)
        {
            // Arrange
            const string testFile = "AddFamily.ged";
            var db = CreateStore($"{fileName}.ged", testFile);
            var newFamily = CreateTestFamily();
            var child = CreateTestIndividual(childId);
            newFamily.Children.Add(child);

            // Act
            db.AddFamily(newFamily);
            db.SaveChanges();

            // Assert
            GEDCOMAssert.IsValidOutput(GetEmbeddedFileString(updatedFileName), GetFileString(testFile));
        }

        #endregion AddFamily

        #region DeleteFamily

        [Fact]
        public void GEDCOMStore_DeleteFamily_Should_Throw_On_Null_Family()
        {
            // Arrange
            const string testFile = "DeleteFamily.ged";
            var db = CreateStore($"NoRecords.ged", testFile);
            Family family = null;

            // Assert
            Assert.Throws<ArgumentNullException>(() => db.DeleteFamily(family));
        }

        [Theory]
        [InlineData("OneFamily", "1", 0)]
        [InlineData("TwoFamilies", "1", 1)]
        [InlineData("TwoFamilies", "2", 1)]
        public void GEDCOMStore_DeleteFamily_Should_Remove_The_Family_From_The_Document(string fileName, string idToDelete, int recordCount)
        {
            // Arrange
            const string testFile = "DeleteFamily.ged";
            var db = CreateStore($"{fileName}.ged", testFile);
            Family family = CreateTestFamily(idToDelete);

            // Act
            db.DeleteFamily(family);
            db.SaveChanges();

            // Assert
            Assert.Equal(recordCount, GetFamilyCount(testFile));
        }

        [Theory]
        [InlineData("OneFamily", "2", 1)]
        [InlineData("TwoFamilies", "3", 2)]
        public void GEDCOMStore_DeleteFamily_Should_Throw_If_Family_Not_In_Document(string fileName, string idToDelete, int recordCount)
        {
            // Arrange
            string testFile = "DeleteFamily.ged";
            var db = CreateStore($"{fileName}.ged", testFile);
            Family family = CreateTestFamily(idToDelete);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => db.DeleteFamily(family));
        }

        #endregion DeleteFamily

        #region UpdateFamily

        [Fact]
        public void GEDCOMStore_UpdateFamily_Should_Throw_On_Null_Family()
        {
            // Arrange
            string testFile = "UpdateFamily.ged";
            var db = CreateStore("NoRecords.ged", testFile);
            Family family = null;

            // Assert
            Assert.Throws<ArgumentNullException>(() => db.UpdateFamily(family));
        }

        [Theory]
        [InlineData("OneFamily", "1", "OneFamily_UpdateFamily")]
        [InlineData("TwoFamilies", "2", "TwoFamilies_UpdateFamily")]
        public void GEDCOMStore_UpdateFamily_Should_Update_Properties_Of_The_Family(string fileName, string idToUpdate, string updatedFileName)
        {
            // Arrange
            string testFile = "UpdateFamily.ged";
            var db = CreateStore($"{fileName}.ged", testFile);

            Family updateFamily = db.Families.Single(ind => ind.Id == idToUpdate);

            // Act
            db.UpdateFamily(updateFamily);
            db.SaveChanges();

            // Assert
            GEDCOMAssert.IsValidOutput(GetEmbeddedFileString(updatedFileName), GetFileString(testFile));
        }

        [Theory]
        [InlineData("OneFamily", "2", 1)]
        [InlineData("TwoFamilies", "3", 2)]
        public void GEDCOMStore_UpdateFamily_Should_Throw_If_Family_Not_In_Document(string fileName, string idToUpdate, int recordCount)
        {
            // Arrange
            string testFile = "UpdateFamily.ged";
            var db = CreateStore($"{fileName}.ged", testFile);
            Family family = CreateTestFamily(idToUpdate);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => db.UpdateFamily(family));
        }

        #endregion UpdateFamily

        #region Other Helpers

        private Family CreateTestFamily(string id = "")
        {
            // Create a test family
            var newFamily = new Family
            {
                Id = id,
                TreeId = _treeId
            };

            // Return the family
            return newFamily;
        }

        private int GetFamilyCount(string file)
        {
            string fileName = Path.Combine(FilePath, file);
            Stream testStream = new FileStream(fileName, FileMode.Open);
            var doc = new GEDCOMDocument();
            doc.Load(testStream);

            return doc.FamilyRecords.Count;
        }

        #endregion Other Helpers
    }
}