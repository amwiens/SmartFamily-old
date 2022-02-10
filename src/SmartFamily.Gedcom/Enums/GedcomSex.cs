namespace SmartFamily.Gedcom.Enums
{
    /// <summary>
    /// The gender / sex of an individual.
    /// </summary>
    public enum GedcomSex
    {
        /// <summary>
        /// The sex of the individual has not been set yet and is equivalent to null.
        /// </summary>
        NotSet = 0,

        /// <summary>
        /// Undetermined from available records and not quite sure what the sex is.
        /// </summary>
        Undetermined,

        /// <summary>
        /// The individual is male.
        /// </summary>
        Male,

        /// <summary>
        /// The individual is female.
        /// </summary>
        Female,

        /// <summary>
        /// The individual is both male and female.
        /// </summary>
        Both,

        /// <summary>
        /// The individual is neuter.
        /// </summary>
        Neuter,
    }
}