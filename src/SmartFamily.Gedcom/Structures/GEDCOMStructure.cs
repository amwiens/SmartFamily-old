using SmartFamily.Gedcom.Common;
using SmartFamily.Gedcom.Records;

namespace SmartFamily.Gedcom.Structures
{
    /// <summary>
    /// The <see cref="GEDCOMStructure"/> class provides a base class for GEDCOM
    /// Structures, by providing support for Collections of Notes
    /// and collections of Source Citations.
    /// </summary>
    public class GEDCOMStructure : GEDCOMRecord
    {
        #region Constructors

        /// <summary>
        /// Constructs a <see cref="GEDCOMStructure"/> from a <see cref="GEDCOMRecord"/>.
        /// </summary>
        /// <param name="record">A <see cref="GEDCOMRecord"/>.</param>
        public GEDCOMStructure(GEDCOMRecord record)
            : base(record)
        {
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets a List of Multimedia.
        /// </summary>
        public List<GEDCOMMultimediaStructure> Multimedia
        {
            get => ChildRecords.GetLinesByTag<GEDCOMMultimediaStructure>(GEDCOMTag.OBJE);
        }

        /// <summary>
        /// Gets a List of Notes
        /// </summary>
        public List<GEDCOMNoteStructure> Notes
        {
            get => ChildRecords.GetLinesByTag<GEDCOMNoteStructure>(GEDCOMTag.NOTE);
        }

        /// <summary>
        /// Gets a List of Source Citations.
        /// </summary>
        public List<GEDCOMSourceCitationStructure> SourceCitations
        {
            get => ChildRecords.GetLinesByTag<GEDCOMSourceCitationStructure>(GEDCOMTag.SOUR);
        }

        #endregion Public Properties
    }
}