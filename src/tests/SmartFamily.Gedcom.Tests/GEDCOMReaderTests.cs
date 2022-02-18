using SmartFamily.Gedcom.Common;
using SmartFamily.Gedcom.IO;
using SmartFamily.Gedcom.Records;
using SmartFamily.Gedcom.Tests.Common;
using SmartFamily.TestUtilities.Common;

using System;
using System.IO;
using System.Reflection;

using Xunit;

namespace SmartFamily.Gedcom.Tests
{
    public class GEDCOMReaderTests : GEDCOMTestBase
    {
        #region Protected Properties

        protected override string EmbeddedFilePath => "SmartFamily.Gedcom.Tests.TestFiles.GEDCOMReaderTests";

        #endregion Protected Properties

        #region Create

        [Fact]
        public void GEDCOMReader_Create_Throws_Exception_If_Stream_Parameter_Is_Null()
        {
            Stream s = GetEmbeddedFileStream("INvalidFileName");
            Assert.Throws<ArgumentNullException>(() => GEDCOMReader.Create(s));
        }

        [Fact]
        public void GEDCOMReader_Create_Throws_Exception_If_TextReader_Parameter_Is_Null()
        {
            StringReader reader = null;
            Assert.Throws<ArgumentNullException>(() => GEDCOMReader.Create(reader));
        }

        [Fact]
        public void GEDCOMReader_Create_Throws_Exception_If_String_Parameter_Is_Null()
        {
            string text = null;
            Assert.Throws<ArgumentNullException>(() => GEDCOMReader.Create(text));
        }

        #endregion Create

        #region MoveToFamily

        [Fact]
        public void GEDCOMReader_MoveToFamily_Returns_False_If_No_Family_Record()
        {
            GEDCOMReader reader;
            using (Stream s = GetEmbeddedFileStream("NoRecords"))
            {
                reader = GEDCOMReader.Create(s);
                bool moveResult = reader.MoveToFamily();

                // Assert that Move failed
                Assert.False(moveResult);
            }
        }

        [Fact]
        public void GEDCOMReader_MoveToFamily_Returns_True_If_At_Least_One_Family_Record()
        {
            GEDCOMReader reader;
            using (Stream s = GetEmbeddedFileStream("OneFamily"))
            {
                reader = GEDCOMReader.Create(s);
                bool moveResult = reader.MoveToFamily();

                // Assert that Move was successful
                Assert.True(moveResult);
            }
        }

        [Fact]
        public void GEDCOMReader_MoveToFamily_Returns_False_If_No_More_Family_Records()
        {
            GEDCOMReader reader;
            using (Stream s = GetEmbeddedFileStream("OneFamily"))
            {
                reader = GEDCOMReader.Create(s);
                bool moveResult = reader.MoveToFamily();

                reader.ReadRecord();
                moveResult = reader.MoveToFamily();

                // Assert that Move failed
                Assert.False(moveResult);
            }
        }

        #endregion MoveToFamily

        #region MoveToHeader

        [Fact]
        public void GEDCOMReader_MoveToHeader_Returns_False_If_No_Header_Record()
        {
            GEDCOMReader reader;
            using (Stream s = GetEmbeddedFileStream("Empty"))
            {
                reader = GEDCOMReader.Create(s);
                bool moveResult = reader.MoveToHeader();

                // Assert that Move failed
                Assert.False(moveResult);
            }
        }

        [Fact]
        public void GEDCOMReader_MoveToHeader_Returns_True_If_At_Least_One_Header_Record()
        {
            GEDCOMReader reader;
            using (Stream s = GetEmbeddedFileStream("OneIndividual"))
            {
                reader = GEDCOMReader.Create(s);
                bool moveResult = reader.MoveToHeader();

                // Assert that Move was successful
                Assert.True(moveResult);
            }
        }

        [Fact]
        public void GEDCOMReader_MoveToHeader_Returns_False_If_Past_Header()
        {
            GEDCOMReader reader;
            using (Stream s = GetEmbeddedFileStream("OneIndividual"))
            {
                reader = GEDCOMReader.Create(s);
                bool moveResult = reader.MoveToIndividual();

                reader.ReadRecord();
                moveResult = reader.MoveToHeader();

                // Assert that Move failed
                Assert.False(moveResult);
            }
        }

        #endregion MoveToHeader

        #region MoveToIndividual

