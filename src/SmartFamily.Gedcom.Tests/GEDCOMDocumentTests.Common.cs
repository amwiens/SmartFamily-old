using SmartFamily.Gedcom.IO;
using SmartFamily.Gedcom.Records;
using SmartFamily.Gedcom.Tests.Common;
using SmartFamily.TestUtilities.Common;

using System;
using System.IO;
using System.Reflection;
using System.Text;

using Xunit;

namespace SmartFamily.Gedcom.Tests
{
    public partial class GEDCOMDocumentTests : GEDCOMTestBase
    {
        #region Protected Properties

        protected override string EmbeddedFilePath => "SmartFamily.Gedcom.Tests.TestFiles.GEDCOMDocumentTests";

        #endregion Protected Properties

        #region AddRecord

        [Fact]
        public void GEDCOMDocument_AddRecord_Throws_If_Record_IsNull()
        {
            // Arrange
            var document = new GEDCOMDocument();

            // Assert
            Assert.Throws<ArgumentNullException>(() => document.AddRecord(null));
        }

        [Fact]
        public void GEDCOMDocument_AddRecord_Adds_Record_To_Document()
        {
            // Arrange
            var document = new GEDCOMDocument();
            var record = new GEDCOMIndividualRecord("1");
            int count = document.Records.Count;

            // Act
            document.AddRecord(record);

            // Assert
            Assert.Equal(count + 1, document.Records.Count);
        }

        [Fact]
        public void GEDCOMDocument_AddREcord_Adds_Record_To_Individuals_Collection()
        {
            // Arrange
            var document = new GEDCOMDocument();
            var record = new GEDCOMIndividualRecord("1");
            int count = document.IndividualRecords.Count;

            // Act
            document.AddRecord(record);

            // Assert
            Assert.Equal(count + 1, document.IndividualRecords.Count);
        }

        [Fact]
        public void GEDCOMDocument_AddRecord_Adds_HeaderRecord()
        {
            // Arrange
            var document = new GEDCOMDocument();
            var record = new GEDCOMHeaderRecord();

            // Act
            document.AddRecord(record);

            // Assert
            Assert.NotNull(document.SelectHeader());
        }

        #endregion AddRecord

        #region AddRecords

