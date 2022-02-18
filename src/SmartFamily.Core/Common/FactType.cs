namespace SmartFamily.Core.Common
{
    /// <summary>
    /// Ann Enum representing the Fact Types
    /// </summary>
    public enum FactType
    {
        // Individual Facts

        /// <summary>
        /// ADOP
        /// </summary>
        Adoption = 0,

        /// <summary>
        /// BAPM
        /// </summary>
        Baptism = 1,

        /// <summary>
        /// BARM
        /// </summary>
        BarMitzvah = 2,

        /// <summary>
        /// BASM
        /// </summary>
        BasMitzvah = 3,

        /// <summary>
        /// BIRT
        /// </summary>
        Birth = 4,

        /// <summary>
        /// BLES
        /// </summary>
        Blessing = 5,

        /// <summary>
        /// BURI
        /// </summary>
        Burial = 6,

        /// <summary>
        /// CENS
        /// </summary>
        Census = 7,

        /// <summary>
        /// CHR
        /// </summary>
        Christening = 8,

        /// <summary>
        /// CHRA
        /// </summary>
        AdultChristening = 9,

        /// <summary>
        /// CONF
        /// </summary>
        Confirmation = 10,

        /// <summary>
        /// CREM
        /// </summary>
        Cremation = 11,

        /// <summary>
        /// DEAT
        /// </summary>
        Death = 12,

        /// <summary>
        /// EMIG
        /// </summary>
        Emigration = 13,

        /// <summary>
        /// /FCOM
        /// </summary>
        FirstCommunion = 14,

        /// <summary>
        /// GRAD
        /// </summary>
        Graduation = 15,

        /// <summary>
        /// IMMI
        /// </summary>
        Immigration = 16,

        /// <summary>
        /// NATU
        /// </summary>
        Naturalization = 17,

        /// <summary>
        /// ORDN
        /// </summary>
        Ordination = 18,

        /// <summary>
        /// PROB
        /// </summary>
        Probate = 19,

        /// <summary>
        /// RETI
        /// </summary>
        Retirement = 20,

        /// <summary>
        /// WILL
        /// </summary>
        Will = 21,

        // Individual Attributes

        /// <summary>
        /// CAST
        /// </summary>
        Caste = 100,

        /// <summary>
        /// DESC
        /// </summary>
        Description = 101,

        /// <summary>
        /// EDUC
        /// </summary>
        Education = 102,

        /// <summary>
        /// IDNO
        /// </summary>
        IdNumber = 103,

        /// <summary>
        /// NATI
        /// </summary>
        NationalOrTribalOrigin = 104,

        /// <summary>
        /// NCHI
        /// </summary>
        NoOfChildren = 105,

        /// <summary>
        /// NMR
        /// </summary>
        NoOfMarriages = 106,

        /// <summary>
        /// OCCU
        /// </summary>
        Occupation = 107,

        /// <summary>
        /// PROP
        /// </summary>
        Property = 108,

        /// <summary>
        /// RELI
        /// </summary>
        Religion = 109,

        /// <summary>
        /// RESI
        /// </summary>
        Residence = 110,

        /// <summary>
        /// SSN
        /// </summary>
        SocialSecurityNumber = 111,

        /// <summary>
        /// TITL
        /// </summary>
        Title = 112,

        // Family Facts

        /// <summary>
        /// ANUL
        /// </summary>
        Annulment = 200,

        /// <summary>
        /// DIV
        /// </summary>
        Divorce = 201,

        /// <summary>
        /// DIVF
        /// </summary>
        DivorceFiled = 202,

        /// <summary>
        /// ENGA
        /// </summary>
        Engagement = 203,

        /// <summary>
        /// MARR
        /// </summary>
        Marriage = 204,

        /// <summary>
        /// MARB
        /// </summary>
        MarriageBann = 205,

        /// <summary>
        /// MARC
        /// </summary>
        MarriageContract = 206,

        /// <summary>
        /// MARL
        /// </summary>
        MarriageLicense = 207,

        /// <summary>
        /// MARS
        /// </summary>
        MarriageSettlement = 208,

        /// <summary>
        /// EVEN
        /// </summary>
        Other = 998,

        Unknown = 999
    }
}