        [Fact]
        public void GEDCOMReader_MoveToIndividual_Returns_False_If_No_Individual_Record()
        {
            GEDCOMReader reader;
            using (Stream s = GetEmbeddedFileStream("NoRecords"))
            {
                reader = GEDCOMReader.Create(s);
                bool moveResult = reader.MoveToIndividual();

                // Assert that Move failed
                Assert.False(moveResult);
            }
        }

        [Fact]
        public void GEDCOMReader_MoveToIndividual_Returns_True_If_At_Least_One_Individual_Record()
        {
            GEDCOMReader reader;
            using (Stream s = GetEmbeddedFileStream("OneIndividual"))
            {
                reader = GEDCOMReader.Create(s);
                bool moveResult = reader.MoveToIndividual();

                // Assert that Move was successful
                Assert.True(moveResult);
            }
        }

        [Fact]
        public void GEDCOMReader_MoveToIndividual_Returns_False_If_No_More_Individual_Records()
        {
            GEDCOMReader reader;
            using (Stream s = GetEmbeddedFileStream("OneIndividual"))
            {
                reader = GEDCOMReader.Create(s);
                bool moveResult = reader.MoveToIndividual();

                reader.ReadRecord();

                moveResult = reader.MoveToIndividual();

                // Assert that Move failed
                Assert.False(moveResult);
            }
        }

        #endregion MoveToIndividual

        #region MoveToRecord

        [Fact]
        public void GEDCOMReader_MoveToRecord_Returns_False_If_No_Records()
        {
            GEDCOMReader reader;
            using (Stream s = GetEmbeddedFileStream("Empty"))
            {
                reader = GEDCOMReader.Create(s);
                bool moveResult = reader.MoveToRecord();

                // Assert that Move failed
                Assert.False(moveResult);
            }
        }

        [Fact]
        public void GEDCOMReader_MoveToRecord_Returns_True_If_At_Least_One_Record()
        {
            GEDCOMReader reader;
            using (Stream s = GetEmbeddedFileStream("OneIndividual"))
            {
                reader = GEDCOMReader.Create(s);
                bool moveResult = reader.MoveToRecord();

                // Assert that Move was successful
                Assert.True(moveResult);
            }
        }

        [Fact]
        public void GEDCOMReader_MoveToRecord_Returns_False_If_No_More_Records()
        {
            GEDCOMReader reader;
            GEDCOMRecord record;
            using (Stream s = GetEmbeddedFileStream("OneIndividual"))
            {
                reader = GEDCOMReader.Create(s);
                bool moveResult = reader.MoveToRecord();

                record = reader.ReadRecord(); // HEAD
                record = reader.ReadRecord(); // SUBM
                record = reader.ReadRecord(); // INDI
                record = reader.ReadRecord(); // TRLR

                moveResult = reader.MoveToRecord();

                // Assert that Move failed
                Assert.False(moveResult);
            }
        }

        #endregion MoveToRecord

        #region Read

        [Theory]
        [InlineData("Empty", 0)]
        [InlineData("NoRecords", 3)]
        [InlineData("OneIndividual", 4)]
        [InlineData("TwoIndividuals", 5)]
        public void GEDCOMReader_Read_Reads_Correct_Count_Of_Records_From_String(string fileName, int expectedCount)
        {
            string text = GetEmbeddedFileString(fileName);
            GEDCOMReader reader = GEDCOMReader.Create(text);
            Assert.Equal(expectedCount, reader.Read().Count);
        }

        [Theory]
        [InlineData("Empty", 0)]
        [InlineData("NoRecords", 3)]
        [InlineData("OneIndividual", 4)]
        [InlineData("TwoIndividuals", 5)]
        public void GEDCOMReader_Read_Reads_Correct_Count_Of_Records_From_Stream(string fileName, int expectedCount)
        {
            GEDCOMReader reader;
            using (Stream s = GetEmbeddedFileStream(fileName))
            {
                reader = GEDCOMReader.Create(s);

                Assert.Equal(expectedCount, reader.Read().Count);
            }
        }

        [Theory]
        [InlineData("Empty", 0)]
        [InlineData("NoRecords", 3)]
        [InlineData("OneIndividual", 4)]
        [InlineData("TwoIndividuals", 5)]
        public void GEDCOMReader_Read_Reads_Correct_Count_Of_Records_From_TextReader(string fileName, int expectedCount)
        {
            string text = GetEmbeddedFileString(fileName);
            GEDCOMReader reader;
            using (StringReader stringReader = new StringReader(text))
            {
                reader = GEDCOMReader.Create(stringReader);

                Assert.Equal(expectedCount, reader.Read().Count);
            }
        }

