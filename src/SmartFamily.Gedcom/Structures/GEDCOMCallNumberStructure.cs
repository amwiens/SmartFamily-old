using SmartFamily.Gedcom.Common;
using SmartFamily.Gedcom.Records;

namespace SmartFamily.Gedcom.Structures
{
    /// <summary>
    /// The <see cref="GEDCOMCallNumberStructure"/> class provides a rich object to define
    /// Call Numbers.
    /// </summary>
    public class GEDCOMCallNumberStructure : GEDCOMRecord
    {
        #region Constructors

        /// <summary>
        /// Constructs a <see cref="GEDCOMCallNumberStructure"/> from a <see cref="GEDCOMRecord"/>.
        /// </summary>
        /// <param name="record">A <see cref="GEDCOMRecord"/>.</param>
        public GEDCOMCallNumberStructure(GEDCOMRecord record)
            : base(record)
        {
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets the Call Number.
        /// </summary>
        public string CallNumber
        {
            get => Data;
        }

        /// <summary>
        /// Gets the Source Media.
        /// </summary>
        public string SourceMedia
        {
            get => ChildRecords.GetRecordData(GEDCOMTag.MEDI);
        }

        #endregion Public Properties
    }
}