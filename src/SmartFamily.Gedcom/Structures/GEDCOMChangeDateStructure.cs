using SmartFamily.Gedcom.Common;
using SmartFamily.Gedcom.Records;

namespace SmartFamily.Gedcom.Structures
{
    /// <summary>
    /// The <see cref="GEDCOMChangeDateStructure"/> class models the GEDCOM Change Date Structure.
    /// </summary>
    /// <remarks>
    ///   <h2>GEDCOM 5.5 Change Date</h2>
    ///   n  CHAN                        {1:1} - <br />
    ///     +1 DATE <CHANGE_DATE>        {1:1} - ChangeDate<br />
    ///       +2 TIME <TIME_VALUE>       {0:1} - <br />
    ///     +1 <<NOTE_STRUCTURE>>        {0:M} - <i>see GEDCOMStructure - Notes</i><br />
    /// </remarks>
    public class GEDCOMChangeDateStructure : GEDCOMStructure
    {
        #region Constructors

        /// <summary>
        /// Constructs a <see cref="GEDCOMChangeDateStructure"/> from a <see cref="GEDCOMRecord"/>.
        /// </summary>
        /// <param name="record">A <see cref="GEDCOMRecord"/>.</param>
        public GEDCOMChangeDateStructure(GEDCOMRecord record)
            : base(record)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the Change Date
        /// </summary>
        public DateTime ChangeDate
        {
            get
            {
                // Get the Date Record
                GEDCOMRecord dateRecord = ChildRecords.GetLineByTag(GEDCOMTag.DATE);
                DateTime changeDate = DateTime.MinValue;

                // If dateRecord is not null
                if (dateRecord != null)
                {
                    string dateString = dateRecord.Data;
                    string timeString = dateRecord.ChildRecords.GetRecordData(GEDCOMTag.TIME);

                    if (!string.IsNullOrEmpty(timeString))
                    {
                        dateString += " " + timeString;
                    }

                    // Parse the string into a DateTime value
                    DateTime.TryParse(dateString, out changeDate);
                }

                // Return the date
                return changeDate;
            }
        }

        #endregion
    }
}