        [Theory]
        [InlineData("NoRecords", 0, GEDCOMTag.HEAD)]
        [InlineData("NoRecords", 1, GEDCOMTag.SUBM)]
        [InlineData("NoRecords", 2, GEDCOMTag.TRLR)]
        [InlineData("OneIndividual", 0, GEDCOMTag.HEAD)]
        [InlineData("OneIndividual", 1, GEDCOMTag.SUBM)]
        [InlineData("OneIndividual", 2, GEDCOMTag.INDI)]
        [InlineData("OneIndividual", 3, GEDCOMTag.TRLR)]
        [InlineData("TwoIndividuals", 0, GEDCOMTag.HEAD)]
        [InlineData("TwoIndividuals", 1, GEDCOMTag.SUBM)]
        [InlineData("TwoIndividuals", 2, GEDCOMTag.INDI)]
        [InlineData("TwoIndividuals", 3, GEDCOMTag.INDI)]
        [InlineData("TwoIndividuals", 4, GEDCOMTag.TRLR)]
        public void GEDCOMReader_Read_Reads_Correct_Records(string fileName, int recordNo, GEDCOMTag tag)
        {
            GEDCOMReader reader;
            GEDCOMRecordList records;
            using (Stream s = GetEmbeddedFileStream(fileName))
            {
                reader = GEDCOMReader.Create(s);
                records = reader.Read();
            }
            Assert.Equal(tag, records[recordNo].TagName);
        }

        [Theory]
        [InlineData("OneIndividual", 2, 4)]
        [InlineData("TwoIndividuals", 2, 4)]
        [InlineData("TwoIndividuals", 3, 3)]
        public void GEDCOMReader_Read_Reads_Correct_Count_Of_ChildRecords(string fileName, int recordNo, int expectedCount)
        {
            GEDCOMReader reader;
            GEDCOMRecordList records;
            using (Stream s = GetEmbeddedFileStream(fileName))
            {
                reader = GEDCOMReader.Create(s);
                records = reader.Read();
            }
            GEDCOMRecord record = records[recordNo];

            Assert.Equal(expectedCount, record.ChildRecords.Count);
        }

        [Theory]
        [InlineData("OneIndividual", 2, 0, GEDCOMTag.NAME)]
        [InlineData("OneIndividual", 2, 1, GEDCOMTag.SEX)]
        [InlineData("OneIndividual", 2, 2, GEDCOMTag.BIRT)]
        [InlineData("OneIndividual", 2, 3, GEDCOMTag.DEAT)]
        [InlineData("TwoIndividuals", 2, 0, GEDCOMTag.NAME)]
        [InlineData("TwoIndividuals", 2, 1, GEDCOMTag.SEX)]
        [InlineData("TwoIndividuals", 2, 2, GEDCOMTag.BIRT)]
        [InlineData("TwoIndividuals", 2, 3, GEDCOMTag.DEAT)]
        [InlineData("TwoIndividuals", 3, 0, GEDCOMTag.NAME)]
        [InlineData("TwoIndividuals", 3, 1, GEDCOMTag.SEX)]
        [InlineData("TwoIndividuals", 3, 2, GEDCOMTag.BIRT)]
        public void GEDCOMReader_Read_Reads_Correct_ChildRecords(string fileName, int recordNo, int childRecordNo, GEDCOMTag tag)
        {
            GEDCOMReader reader;
            GEDCOMRecordList records;
            using (Stream s = GetEmbeddedFileStream(fileName))
            {
                reader = GEDCOMReader.Create(s);
                records = reader.Read();
            }
            GEDCOMRecord record = records[recordNo];

            Assert.Equal(tag, record.ChildRecords[childRecordNo].TagName);
        }

