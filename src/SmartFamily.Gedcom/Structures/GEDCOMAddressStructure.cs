using SmartFamily.Gedcom.Common;
using SmartFamily.Gedcom.Records;

namespace SmartFamily.Gedcom.Structures
{
    /// <summary>
    /// The <see cref="GEDCOMAddressStructure"/> class models the GEDCOM Address Structure.
    /// </summary>
    /// <remarks>
    ///  <h2>GEDCOM 5.5 Address Structure</h2>
    ///  n  ADDR <ADDRESS_LINE>                        {0:1} - Address<br />
    ///    +1 CONT <ADDRESS_LINE>                      {0:M} - <br />
    ///    +1 ADR1 <ADDRESS_LINE1>                     {0:1} - AddressLine1<br />
    ///    +1 ADR2 <ADDRESS_LINE2>                     {0:1} - AddressLine2<br />
    ///    +1 CITY <ADDRESS_CITY>                      {0:1} - City<br />
    ///    +1 STAE <ADDRESS_STATE>                     {0:1} - State<br />
    ///    +1 POST <ADDRESS_POSTAL_CODE>               {0:1} - PostCode<br />
    ///    +1 CTRY <ADDRESS_COUNTRY>                   {0:1} - Country<br />
    ///  n  PHON <PHONE_NUMBER>                        {0:3} - not implemented<br />
    /// </remarks>
    public class GEDCOMAddressStructure : GEDCOMStructure
    {
        #region Constructors

        public GEDCOMAddressStructure()
            : base(new GEDCOMRecord(1, "", "", "ADDR", ""))
        {
        }

        public GEDCOMAddressStructure(int level)
            : base(new GEDCOMRecord(level, "", "", "ADDR", ""))
        {
        }

        public GEDCOMAddressStructure(int level, string address)
            : base(new GEDCOMRecord(level, "", "", "ADDR", address))
        {
        }

        /// <summary>
        /// Constructs a <see cref="GEDCOMAddressStructure"/> from a <see cref="GEDCOMRecord"/>.
        /// </summary>
        /// <param name="record">A <see cref="GEDCOMRecord"/>.</param>
        public GEDCOMAddressStructure(GEDCOMRecord record)
            : base(record)
        {
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the Address.
        /// </summary>
        public string Address
        {
            get => Data;
            set => Data = value;
        }

        /// <summary>
        /// Gets or sets the AddressLine1.
        /// </summary>
        public string AddressLine1
        {
            get => GetChildData(GEDCOMTag.ADR1);
            set => SetChildData(GEDCOMTag.ADR1, value);
        }

        /// <summary>
        /// Gets or sets the AddressLine2.
        /// </summary>
        public string AddressLine2
        {
            get => GetChildData(GEDCOMTag.ADR2);
            set => SetChildData(GEDCOMTag.ADR2, value);
        }

        /// <summary>
        /// Gets or sets the City.
        /// </summary>
        public string City
        {
            get => GetChildData(GEDCOMTag.CITY);
            set => SetChildData(GEDCOMTag.CITY, value);
        }

        /// <summary>
        /// Gets or sets the State.
        /// </summary>
        public string State
        {
            get => GetChildData(GEDCOMTag.STAE);
            set => SetChildData(GEDCOMTag.STAE, value);
        }

        /// <summary>
        /// Gets or sets the PostCode.
        /// </summary>
        public string PostCode
        {
            get => GetChildData(GEDCOMTag.POST);
            set => SetChildData(GEDCOMTag.POST, value);
        }

        /// <summary>
        /// Gets or sets the Country.
        /// </summary>
        public string Country
        {
            get => GetChildData(GEDCOMTag.CTRY);
            set => SetChildData(GEDCOMTag.CTRY, value);
        }

        #endregion Public Properties
    }
}