using SmartFamily.Gedcom.Common;

namespace SmartFamily.Gedcom.Records
{
    /// <summary>
    /// The <see cref="GEDCOMMultimediaRecord"/> class models a GEDCOM Multimedia Record.
    /// </summary>
    /// <remarks>
    ///   <h2>GEDCOM 5.5 Multimedia Record</h2>
    ///   n @XREF:OBJE@ OBJE                                      {1:1} <i>see GEDCOMRecord</i><br />
    ///     +1 FORM <MULTIMEDIA_FORMAT>                           {1:1} - Format<br />
    ///     +1 TITL <DESCRIPTIVE_TITLE>                           {0:1} - Title<br />
    ///     +1 <<NOTE_STRUCTURE>>                                 {0:M} - <i>see GEDCOMBaseRecord - Notes</i><br />
    ///     +1 BLOB                                               {1:1} - Content<br />
    ///       +2 CONT <ENCODED_MULTIMEDIA_LINE>                   {1:M} - <br />
    ///     +1 OBJE @<XREF:OBJE>@ /* chain to continued object */ {0:1} - not implemented<br />
    ///     +1 REFN <USER_REFERENCE_NUMBER>                       {0:M} - <i>see GEDCOMBaseRecord - UserDefinedIDs</i><br />
    ///       +2 TYPE <USER_REFERENCE_TYPE>                       {0:1} - <br />
    ///     +1 RIN <AUTOMATED_RECORD_ID>                          {0:1} - <i>see GEDCOMBaseRecord - AutomatedRecordID</i><br />
    ///     +1 <<CHANGE_DATE>>                                    {0:1} - <i>see GEDCOMBaseRecord - ChangeDate</i><br />
    /// </remarks>
    public class GEDCOMMultimediaRecord : GEDCOMBaseRecord
    {
        #region Constructors

        /// <summary>
        /// Constructs a <see cref="GEDCOMMultimediaRecord"/> from a <see cref="GEDCOMRecord"/>.
        /// </summary>
        /// <param name="record">A <see cref="GEDCOMRecord"/>.</param>
        public GEDCOMMultimediaRecord(GEDCOMRecord record)
            : base(record)
        {
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets the Content.
        /// </summary>
        public string Content
        {
            get => ChildRecords.GetRecordData(GEDCOMTag.BLOB);
        }

        /// <summary>
        /// Gets the Format.
        /// </summary>
        public string Format
        {
            get => ChildRecords.GetRecordData(GEDCOMTag.FORM);
        }

        /// <summary>
        /// Gets the Title.
        /// </summary>
        public string Title
        {
            get => ChildRecords.GetRecordData(GEDCOMTag.TITL);
        }

        #endregion Public Properties
    }
}