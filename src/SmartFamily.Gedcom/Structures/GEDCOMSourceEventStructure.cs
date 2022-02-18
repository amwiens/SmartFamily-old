using SmartFamily.Gedcom.Common;
using SmartFamily.Gedcom.Records;

namespace SmartFamily.Gedcom.Structures
{
    /// <summary>
    /// The <see cref="GEDCOMSourceEventStructure"/> class models the GEDCOM Source Fact.
    /// </summary>
    public class GEDCOMSourceEventStructure : GEDCOMStructure
    {
        #region Constructors

        /// <summary>
        /// Constructs a <see cref="GEDCOMSourceEventStructure"/> from a <see cref="GEDCOMRecord"/>.
        /// </summary>
        /// <param name="record">A <see cref="GEDCOMRecord"/>.</param>
        public GEDCOMSourceEventStructure(GEDCOMRecord record)
            : base(record)
        {
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets the Facts.
        /// </summary>
        public string Events
        {
            get => Data;
        }

        /// <summary>
        /// Gets the Date.
        /// </summary>
        public string Date
        {
            get => ChildRecords.GetRecordData(GEDCOMTag.DATE);
        }

        /// <summary>
        /// Gets the Place.
        /// </summary>
        public string Place
        {
            get => ChildRecords.GetRecordData(GEDCOMTag.PLAC);
        }

        #endregion Public Properties
    }
}