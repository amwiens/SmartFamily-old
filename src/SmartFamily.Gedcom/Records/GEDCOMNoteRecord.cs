namespace SmartFamily.Gedcom.Records
{
    /// <summary>
    /// The <see cref="GEDCOMNoteRecord"/> class models a GEDCOM Note Record.
    /// </summary>
    /// <remarks>
    ///   <h2>GEDCOM 5.5 Note Record</h2>
    ///   n @<XREF:NOTE>@ NOTE <SUBMITTER_TEXT>		{1:1} <i>see GEDCOMRecord</i><br />
    ///       +1 [ CONC | CONT] <SUBMITTER_TEXT>		{0:M} - <br />
    ///       +1 <<SOURCE_CITATION>>					{0:M} - <i>see GEDCOMBaseRecord - SourceCitations<br />
    ///       +1 REFN <USER_REFERENCE_NUMBER>		{0:M} - <i>see GEDCOMBaseRecord - UserDefinedIDs</i><br />
    ///           +2 TYPE <USER_REFERENCE_TYPE>		{0:1} - <br />
    ///       +1 RIN <AUTOMATED_RECORD_ID>           {0:1} - <i>see GEDCOMBaseRecord - AutomatedRecordID</i><br />
    ///       +1 <<CHANGE_DATE>>                     {0:1} - <i>see GEDCOMBaseRecord - ChangeDate</i><br />
    ///       <br />
    ///       Note that most notes are usually included in GEDCOM files as <NOTE_STRUCTURE>
    /// </remarks>
    public class GEDCOMNoteRecord : GEDCOMBaseRecord
    {
        #region Constructors

        /// <summary>
        /// Constructs a <see cref="GEDCOMNoteRecord"/> from a <see cref="GEDCOMRecord"/>.
        /// </summary>
        /// <param name="record">A <see cref="GEDCOMRecord"/>.</param>
        public GEDCOMNoteRecord(GEDCOMRecord record)
            : base(record)
        {
        }

        #endregion Constructors
    }
}