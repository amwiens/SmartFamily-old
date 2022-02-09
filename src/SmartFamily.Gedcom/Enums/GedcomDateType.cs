namespace SmartFamily.Gedcom.Enums
{
    /// <summary>
    /// Calendars recognized in GEDCOM format.
    /// </summary>
    public enum GedcomDateType
    {
        /// <summary>
        /// Gregorian calendar.
        /// </summary>
        Gregorian,

        /// <summary>
        /// Julian calendar.
        /// </summary>
        Julian,

        /// <summary>
        /// Hebrew calendar.
        /// </summary>
        Hebrew,

        /// <summary>
        /// French calendar.
        /// </summary>
        French,

        /// <summary>
        /// Roman calendar.
        /// </summary>
        Roman,

        /// <summary>
        /// Unknown calendar.
        /// </summary>
        Unknown, // TODO: Shouldn't this be first, as in 0 for thte default int value?
    }
}