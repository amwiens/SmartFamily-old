using SmartFamily.Gedcom.Common;
using SmartFamily.Gedcom.Records;

namespace SmartFamily.Gedcom.Structures
{
    /// <summary>
    /// The GEDCOMSourceCitationStructure class models the GEDCOM Source Citation
    /// Structure
    /// </summary>
    /// <remarks>
    ///   <h2>GEDCOM 5.5 Source Citation Structure</h2>
    ///   n  SOUR @<XREF:SOUR>@                          {1:1} - <i>see GEDCOMRecord - XRefId</i><br />
    ///     +1 PAGE <WHERE_WITHIN_SOURCE>                {0:1} - Page<br />
    ///     +1 EVEN <EVENT_TYPE_CITED_FROM>              {0:1} - FactType<br />
    ///       +2 ROLE <ROLE_IN_EVENT>                    {0:1} - Role<br />
    ///     +1 DATA                                      {0:1} - CitationData<br />
    ///       +2 DATE <ENTRY_RECORDING_DATE>             {0:1} - Date<br />
    ///       +2 TEXT <TEXT_FROM_SOURCE>                 {0:M} - Text<br />
    ///         +3 [ CONC | CONT ] <TEXT_FROM_SOURCE>    {0:M} - <br />
    ///     +1 QUAY <CERTAINTY_ASSESSMENT>               {0:1} - Quality<br />
    ///     +1 <<MULTIMEDIA_LINK>>                       {0:M} - <i>see GEDCOMStructure - Multimedia</i><br />
    ///     +1 <<NOTE_STRUCTURE>>                        {0:M} - <i>see GEDCOMStructure - Notes</i><br />
    ///
    ///   -- Systems not using source records --
    ///   n  SOUR <SOURCE_DESCRIPTION>                   {1:1} - Description<br />
    ///     +1 [ CONC | CONT ] <SOURCE_DESCRIPTION>      {0:M} - <br />
    ///     +1 TEXT <TEXT_FROM_SOURCE>                   {0:M} - Text<br />
    ///       +2 [CONC | CONT ] <TEXT_FROM_SOURCE>       {0:M} - <br />
    ///     +1 <<NOTE_STRUCTURE>>                        {0:M} - <i>see GEDCOMStructure - Notes</i><br />
    /// </remarks>
    public class GEDCOMSourceCitationStructure : GEDCOMStructure
    {
        #region Constructors

        /// <summary>
        /// Constructs a <see cref="GEDCOMSourceCitationStructure"/> from a <see cref="GEDCOMRecord"/>.
        /// </summary>
        /// <param name="record">A <see cref="GEDCOMRecord"/>.</param>
        public GEDCOMSourceCitationStructure(GEDCOMRecord record)
            : base(record)
        {
        }

        #endregion Constructors

        #region Protected Properties

        /// <summary>
        /// Gets the Data Record
        /// </summary>
        protected GEDCOMRecord CitationData
        {
            get => ChildRecords.GetLineByTag<GEDCOMRecord>(GEDCOMTag.DATA);
        }

        #endregion Protected Properties

        #region Public Properties

        /// <summary>
        /// Gets the Citation Date.
        /// </summary>
        public string Date
        {
            get
            {
                string date = string.Empty;
                if (CitationData != null)
                {
                    date = CitationData.ChildRecords.GetRecordData(GEDCOMTag.DATE);
                }
                return date;
            }
        }

        /// <summary>
        /// Gets the Source Description.
        /// </summary>
        public string Description
        {
            get => Data;
        }

        /// <summary>
        /// Gets a List of Entries.
        /// </summary>
        public List<string> Entries
        {
            get
            {
                List<string> entries = new List<string>();

                // Get Text Records from TEXT Record
                foreach (GEDCOMRecord textRecord in ChildRecords.GetLinesByTag<GEDCOMRecord>(GEDCOMTag.TEXT))
                {
                    if (!string.IsNullOrEmpty(textRecord.Data))
                    {
                        entries.Add(textRecord.Data);
                    }
                }

                // Get Text Records from DATA/TEXT Record
                if (CitationData != null)
                {
                    foreach (GEDCOMRecord textRecord in CitationData.ChildRecords.GetLinesByTag<GEDCOMRecord>(GEDCOMTag.TEXT))
                    {
                        if (!string.IsNullOrEmpty(textRecord.Data))
                        {
                            entries.Add(textRecord.Data);
                        }
                    }
                }

                return entries;
            }
        }

        /// <summary>
        /// Gets the Source FactType.
        /// </summary>
        public string EventType
        {
            get => ChildRecords.GetRecordData(GEDCOMTag.EVEN);
        }

        /// <summary>
        /// Gets the Page for the Citation.
        /// </summary>
        public string Page
        {
            get => ChildRecords.GetRecordData(GEDCOMTag.PAGE);
        }

        /// <summary>
        /// Gets the Quality for the Citation.
        /// </summary>
        public int Quality
        {
            get
            {
                string qualityString = ChildRecords.GetRecordData(GEDCOMTag.QUAY);
                int quality = -1;
                if (!string.IsNullOrEmpty(qualityString))
                {
                    quality = Int32.Parse(qualityString);
                }
                return quality;
            }
        }

        /// <summary>
        /// Gets the Source Role.
        /// </summary>
        public string Role
        {
            get
            {
                string role = string.Empty;
                GEDCOMRecord eventType = ChildRecords.GetLineByTag<GEDCOMRecord>(GEDCOMTag.EVEN);
                if (eventType != null)
                {
                    role = eventType.ChildRecords.GetRecordData(GEDCOMTag.ROLE);
                }
                return role;
            }
        }

        /// <summary>
        /// Gets the Citation Text.
        /// </summary>
        public string Text
        {
            get
            {
                string text = String.Empty;
                if (CitationData != null)
                {
                    text = CitationData.ChildRecords.GetRecordData(GEDCOMTag.TEXT);
                }
                return text;
            }
        }

        #endregion Public Properties
    }
}