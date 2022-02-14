using SmartFamily.Gedcom.Common;
using SmartFamily.Gedcom.IO;
using SmartFamily.Gedcom.Records;
using SmartFamily.Gedcom.Tests.Common;

using System;
using System.IO;
using System.Reflection;
using System.Text;

using Xunit;

namespace SmartFamily.Gedcom.Tests
{
    public class GEDCOMWriterTests : GEDCOMTestBase
    {
        #region Protected Properties

        protected override string EmbeddedFilePath => "SmartFamily.Gedcom.Tests.TestFiles.GEDCOMWriterTests";

        #endregion Protected Properties

        #region Create

        [Fact]
        public void GEDCOMWriter_Create_Throws_Exception_If_Stream_Parameter_Is_Null()
        {
            Stream s = GetEmbeddedFileStream("InvalidFileName");
            Assert.Throws<ArgumentNullException>(() => GEDCOMWriter.Create(s));
        }

        [Fact]
        public void GEDCOMWriter_Create_Throws_Exception_If_TextWriter_Parameter_Is_Null()
        {
            StringWriter stringWriter = null;
            Assert.Throws<ArgumentNullException>(() => GEDCOMWriter.Create(stringWriter));
        }

        [Fact]
        public void GEDCOMWriter_Create_Throws_Exception_If_StringBuilder_Parameter_Is_Null()
        {
            StringBuilder sb = null;
            Assert.Throws<ArgumentNullException>(() => GEDCOMWriter.Create(sb));
        }

        [Fact]
        public void GEDCOMWriter_Create_Creates_Instance_Of_Writer_If_Stream_Parameter_Is_Valid()
        {
            // Arrange
            Stream s = new MemoryStream();
            GEDCOMWriter writer = GEDCOMWriter.Create(s);

            // Assert
            Assert.IsType<GEDCOMWriter>(writer);
        }

        [Fact]
        public void GEDCOMWriter_Create_Creates_Instance_Of_Writer_If_TextWriter_Parameter_Is_Valid()
        {
            // Arrange
            StringWriter stringWriter = new StringWriter(new StringBuilder());
            GEDCOMWriter writer = GEDCOMWriter.Create(stringWriter);

            // Assert
            Assert.IsType<GEDCOMWriter>(writer);
        }

        [Fact]
        public void GEDCOMWriter_Create_Creates_Instance_Of_Writer_If_StringBuilder_Parameter_Is_Valid()
        {
            // Arrange
            StringBuilder sb = new StringBuilder();
            GEDCOMWriter writer = GEDCOMWriter.Create(sb);

            // Assert
            Assert.IsType<GEDCOMWriter>(writer);
        }

        #endregion Create

        #region WriteData

        [Fact]
        public void GEDCOMWriter_WriteData_Correctly_Writes_Provided_Text()
        {
            // Arrange
            StringBuilder sb = new StringBuilder();
            GEDCOMWriter writer = GEDCOMWriter.Create(sb);

            // Act
            writer.WriteData("Data");

            // Assert
            Assert.Equal("Data", sb.ToString());
        }

        #endregion WriteData

        #region WriteId

        [Fact]
        public void GEDCOMWriter_WriteId_Correctly_Writes_Provided_Id()
        {
            // Arrange
            StringBuilder sb = new StringBuilder();
            GEDCOMWriter writer = GEDCOMWriter.Create(sb);

            // Act
            writer.WriteId("@I001@");

            // Assert
            Assert.Equal("@I001@", sb.ToString());
        }

        [Fact]
        public void GEDCOMWriter_WriteId_Correctly_Writes_Provided_Id_And_Prefix()
        {
            // Arrange
            StringBuilder sb = new StringBuilder();
            GEDCOMWriter writer = GEDCOMWriter.Create(sb);

            // Act
            writer.WriteId(1, "I");

            // Assert
            Assert.Equal("@I1@", sb.ToString());
        }

        #endregion WriteId

        #region WriteLevel

        [Fact]
        public void GEDCOMWriter_WriteLevel_Correctly_Writes_Provided_Level()
        {
            // Arrange
            StringBuilder sb = new StringBuilder();
            GEDCOMWriter writer = GEDCOMWriter.Create(sb);

            // Act
            writer.WriteLevel(2);

            // Assert
            Assert.Equal("2", sb.ToString());
        }

        #endregion WriteLevel

        #region WriteTag

        [Fact]
        public void GEDCOMWriter_WriteTag_Correctly_Writes_Provided_Tag()
        {
            // Arrange
            StringBuilder sb = new StringBuilder();
            GEDCOMWriter writer = GEDCOMWriter.Create(sb);

            // Act
            writer.WriteTag("INDI");

            // Assert
            Assert.Equal("INDI", sb.ToString());
        }

        [Fact]
        public void GEDCOMWriter_WriteTag_Correctly_Writes_Provided_TagName()
        {
            // Arrange
            StringBuilder sb = new StringBuilder();
            GEDCOMWriter writer = GEDCOMWriter.Create(sb);

            // Act
            writer.WriteTag(GEDCOMTag.INDI);

            // Assert
            Assert.Equal("INDI", sb.ToString());
        }

        #endregion WriteTag

        #region WriteXRefId

        [Fact]
        public void GEDCOMWriter_WriteXRefId_Correctly_Writes_Provided_Id()
        {
            // Arrange
            StringBuilder sb = new StringBuilder();
            GEDCOMWriter writer = GEDCOMWriter.Create(sb);

            // Act
            writer.WriteXRefId("@I001@");

            // Assert
            Assert.Equal("@I001@", sb.ToString());
        }