        [Fact]
        public void GEDCOMReader_Read_Reads_Record_If_One_Individual_Record()
        {
            GEDCOMReader reader;
            GEDCOMRecordList records;
            using (Stream s = GetEmbeddedFileStream("TwoIndividuals"))
            {
                reader = GEDCOMReader.Create(s);
                records = reader.Read();
            }

            // Get first real record
            GEDCOMRecord record = records[2];

            // Assert that Name record is valid
            GEDCOMRecord nameRecord = record.ChildRecords.GetLineByTag(GEDCOMTag.NAME);
            GEDCOMAssert.IsValidRecord(nameRecord, 1, "John /Smith/", false, -1);

            // Assert that Sex record is valid
            GEDCOMRecord sexRecord = record.ChildRecords.GetLineByTag(GEDCOMTag.SEX);
            GEDCOMAssert.IsValidRecord(sexRecord, 1, "M", false, -1);

            // Assert that Birth record is valid
            GEDCOMRecord birthRecord = record.ChildRecords.GetLineByTag(GEDCOMTag.BIRT);
            GEDCOMAssert.IsValidRecord(birthRecord, 1, "", true, 2);
            Assert.Equal(GEDCOMTag.DATE, birthRecord.ChildRecords[0].TagName);
            Assert.Equal(GEDCOMTag.PLAC, birthRecord.ChildRecords[1].TagName);

            GEDCOMRecord dateRecord = birthRecord.ChildRecords.GetLineByTag(GEDCOMTag.DATE);
            GEDCOMAssert.IsValidRecord(dateRecord, 2, "10 Apr 1964", false, -1);

            GEDCOMRecord placeRecord = birthRecord.ChildRecords.GetLineByTag(GEDCOMTag.PLAC);
            GEDCOMAssert.IsValidRecord(placeRecord, 2, "AnyTown", false, -1);

            // Assert that Death record is valid
            GEDCOMRecord deathRecord = record.ChildRecords.GetLineByTag(GEDCOMTag.DEAT);
            GEDCOMAssert.IsValidRecord(deathRecord, 1, "", true, 2);
            Assert.Equal(GEDCOMTag.DATE, deathRecord.ChildRecords[0].TagName);
            Assert.Equal(GEDCOMTag.PLAC, deathRecord.ChildRecords[1].TagName);

            dateRecord = deathRecord.ChildRecords.GetLineByTag(GEDCOMTag.DATE);
            GEDCOMAssert.IsValidRecord(dateRecord, 2, "15 May 1998", false, -1);

            placeRecord = birthRecord.ChildRecords.GetLineByTag(GEDCOMTag.PLAC);
            GEDCOMAssert.IsValidRecord(placeRecord, 2, "AnyTown", false, -1);
        }

        #endregion Read

        #region ReadFamilies

        [Theory]
        [InlineData("NoRecords", 0)]
        [InlineData("OneFamily", 1)]
        [InlineData("TwoFamilies", 2)]
        public void GEDCOMReader_ReadFamilies_Returns_A_List_Of_GEDCOMFamilyRecords(string fileName, int recordCount)
        {
            // Arrange
            GEDCOMReader reader;
            GEDCOMRecordList records;

            // Act
            using (Stream s = GetEmbeddedFileStream(fileName))
            {
                reader = GEDCOMReader.Create(s);
                records = reader.ReadFamilies();
            }

            // Assert
            Assert.Equal(recordCount, records.Count);

            for (int i = 0; i < records.Count; i++)
            {
                Assert.IsType<GEDCOMFamilyRecord>(records[i]);
            }
        }

        #endregion ReadFamilies

        #region ReadFamily

        [Fact]
        public void GEDCOMReader_ReadFamily_Returns_Null_If_No_Family_Records()
        {
            GEDCOMReader reader;
            GEDCOMFamilyRecord record;
            using (Stream s = GetEmbeddedFileStream("NoRecords"))
            {
                reader = GEDCOMReader.Create(s);
                record = reader.ReadFamily();
            }
            // Assert base record values
            Assert.Null(record);
        }

        [Theory]
        [InlineData("OneFamily", 2)]
        [InlineData("TwoFamilies", 3)]
        public void GEDCOMReader_ReadFamily_Returns_No_More_Individual_Records(string fileName, int recordNo)
        {
            GEDCOMReader reader;
            GEDCOMFamilyRecord record = null;
            using (Stream s = GetEmbeddedFileStream(fileName))
            {
                reader = GEDCOMReader.Create(s);
                for (int i = 0; i < recordNo; i++)
                {
                    record = reader.ReadFamily();
                }
            }
            // Assert base record values
            Assert.Null(record);
        }

        [Theory]
        [InlineData("OneFamily", 1)]
        [InlineData("TwoFamilies", 1)]
        [InlineData("TwoFamilies", 2)]
        public void GEDCOMReader_ReadFamily_Returns_Correct_GEDCOMFamilyRecord(string fileName, int recordNo)
        {
            GEDCOMReader reader;
            GEDCOMFamilyRecord actualRecord = null;
            GEDCOMFamilyRecord expectedRecord = Util.CreateFamilyRecord(recordNo);
            using (Stream s = GetEmbeddedFileStream(fileName))
            {
                reader = GEDCOMReader.Create(s);
                for (int i = 0; i < recordNo; i++)
                {
                    actualRecord = reader.ReadFamily();
                }
            }

            GEDCOMAssert.IsValidFamily(actualRecord);
            GEDCOMAssert.FamilyIsEqual(expectedRecord, actualRecord);
        }

