namespace SmartFamily.Gedcom.Enums
{
    /// <summary>
    /// How accurate is the date and what range does it span?
    /// </summary>
    public enum GedcomDatePeriod
    {
        /// <summary>
        /// A default for dates so that none slip in without being explicitly checked.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// A single point in time.
        /// </summary>
        Exact,

        /// <summary>
        /// Any point in time after.
        /// </summary>
        After,

        /// <summary>
        /// Any point in time before.
        /// </summary>
        Before,

        /// <summary>
        /// Any point in time between.
        /// </summary>
        Between,

        /// <summary>
        /// Roughly near the date.
        /// </summary>
        About,

        /// <summary>
        /// Calculated/reverse engineered from another piece of data.
        /// </summary>
        Calculated,

        /// <summary>
        /// An estimated date, likely to be slightly wrong.
        /// </summary>
        Estimate,

        /// <summary>
        /// What someone thinks the date look like based on reading old documents.
        /// </summary>
        Interpretation,

        /// <summary>
        /// A date range.
        /// </summary>
        Range,
    }
}