        [Fact]
        public void GEDCOMDocument_AddRecords_Exception_If_RecordList_IsNull()
        {
            // Arrange
            var document = new GEDCOMDocument();

            // Assert
            Assert.Throws<ArgumentNullException>(() => document.AddRecords(null));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public void GEDCOMDocument_AddRecords_Add_Records_To_Document(int recordCount)
        {
            // Arrange
            var document = new GEDCOMDocument();
            var records = new GEDCOMRecordList();
            int count = document.Records.Count;

            for (int i = 1; i <= recordCount; i++)
            {
                records.Add(Util.CreateIndividualRecord(i));
            }

            // Act
            document.AddRecords(records);

            // Assert
            Assert.Equal(count + recordCount, document.Records.Count);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public void GEDCOMDocument_AddRecords_Add_Records_To_Individuals_Collection(int recordCount)
        {
            // Arrange
            var document = new GEDCOMDocument();
            var records = new GEDCOMRecordList();
            int count = document.Records.Count;

            for (int i = 1; i <= recordCount; i++)
            {
                records.Add(Util.CreateIndividualRecord(i));
            }

            // Act
            document.AddRecords(records);

            // Assert
            Assert.Equal(count + recordCount, document.IndividualRecords.Count);
        }

        #endregion AddRecords

        #region Load

        [Fact]
        public void GEDCOMDocument_Load_Throws_If_Stream_Parameter_IsNull()
        {
            // Arrange
            Stream s = GetEmbeddedFileStream("InvalidFileName");
            var document = new GEDCOMDocument();

            // Assert
            Assert.Throws<ArgumentNullException>(() => document.Load(s));
        }

        [Fact]
        public void GEDCOMDocument_Load_Throws_If_TextReader_Parameter_IsNull()
        {
            // Arrange
            TextReader reader = null;
            var document = new GEDCOMDocument();

            // Assert
            Assert.Throws<ArgumentNullException>(() => document.Load(reader));
        }

        [Fact]
        public void GEDCOMDocument_Load_Throws_If_GEDCOMReader_Parameter_IsNull()
        {
            // Arrange
            GEDCOMReader reader = null;
            var document = new GEDCOMDocument();

            // Assert
            Assert.Throws<ArgumentNullException>(() => document.Load(reader));
        }

        [Theory]
        [InlineData("NoRecords", 0)]
        [InlineData("OneIndividual", 1)]
        [InlineData("TwoIndividuals", 2)]
        public void GEDCOMDocument_Load_Loads_Document_With_Individuals_Using_Stream(string fileName, int recordCount)
        {
            // Arrange
            var document = new GEDCOMDocument();

            // Act
            using (Stream s = GetEmbeddedFileStream(fileName))
            {
                document.Load(s);
            }

            // Assert
            Assert.Equal(recordCount, document.IndividualRecords.Count);
        }

        [Theory]
        [InlineData("NoRecords", 0)]
        [InlineData("OneIndividual", 1)]
        [InlineData("TwoIndividuals", 2)]
        public void GEDCOMDocument_Load_Loads_Document_With_Individuals_Using_TextReader(string fileName, int recordCount)
        {
            // Arrange
            var document = new GEDCOMDocument();

            // Act
            using (Stream s = GetEmbeddedFileStream(fileName))
            {
                document.Load(new StreamReader(s));
            }

            // Assert
            Assert.Equal(recordCount, document.IndividualRecords.Count);
        }

        [Theory]
        [InlineData("NoRecords")]
        public void GEDCOMDocument_Load_Loads_Document_With_Header_If_Document_Is_WellFormed(string fileName)
        {
            // Arrange
            var document = new GEDCOMDocument();

            // Act
            using (Stream s = GetEmbeddedFileStream(fileName))
            {
                document.Load(new StreamReader(s));
            }

            // Assert
            GEDCOMAssert.IsValidHeader(document.SelectHeader());
            GEDCOMAssert.HeaderIsEqual(Util.CreateHeaderRecord(fileName), document.SelectHeader());
        }

        #endregion Load

        #region LoadGEDCOM

        [Theory]
        [InlineData("NoRecords", 0)]
        [InlineData("OneIndividual", 1)]
        [InlineData("TwoIndividuals", 2)]
        public void GEDCOMDocument_LoadGEDCOM_Loads_Document_With_Individuals(string fileName, int recordCount)
        {
            // Arrange
            var document = new GEDCOMDocument();

            // Act
            document.LoadGEDCOM(GetEmbeddedFileString(fileName));

            // Assert
            Assert.Equal(recordCount, document.IndividualRecords.Count);
        }

        #endregion LoadGEDCOM

        #region RemoveRecord

        [Fact]
        public void GEDCOMDocument_RemoveRecord_Throws_If_Record_IsNull()
        {
            // Arrange
            var document = new GEDCOMDocument();

            // Assert
            Assert.Throws<ArgumentNullException>(() => document.RemoveRecord(null));
        }

        [Fact]
        public void GEDCOMDocument_RemoveRecord_Removes_Record_From_Document()
        {
            // Arrange
            var document = new GEDCOMDocument();
            document.AddRecord(Util.CreateHeaderRecord("Header"));
            for (int i = 1; i <= 2; i++)
            {
                document.AddRecord(Util.CreateIndividualRecord(i));
            }

            var record = document.IndividualRecords[1] as GEDCOMIndividualRecord;

            // Act
            document.RemoveRecord(record);

            // Assert
            Assert.Equal(2, document.Records.Count);
        }

        [Fact]
        public void GEDCOMDocument_RemoveRecord_Removes_Record_From_Individuals_Collection()
        {
            // Arrange
            var document = new GEDCOMDocument();
            document.AddRecord(Util.CreateHeaderRecord("Header"));
            for (int i = 1; i <= 2; i++)
            {
                document.AddRecord(Util.CreateIndividualRecord(i));
            }

            var record = document.IndividualRecords[1] as GEDCOMIndividualRecord;

            // Act
            document.RemoveRecord(record);

            // Assert
            Assert.Equal(1, document.IndividualRecords.Count);
        }

        [Fact]
        public void GEDCOMDocument_RemoveRecord_Throws_If_Record_Not_Present()
        {
            // Arrange
            var document = new GEDCOMDocument();
            document.AddRecord(Util.CreateHeaderRecord("Header"));
            for (int i = 1; i <= 2; i++)
            {
                document.AddRecord(Util.CreateIndividualRecord(i));
            }

            var record = Util.CreateIndividualRecord(3);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => document.RemoveRecord(record));
        }

        #endregion RemoveRecord

        #region Save

        [Fact]
        public void GEDCOMDocument_Save_If_Stream_Parameter_IsNull()
        {
            // Arrange
            var s = GetEmbeddedFileStream("InvalidFileName");
            var document = new GEDCOMDocument();

            // Assert
            Assert.Throws<ArgumentNullException>(() => document.Save(s));
        }

        [Fact]
        public void GEDCOMDocument_Save_If_TextWriter_Parameter_IsNull()
        {
            // Arrange
            TextWriter writer = null;
            var document = new GEDCOMDocument();

            // Assert
            Assert.Throws<ArgumentNullException>(() => document.Save(writer));
        }

        [Fact]
        public void GEDCOMDocument_Save_If_GEDCOMWriter_Parameter_IsNull()
        {
            // Arrange
            GEDCOMWriter writer = null;
            var document = new GEDCOMDocument();

            // Assert
            Assert.Throws<ArgumentNullException>(() => document.Save(writer));
        }

        [Theory]
        [InlineData("NoRecordsSave", 0)]
        [InlineData("OneIndividualSave", 1)]
        [InlineData("TwoIndividualsSave", 2)]
        public void GEDCOMDocument_Save_Saves_Document_Using_GEDCOMWriter(string fileName, int recordCount)
        {
            // Arrange
            var document = new GEDCOMDocument();
            document.AddRecord(Util.CreateHeaderRecord(fileName));
            for (int i = 1; i <= recordCount; i++)
            {
                document.AddRecord(Util.CreateIndividualRecord(i));
            }

            var sb = new StringBuilder();
            var writer = GEDCOMWriter.Create(sb);
            writer.NewLine = "\n";

            // Act
            document.Save(writer);

            // Assert
            GEDCOMAssert.IsValidOutput(GetEmbeddedFileString(fileName), sb);
        }

        [Theory]
        [InlineData("NoRecordsSave", 0)]
        [InlineData("OneIndividualSave", 1)]
        [InlineData("TwoIndividualsSave", 2)]
        public void GEDCOMDocument_Save_Saves_Document_Using_TextWriter(string fileName, int recordCount)
        {
            // Arrange
            var document = new GEDCOMDocument();
            document.AddRecord(Util.CreateHeaderRecord(fileName));
            for (int i = 1; i <= recordCount; i++)
            {
                document.AddRecord(Util.CreateIndividualRecord(i));
            }

            var sb = new StringBuilder();
            var writer = new StringWriter(sb);
            writer.NewLine = "\n";

            // Act
            document.Save(writer);

            // Assert
            GEDCOMAssert.IsValidOutput(GetEmbeddedFileString(fileName), sb);
        }

        [Theory]
        [InlineData("NoRecordsSave", 0)]
        [InlineData("OneIndividualSave", 1)]
        [InlineData("TwoIndividualsSave", 2)]
        public void GEDCOMDocument_Save_Saves_Document_Regardless_Of_Record_Order_And_Type(string fileName, int recordCount)
        {
            // Arrange
            var document = new GEDCOMDocument();
            if (recordCount > 0)
            {
                document.AddRecord(Util.CreateIndividualRecord(1));
            }

            document.AddRecord(Util.CreateHeaderRecord(fileName));

            if (recordCount > 1)
            {
                document.AddRecord(Util.CreateIndividualRecord(2));
            }

            var sb = new StringBuilder();
            var writer = GEDCOMWriter.Create(sb);
            writer.NewLine = "\n";

            // Act
            document.Save(writer);

            // Assert
            GEDCOMAssert.IsValidOutput(GetEmbeddedFileString(fileName), sb);
        }

        #endregion Save

        #region SaveGEDCOM

        [Theory]
        [InlineData("NoRecordsSave", 0, 0)]
        [InlineData("OneIndividualSave", 1, 0)]
        [InlineData("TwoIndividualsSave", 2, 0)]
        [InlineData("OneFamilySave", 2, 1)]
        [InlineData("TwoFamiliesSave", 3, 2)]
        public void GEDCOMDocument_SaveGEDCOM_Saves_Document(string fileName, int individualCount, int familyCount)
        {
            // Arrange
            var document = new GEDCOMDocument();
            document.AddRecord(Util.CreateHeaderRecord(fileName));
            for (int i = 1; i <= individualCount; i++)
            {
                document.AddRecord(Util.CreateIndividualRecord(i));
            }
            for (int i = 1; i <= familyCount; i++)
            {
                document.AddRecord(Util.CreateFamilyRecord(i));
            }

            // Assert
            GEDCOMAssert.IsValidOutput(GetEmbeddedFileString(fileName), document.SaveGEDCOM());
        }

        #endregion SaveGEDCOM

        protected override Stream GetEmbeddedFileStream(string fileName)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(GetEmbeddedFileName(fileName));
        }
    }
}