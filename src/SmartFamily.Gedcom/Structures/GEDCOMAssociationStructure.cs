using SmartFamily.Gedcom.Common;
using SmartFamily.Gedcom.Records;

namespace SmartFamily.Gedcom.Structures
{
    /// <summary>
    /// The <see cref="GEDCOMAssociationStructure"/> class models the GEDCOM Association Structure
    /// </summary>
    /// <remarks>
    ///   <h2>GEDCOM 5.5 Association</h2>
    ///   n ASSO @<XREF:INDI>@                      {0:M} - Individual<br />
    ///     +1 TYPE <RECORD_TYPE>                   {1:1} - RecordType<br />
    ///     +1 RELA <RELATION_IS_DESCRIPTOR>        {1:1} - Relation<br />
    ///     +1 <<SOURCE_CITATION>>                  {0:M} - <i>see GEDCOMStructure - SourceCitations<br />
    ///     +1 <<NOTE_STRUCTURE>>                   {0:M} - <i>see GEDCOMStructure - Notes</i><br />
    /// </remarks>
    public class GEDCOMAssociationStructure : GEDCOMStructure
    {
        #region Constructors

        /// <summary>
        /// Constructs a <see cref="GEDCOMAssociationStructure"/> from a <see cref="GEDCOMRecord"/>.
        /// </summary>
        /// <param name="record">A <see cref="GEDCOMRecord"/>.</param>
        public GEDCOMAssociationStructure(GEDCOMRecord record)
            : base(record)
        {
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets the Associated Individual.
        /// </summary>
        public string Individual
        {
            get => XRefId;
        }

        /// <summary>
        /// Gets the Record Type.
        /// </summary>
        public string RecordType
        {
            get => ChildRecords.GetRecordData(GEDCOMTag.TYPE);
        }

        /// <summary>
        /// Gets the Relation.
        /// </summary>
        public string Relation
        {
            get => ChildRecords.GetRecordData(GEDCOMTag.RELA);
        }

        #endregion Public Properties
    }
}