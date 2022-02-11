namespace SmartFamily.Gedcom.Common
{
    /// <summary>
    /// An Enum representing the Event Types
    /// </summary>
    public enum IndividualAttributeType
    {
        /// <summary>
        /// CAST
        /// </summary>
        Caste,

        /// <summary>
        /// NATI
        /// </summary>
        Nationality,

        /// <summary>
        /// OCCU
        /// </summary>
        Occupation,

        /// <summary>
        /// RESI
        /// </summary>
        Residence,

        /// <summary>
        /// DSCR (meaning physical attribute, such as height)
        /// </summary>
        Attribute,

        Military,

        /// <summary>
        /// EDUC
        /// </summary>
        Education,

        /// <summary>
        /// SSN (meaining social security number)
        /// </summary>
        SSN,

        /// <summary>
        /// IDNO
        /// </summary>
        NationalID,

        /// <summary>
        /// NMR (information about and number of marriages)
        /// </summary>
        Marriages,

        /// <summary>
        /// NCHI (information about and number of children)
        /// </summary>
        Children,

        /// <summary>
        /// PROP
        /// </summary>
        Property,

        /// <summary>
        /// RELI
        /// </summary>
        Religion,

        /// <summary>
        /// TITL (meaning nobility title)
        /// </summary>
        Title,

        Other,

        Unknown
    }
}