        #endregion ReadFamily

        #region ReadHeader

        [Fact]
        public void GEDCOMReader_ReadHeader_Returns_Null_If_No_Header_Record()
        {
            GEDCOMReader reader;
            GEDCOMHeaderRecord record;
            using (Stream s = GetEmbeddedFileStream("Empty"))
            {
                reader = GEDCOMReader.Create(s);
                record = reader.ReadHeader();
            }
            // Assert base record values
            Assert.Null(record);
        }

        [Theory]
        [InlineData("NoRecords")]
        [InlineData("OneIndividual")]
        [InlineData("TwoIndividuals")]
        public void GEDCOMReader_ReadHeader_Returns_GEDCOMHeaderRecord(string fileName)
        {
            GEDCOMReader reader;
            GEDCOMHeaderRecord actualRecord;
            GEDCOMHeaderRecord expectedRecord = Util.CreateHeaderRecord(fileName);

            using (Stream s = GetEmbeddedFileStream(fileName))
            {
                reader = GEDCOMReader.Create(s);
                actualRecord = reader.ReadHeader();
            }

            GEDCOMAssert.IsValidHeader(actualRecord);
            GEDCOMAssert.HeaderIsEqual(expectedRecord, actualRecord);
        }

        #endregion ReadHeader

        #region ReadIndividual

        [Fact]
        public void GEDCOMReader_ReadIndividual_Returns_Null_If_No_Individual_Records()
        {
            GEDCOMReader reader;
            GEDCOMIndividualRecord record;
            using (Stream s = GetEmbeddedFileStream("NoRecords"))
            {
                reader = GEDCOMReader.Create(s);
                record = reader.ReadIndividual();
            }
            // Assert base record values
            Assert.Null(record);
        }

        [Theory]
        [InlineData("OneIndividual", 2)]
        [InlineData("TwoIndividuals", 3)]
        public void GEDCOMReader_ReadIndividual_Returns_No_More_Individual_Records(string fileName, int recordNo)
        {
            GEDCOMReader reader;
            GEDCOMIndividualRecord record = null;
            using (Stream s = GetEmbeddedFileStream(fileName))
            {
                reader = GEDCOMReader.Create(s);
                for (int i = 0; i < recordNo; i++)
                {
                    record = reader.ReadIndividual();
                }
            }
            // Assert base record values
            Assert.Null(record);
        }

        [Theory]
        [InlineData("OneIndividual", 1)]
        [InlineData("TwoIndividuals", 1)]
        [InlineData("TwoIndividuals", 2)]
        public void GEDCOMReader_ReadIndividual_Returns_Correct_GEDCOMIndividualRecord(string fileName, int recordNo)
        {
            GEDCOMReader reader;
            GEDCOMIndividualRecord actualRecord = null;
            GEDCOMIndividualRecord expectedRecord = Util.CreateIndividualRecord(recordNo);
            using (Stream s = GetEmbeddedFileStream(fileName))
            {
                reader = GEDCOMReader.Create(s);
                for (int i = 0; i < recordNo; i++)
                {
                    actualRecord = reader.ReadIndividual();
                }
            }

            GEDCOMAssert.IsValidIndividual(actualRecord);
            GEDCOMAssert.IndividualIsEqual(expectedRecord, actualRecord);
        }

        #endregion ReadIndividual

        #region ReadIndividuals

        [Theory]
        [InlineData("NoRecords", 0)]
        [InlineData("OneIndividual", 1)]
        [InlineData("TwoIndividuals", 2)]
        public void GEDCOMReader_ReadIndividual_Returns_A_List_Of_GEDCOMIndividualRecords(string fileName, int recordCount)
        {
            // Arrange
            GEDCOMReader reader;
            GEDCOMRecordList records;

            // Act
            using (Stream s = GetEmbeddedFileStream(fileName))
            {
                reader = GEDCOMReader.Create(s);
                records = reader.ReadIndividuals();
            }

            // Assert
            Assert.Equal(recordCount, records.Count);

            for (int i = 0; i < records.Count; i++)
            {
                Assert.IsType<GEDCOMIndividualRecord>(records[i]);
            }
        }

        #endregion ReadIndividuals

        protected override Stream GetEmbeddedFileStream(string fileName)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(GetEmbeddedFileName(fileName));
        }
    }
}