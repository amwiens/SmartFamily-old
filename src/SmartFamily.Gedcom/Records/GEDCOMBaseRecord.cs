using SmartFamily.Gedcom.Common;
using SmartFamily.Gedcom.Structures;

namespace SmartFamily.Gedcom.Records
{
    /// <summary>
    /// The <see cref="GEDCOMBaseRecord"/> class provides a base class for GEDCOM
    /// Records, by providing support for Change Date, a Collection
    /// of Notes and a collection of Source Citations.
    /// </summary>
    public class GEDCOMBaseRecord : GEDCOMRecord
    {
        /// <summary>
        /// Constructs a <see cref="GEDCOMBaseRecord"/> from a <see cref="GEDCOMRecord"/>.
        /// </summary>
        /// <param name="record">A <see cref="GEDCOMRecord"/>.</param>
        public GEDCOMBaseRecord(GEDCOMRecord record)
            : base(record)
        {
        }

        /// <summary>
        /// Gets the Automated Record ID.
        /// </summary>
        public GEDCOMExternalIDStructure AutomatedRecordId
        {
            get => ChildRecords.GetLineByTag<GEDCOMExternalIDStructure>(GEDCOMTag.RIN);
        }

        /// <summary>
        /// Gets the Change Date
        /// </summary>
        public GEDCOMChangeDateStructure ChangeDate
        {
            get => (GEDCOMChangeDateStructure)ChildRecords.GetLineByTag(GEDCOMTag.CHAN);
        }

        /// <summary>
        /// Gets a List of Multimedia.
        /// </summary>
        public List<GEDCOMMultimediaStructure> Multimedia
        {
            get => ChildRecords.GetLinesByTag<GEDCOMMultimediaStructure>(GEDCOMTag.OBJE);
        }

        /// <summary>
        /// Gets a List of Notes.
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

        /// <summary>
        /// Gets the User Defined IDs
        /// </summary>
        public List<GEDCOMExternalIDStructure> UserDefinedIDs
        {
            get => ChildRecords.GetLinesByTag<GEDCOMExternalIDStructure>(GEDCOMTag.REFN);
        }
    }
}