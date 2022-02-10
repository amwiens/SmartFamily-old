namespace SmartFamily.Gedcom.Enums
{
    /// <summary>
    /// Line values in GEDCOM can either be a pointer to another record, or the data itself.
    /// </summary>
    public enum GedcomLineValueType
    {
        /// <summary>
        /// No line value
        /// </summary>
        NoType,

        /// <summary>
        /// Line value is a pointer to another record.
        /// </summary>
        PointerType,

        /// <summary>
        /// Line value is the actual data
        /// </summary>
        DataType,
    }
}