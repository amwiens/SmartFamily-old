using SmartFamily.Gedcom.Common;
using SmartFamily.Gedcom.Records;

using System.Text;

namespace SmartFamily.Gedcom.IO
{
    public class GEDCOMWriter : IDisposable
    {
        private bool _disposed;
        private TextWriter _writer;

        #region Constructors

        /// <summary>
        /// This constructor creates a GEDCOMWriter from a TextWriter.
        /// </summary>
        /// <param name="writer">The TextWriter to use.</param>
        private GEDCOMWriter(TextWriter writer)
        {
            _writer = writer;
        }

        /// <summary>
        /// This constructor creates a GEDCOMWriter that writes to a Stream.
        /// </summary>
        /// <param name="stream">The Stream to use.</param>
        private GEDCOMWriter(Stream stream)
        {
            _writer = new StreamWriter(stream);
        }

        /// <summary>
        /// This constructor creates a GEDCoMWriter that writes to a String.
        /// </summary>
        /// <param name="sb">The StringBuilder to use.</param>
        private GEDCOMWriter(StringBuilder sb)
        {
            _writer = new StringWriter(sb);
        }

        #endregion Constructors

        public string NewLine
        {
            get => _writer.NewLine;
            set => _writer.NewLine = value;
        }

        #region Private Methods

        private void WriteSpace()
        {
            WriteText(" ");
        }

        private void WriteText(string text)
        {
            _writer.Write(text);
        }

        #endregion Private Methods

        #region Public Static Methods

        /// <summary>
        /// Creates a GEDCOMWriter from a TextWriter.
        /// </summary>
        /// <param name="writer">The TextWriter to use.</param>
        /// <returns></returns>
        public static GEDCOMWriter Create(TextWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentNullException(typeof(TextWriter).Name);
            }

            return new GEDCOMWriter(writer);
        }

        /// <summary>
        /// Creates a GEDCOMWriter that writes to a Stream.
        /// </summary>
        /// <param name="stream">The Stream to use.</param>
        /// <returns></returns>
        public static GEDCOMWriter Create(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(typeof(Stream).Name);
            }

            return new GEDCOMWriter(stream);
        }

        /// <summary>
        /// Creates a GEDCOMWriter that writes to a String.
        /// </summary>
        /// <param name="stringBuilder">The StringBuilder to use.</param>
        /// <returns></returns>
        public static GEDCOMWriter Create(StringBuilder stringBuilder)
        {
            if (stringBuilder == null)
            {
                throw new ArgumentNullException(typeof(StringBuilder).Name);
            }

            return new GEDCOMWriter(stringBuilder);
        }

        #endregion Public Static Methods

        public void Flush()
        {
            if (_writer != null)
            {
                _writer.Flush();
            }
        }

        public void WriteNewLine()
        {
            _writer.Write(NewLine);
        }

        public void WriteRecords(GEDCOMRecordList records)
        {
            WriteRecords(records, true);
        }

        public void WriteRecords(GEDCOMRecordList records, bool includChildRecords)
        {
            foreach (GEDCOMRecord record in records)
            {
                WriteRecord(record, includChildRecords);
            }
        }

        #region WriteTag Methods

        public void WriteTag(GEDCOMTag tagName)
        {
            WriteTag(tagName.ToString());
        }

        public void WriteTag(string tag)
        {
            WriteText(tag);
        }

        #endregion WriteTag Methods

        #region WriteXRefID Methods

        public void WriteXRefId(string xRefId)
        {
            WriteText(xRefId);
        }

        public void WriteXRefId(int xRefId, string prefix)
        {
            WriteText($"@{prefix}{xRefId}@");
        }

        #endregion WriteXRefID Methods

        #region WriteRecord Methods

        public void WriteRecord(GEDCOMRecord record)
        {
            WriteRecord(record, true);
        }

        public void WriteRecord(GEDCOMRecord record, bool includeChildRecords)
        {
            // Split the data into CONT
            record.SplitData();

            WriteRecord(record.Id, record.Level, record.XRefId, record.Tag, record.Data);

            if (includeChildRecords)
            {
                WriteRecords(record.ChildRecords, true);
            }
        }

        public void WriteRecord(string id, int level, string xRefId, string tag, string data)
        {
            // Write the level
            WriteLevel(level);

            // If the id is specified, tag is of the form "level id tag"
            // If the XRefId is specified, record is of form "level tag xRefId"
            // Else record is of form "level tag data"
            if (!string.IsNullOrEmpty(id))
            {
                // Level Id Tag
                WriteSpace();
                WriteId(id);

                if (!string.IsNullOrEmpty(tag))
                {
                    WriteSpace();
                    WriteTag(tag);
                }
            }
            else if (!string.IsNullOrEmpty(xRefId))
            {
                // Level Tag XRefID
                if (!string.IsNullOrEmpty(tag))
                {
                    WriteSpace();
                    WriteTag(tag);
                }
                if (!string.IsNullOrEmpty(xRefId))
                {
                    WriteSpace();
                    WriteXRefId(xRefId);
                }
            }
            else
            {
                // Level Tag Data
                if (!string.IsNullOrEmpty(tag))
                {
                    WriteSpace();
                    WriteTag(tag);
                }
                if (!string.IsNullOrEmpty(data))
                {
                    WriteSpace();
                    WriteData(data);
                }
            }
            WriteNewLine();
        }

        #endregion WriteRecord Methods

        #region WriteData Methods

        public void WriteData(string data)
        {
            WriteText(data);
        }

        #endregion WriteData Methods

        #region WriteId Methods

        public void WriteId(string xRefId)
        {
            WriteText(xRefId);
        }

        public void WriteId(int id, string prefix)
        {
            WriteText($"@{prefix}{id}@");
        }

        #endregion WriteId Methods

        #region WriteLevel Methods

        public void WriteLevel(int level)
        {
            WriteText(level.ToString());
        }

        #endregion WriteLevel Methods

        #region IDisposable Implementation

        public void Dispose()
        {
            Dispose(true);

            // Use SuppressFinalize in case a subclass
            // of this type implements a finalizer.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // If you need thread safety, use a lock around these
            // operations, as well as in your methods that use the resource.
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_writer != null)
                    {
                        _writer.Dispose();
                    }
                }

                // Indicate that the instance has been disposed.
                _writer = null; ;
                _disposed = true;
            }
        }

        #endregion IDisposable Implementation
    }
}