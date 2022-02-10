namespace SmartFamily.Gedcom.Enums
{
    /// <summary>
    /// Defines the parse states for GEDCOM file.
    /// </summary>
    public enum GedcomState
    {
        /// <summary>
        /// Reading the current level
        /// </summary>
        Level,

        /// <summary>
        /// Reading the current ID
        /// </summary>
        XrefID,

        /// <summary>
        /// Reading the current tag name
        /// </summary>
        Tag,

        /// <summary>
        /// Reading the value for the current tag
        /// </summary>
        LineValue,
    }
}