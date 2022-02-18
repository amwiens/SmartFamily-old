using SmartFamily.Gedcom.Common;
using SmartFamily.Gedcom.Records;

namespace SmartFamily.Gedcom.Structures
{
    /// <summary>
    /// The <see cref="GEDCOMHeaderSourceStructure"/> class models the GEDCOM Header Records Source Structure.
    /// </summary>
    /// <remarks>
    ///   +1 SOUR <APPROVED_SYSTEM_ID>               {1:1} - Source<br />
    ///     +2 VERS <VERSION_NUMBER>                 {0:1} - Version<br />
    ///     +2 NAME <NAME_OF_PRODUCT>                {0:1} - ProductName<br />
    ///     +2 CORP <NAME_OF_BUSINESS>               {0:1} - Company<br />
    ///       +3 <<ADDRESS_STRUCTURE>>               {0:1} - Address<br />
    ///     +2 DATA <NAME_OF_SOURCE_DATA>            {0:1} - SourceData<br />
    ///       +3 DATE <PUBLICATION_DATE>             {0:1} - PublicationDate<br />
    ///       +3 COPR <COPYRIGHT_SOURCE_DATA>        {0:1} - SourceCopyright<br />
    /// </remarks>
    public class GEDCOMHeaderSourceStructure : GEDCOMStructure
    {
        #region Constructors

        /// <summary>
        /// Constructs a <see cref="GEDCOMHeaderSourceStructure"/>.
        /// </summary>
        public GEDCOMHeaderSourceStructure()
            : base(new GEDCOMRecord(1, "", "", "SOUR", ""))
        {
        }

        /// <summary>
        /// Constructs a <see cref="GEDCOMHeaderSourceStructure"/>.
        /// </summary>
        /// <param name="systemId">The system Id.</param>
        public GEDCOMHeaderSourceStructure(string systemId)
            : base(new GEDCOMRecord(1, "", "", "SOUR", systemId))
        {
        }

        /// <summary>
        /// Constructs a <see cref="GEDCOMHeaderSourceStructure"/> from a <see cref="GEDCOMRecord"/>.
        /// </summary>
        /// <param name="record">A <see cref="GEDCOMRecord"/>.</param>
        public GEDCOMHeaderSourceStructure(GEDCOMRecord record)
            : base(record)
        {
        }

        #endregion Constructors

        #region Protected Properties

        protected GEDCOMRecord CompanyRecord
        {
            get => ChildRecords.GetLineByTag<GEDCOMRecord>(GEDCOMTag.CORP);
        }

        protected GEDCOMRecord DataRecord
        {
            get => ChildRecords.GetLineByTag<GEDCOMRecord>(GEDCOMTag.DATA);
        }

        #endregion Protected Properties

        #region Public Properties

        public GEDCOMAddressStructure Address
        {
            get
            {
                GEDCOMAddressStructure address = null;
                if (CompanyRecord != null)
                {
                    address = CompanyRecord.ChildRecords.GetLineByTag<GEDCOMAddressStructure>(GEDCOMTag.ADDR);
                }
                return address;
            }
            set
            {
                if (CompanyRecord == null)
                {
                    // Add new Company Record
                    ChildRecords.Add(new GEDCOMRecord(Level + 1, "", "", "CORP", ""));
                }

                GEDCOMAddressStructure address = CompanyRecord.ChildRecords.GetLineByTag<GEDCOMAddressStructure>(GEDCOMTag.ADDR);

                if (address == null)
                {
                    // Add address structure
                    CompanyRecord.ChildRecords.Add(value);
                }
                else
                {
                    // Replace address structure
                    int index = CompanyRecord.ChildRecords.IndexOf(address);
                    CompanyRecord.ChildRecords[index] = value;
                }
            }
        }

        public string Company
        {
            get => GetChildData(GEDCOMTag.CORP);
            set => SetChildData(GEDCOMTag.CORP, value);
        }

        public string ProductName
        {
            get => GetChildData(GEDCOMTag.NAME);
            set => SetChildData(GEDCOMTag.NAME, value);
        }

        public string PublicationDate
        {
            get => GetChildData(GEDCOMTag.DATA, GEDCOMTag.DATE);
            set => SetChildData(GEDCOMTag.DATA, GEDCOMTag.DATE, value);
        }

        public string SourceCopyright
        {
            get => GetChildData(GEDCOMTag.DATA, GEDCOMTag.COPR);
            set => SetChildData(GEDCOMTag.DATA, GEDCOMTag.CORP, value);
        }

        public string SourceData
        {
            get => GetChildData(GEDCOMTag.DATA);
            set => SetChildData(GEDCOMTag.DATA, value);
        }

        public string SystemId
        {
            get => Data;
            set => Data = value;
        }

        public string Version
        {
            get => GetChildData(GEDCOMTag.VERS);
            set => SetChildData(GEDCOMTag.VERS, value);
        }

        #endregion Public Properties
    }
}