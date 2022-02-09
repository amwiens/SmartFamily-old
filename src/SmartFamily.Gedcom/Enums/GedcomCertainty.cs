namespace SmartFamily.Gedcom.Enums
{
    /// <summary>
    /// Indicates the credibility of a piece of information, based upon its supporting evidence.
    /// </summary>
    public enum GedcomCertainty
    {
        /// <summary>
        /// Unreliable
        /// </summary>
        Unreliable = 0,

        /// <summary>
        /// Questionable
        /// </summary>
        Questionable = 1,

        /// <summary>
        /// Secondary
        /// </summary>
        Secondary = 2,

        /// <summary>
        /// Primary
        /// </summary>
        Primary = 3,

        /// <summary>
        /// Unknown
        /// </summary>
        Unknown = 4,
    }
}