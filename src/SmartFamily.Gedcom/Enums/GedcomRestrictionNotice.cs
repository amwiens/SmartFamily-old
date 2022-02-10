namespace SmartFamily.Gedcom.Enums
{
    /// <summary>
    /// GEDCOM Restriction Types.
    /// </summary>
    /// <remarks>
    /// Signifies that access to information has been denied or otherwise restricted.
    /// </remarks>
    public enum GedcomRestrictionNotice
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,

        /// <summary>
        /// Confidential
        /// </summary>
        Confidential,

        /// <summary>
        /// Locked
        /// </summary>
        Locked,

        /// <summary>
        /// Privacy
        /// </summary>
        Privacy,
    }
}