namespace SmartFamily.Gedcom.Enums
{
    /// <summary>
    /// When parsing date formats dates can be prefixed but are sometimes suffixed.
    /// This defines where to look for specific date period indicators.
    /// </summary>
    public enum GedcomDatePeriodPosition
    {
        /// <summary>
        /// Error state for uninitialized instances.
        /// </summary>
        NotSet = 0,

        /// <summary>
        /// The text denoting the date period is before the dates.
        /// </summary>
        Prefix = 1,

        /// <summary>
        /// The text denoting the date period is after the dates.
        /// </summary>
        Suffix = 2,
    }
}