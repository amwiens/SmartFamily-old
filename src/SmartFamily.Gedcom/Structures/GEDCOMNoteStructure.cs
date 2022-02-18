using SmartFamily.Gedcom.Records;

namespace SmartFamily.Gedcom.Structures
{
    /// <summary>
    /// The <see cref="GEDCOMNoteStructure"/> class models the GEDCOM Note Structure.
    /// </summary>
    /// <remarks>
    ///  <h2>GEDCOM 5.5 Note Structure</h2>
    ///  n  NOTE @<XREF:NOTE>@                      {1:1} - NoteRecord<br />
    ///      +1 <<SOURCE_CITATION>>                 {0:M} - <i>see GEDCOMStructure - SourceCitations<br />
    ///
    ///  n  NOTE <SUBMITTER_TEXT> | <NULL>]         {1:1} - Text<br />
    ///      +1 [ CONC | CONT ] <SUBMITTER_TEXT>    {0:M} - <br />
    ///      +1 <<SOURCE_CITATION>>                 {0:M} - <i>see GEDCOMStructure - SourceCitations<br />
    /// </remarks>
    public class GEDCOMNoteStructure : GEDCOMStructure
    {
        #region Constructors

        /// <summary>
        /// Constructs a <see cref="GEDCOMNoteStructure"/> from a <see cref="GEDCOMRecord"/>.
        /// </summary>
        /// <param name="record">A <see cref="GEDCOMRecord"/>.</param>
        public GEDCOMNoteStructure(GEDCOMRecord record)
            : base(record)
        {
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets the Id of the Linked Note Record.
        /// </summary>
        public string NoteRecord
        {
            get => XRefId;
        }

        /// <summary>
        /// Gets the TExt for the Note
        /// </summary>
        public string Text
        {
            get => Data;
        }

        #endregion Public Properties
    }
}