using SmartFamily.Gedcom.Common;
using SmartFamily.Gedcom.Records;

namespace SmartFamily.Gedcom.Structures
{
    /// <summary>
    /// The <see cref="GEDCOMMultimediaStructure"/> class models the GEDCOM Multimedia Link Structure.
    /// </summary>
    /// <remarks>
    ///   <h2>GEDCOM 5.5 MultiMedia Link Structure</h2>
    ///   n  OBJE @<XREF:OBJE>@                      {1:1} - MultimediaRecord<br />
    ///
    ///   n  OBJE                                    {1:1} - <br />
    ///     +1 FORM <MULTIMEDIA_FORMAT>              {1:1} - Format<br />
    ///     +1 TITL <DESCRIPTIVE_TITLE>              {0:1} - Title<br />
    ///     +1 FILE <MULTIMEDIA_FILE_REFERENCE>      {1:1} - FileReference<br />
    ///     +1 <<NOTE_STRUCTURE>>                    {0:M} - <i>see GEDCOMStructure - Notes<br />
    /// </remarks>
    public class GEDCOMMultimediaStructure : GEDCOMStructure
    {
        #region Constructors

        /// <summary>
        /// Constructs a <see cref="GEDCOMMultimediaStructure"/> from a <see cref="GEDCOMRecord"/>.
        /// </summary>
        /// <param name="record">A <see cref="GEDCOMRecord"/>.</param>
        public GEDCOMMultimediaStructure(GEDCOMRecord record)
            : base(record)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the Id of the Linked Multimedia Record.
        /// </summary>
        public string MultimediaRecord
        {
            get => XRefId;
        }

        /// <summary>
        /// Gets the FileReference for the Multimedia Link.
        /// </summary>
        public string FileReference
        {
            get => ChildRecords.GetRecordData(GEDCOMTag.FILE);
        }

        /// <summary>
        /// Gets the Format for the Multimedia Link.
        /// </summary>
        public string Format
        {
            get => ChildRecords.GetRecordData(GEDCOMTag.FORM);
        }

        /// <summary>
        /// Gets the Title for the Multimedia Link.
        /// </summary>
        public string Title
        {
            get => ChildRecords.GEtRecordDdata(GEDCOMTag.TITL);
        }

        #endregion
    }
}