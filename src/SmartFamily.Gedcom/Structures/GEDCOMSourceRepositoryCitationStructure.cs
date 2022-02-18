using SmartFamily.Gedcom.Common;
using SmartFamily.Gedcom.Records;

namespace SmartFamily.Gedcom.Structures
{
    /// <summary>
    /// The <see cref="GEDCOMSourceRepositoryCitationStructure"/> class models the GEDCOM Source Repository Citation
    /// Structure.
    /// </summary>
    /// <remarks>
    ///   <h2>GEDCOM 5.5 Source Citation Structure</h2>
    ///   n REPO @XREF:REPO@                  {1:1} - <i>see GEDCOMRecord - XRefId</i><br />
    ///     +1 <<NOTE_STRUCTURE>>             {0:M} - <i>see GEDCOMBaseRecord - Notes</i><br />
    ///     +1 CALN <SOURCE_CALL_NUMBER>      {0:M} - CallNumbers<br />
    ///       +2 MEDI <SOURCE_MEDIA_TYPE>     {0:1} - <br />
    /// </remarks>
    public class GEDCOMSourceRepositoryCitationStructure : GEDCOMStructure
    {
        #region Constructors

        /// <summary>
        /// Constructs a <see cref="GEDCOMSourceRepositoryCitationStructure"/> from a <see cref="GEDCOMRecord"/>.
        /// </summary>
        /// <param name="record">A <see cref="GEDCOMRecord"/>.</param>
        public GEDCOMSourceRepositoryCitationStructure(GEDCOMRecord record)
            : base(record)
        {
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets a List of Call Numbers.
        /// </summary>
        public List<GEDCOMCallNumberStructure> CallNumbers
        {
            get => ChildRecords.GetLinesByTag<GEDCOMCallNumberStructure>(GEDCOMTag.CALN);
        }

        #endregion Public Properties
    }
}