        [Fact]
        public void GEDCOMWriter_WriteXRefId_Correctly_Writes_Provided_Id_And_Prefix()
        {
            // Arrange
            StringBuilder sb = new StringBuilder();
            GEDCOMWriter writer = GEDCOMWriter.Create(sb);

            // Act
            writer.WriteXRefId(1, "I");

            // Assert
            Assert.Equal("@I1@", sb.ToString());
        }

        #endregion WriteXRefId

        #region WriteRecord

        [Fact]
        public void GEDCOMWriter_WriteRecord_Correctly_Writes_Provided_Level_Id_And_Tag()
        {
            // Arrange
            StringBuilder sb = new StringBuilder();
            GEDCOMWriter writer = GEDCOMWriter.Create(sb);
            writer.NewLine = "\n";

            // Act
            writer.WriteRecord("@I001@", 0, "", "INDI", "");

            // Assert
            Assert.Equal("0 @I001@ INDI\n", sb.ToString());
        }

        [Fact]
        public void GEDCOMWriter_WriteRecord_Correctly_Writes_Provided_Level_Tag_And_Data()
        {
            // Arrange
            StringBuilder sb = new StringBuilder();
            GEDCOMWriter writer = GEDCOMWriter.Create(sb);
            writer.NewLine = "\n";

            // Act
            writer.WriteRecord("", 1, "", "NAME", "John /Smith/");

            // Assert
            Assert.Equal("1 NAME John /Smith/\n", sb.ToString());
        }

        [Fact]
        public void GEDCOMWriter_WriteRecord_Correctly_Writes_Provided_Level_Tag_And_XRefId()
        {
            // Arrange
            StringBuilder sb = new StringBuilder();
            GEDCOMWriter writer = GEDCOMWriter.Create(sb);
            writer.NewLine = "\n";

            // Act
            writer.WriteRecord("", 2, "@N002@", "NOTE", "");

            // Assert
            Assert.Equal("2 NOTE @N002@\n", sb.ToString());
        }

        [Fact]
        public void GEDCOMWriter_WriterRecord_Correctly_Renders_Header_Record()
        {
            // Arrange
            StringBuilder sb = new StringBuilder();
            GEDCOMWriter writer = GEDCOMWriter.Create(sb);
            writer.NewLine = "\n";

            // Act
            GEDCOMHeaderRecord record = Util.CreateHeaderRecord("Header");

            // write Header
            writer.WriteRecord(record, false);

            // Assert
            Assert.Equal("0 HEAD\n", sb.ToString());
        }

        [Fact]
        public void GEDCOMWriter_WriterRecord_Correctly_Renders_Family_Record_And_Children()
        {
            // Arrange
            var sb = new StringBuilder();
            GEDCOMWriter writer = GEDCOMWriter.Create(sb);
            writer.NewLine = "\n";

            // Act
            GEDCOMFamilyRecord record = Util.CreateFamilyRecord(1);

            // write Header
            writer.WriteRecord(record);

            // Assert
            GEDCOMAssert.IsValidOutput(GetEmbeddedFileString("OneFamily"), sb);
        }

        [Fact]
        public void GEDCOMWriter_WriterRecord_Correctly_Renders_Header_Record_And_Children()
        {
            // Arrange
            var sb = new StringBuilder();
            GEDCOMWriter writer = GEDCOMWriter.Create(sb);
            writer.NewLine = "\n";

            // Act
            GEDCOMHeaderRecord record = Util.CreateHeaderRecord("Header");

            // write Header
            writer.WriteRecord(record);

            // Assert
            GEDCOMAssert.IsValidOutput(GetEmbeddedFileString("Header"), sb);
        }

        [Fact]
        public void GEDCOMWriter_WriterRecord_Correctly_Renders_Individual_Record_And_Children()
        {
            // Arrange
            var sb = new StringBuilder();
            GEDCOMWriter writer = GEDCOMWriter.Create(sb);
            writer.NewLine = "\n";

            // Act
            GEDCOMIndividualRecord record = Util.CreateIndividualRecord(1);

            // write Header
            writer.WriteRecord(record);

            // Assert
            GEDCOMAssert.IsValidOutput(GetEmbeddedFileString("OneIndividual"), sb);
        }

        #endregion WriteRecord

        #region WriteRecords

        [Fact]
        public void GEDCOMWriter_WriterRecords_Correctly_Renders_Two_Family_Records()
        {
            // Arrange
            var sb = new StringBuilder();
            GEDCOMWriter writer = GEDCOMWriter.Create(sb);
            writer.NewLine = "\n";

            // Act
            GEDCOMRecordList individuals = Util.CreateFamilyRecords();

            // write Individuals
            writer.WriteRecords(individuals);

            // Assert
            GEDCOMAssert.IsValidOutput(GetEmbeddedFileString("TwoFamilies"), sb);
        }

        [Fact]
        public void GEDCOMWriter_WriterRecords_Correctly_Renders_Two_Individual_Records()
        {
            // Arrange
            var sb = new StringBuilder();
            GEDCOMWriter writer = GEDCOMWriter.Create(sb);
            writer.NewLine = "\n";

            // Act
            GEDCOMRecordList individuals = Util.CreateIndividualRecords();

            // write Individuals
            writer.WriteRecords(individuals);

            // Assert
            GEDCOMAssert.IsValidOutput(GetEmbeddedFileString("TwoIndividuals"), sb);
        }

        #endregion WriteRecords

        protected override Stream GetEmbeddedFileStream(string fileName)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(GetEmbeddedFileName(fileName));
        }
    }
}