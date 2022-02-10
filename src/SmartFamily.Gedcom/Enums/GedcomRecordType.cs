namespace SmartFamily.Gedcom.Enums
{
    /// <summary>
    /// GEDCOM Record Types.
    /// </summary>
    public enum GedcomRecordType
    {
        /// <summary>
        /// Generic Record
        /// </summary>
        GenericRecord = 0,

        /// <summary>
        /// Family
        /// </summary>
        Family,

        /// <summary>
        /// Individual
        /// </summary>
        Individual,

        /// <summary>
        /// Multimedia
        /// </summary>
        Multimedia,

        /// <summary>
        /// Note
        /// </summary>
        Note,

        /// <summary>
        /// Repository
        /// </summary>
        Repository,

        /// <summary>
        /// Source
        /// </summary>
        Source,

        /// <summary>
        /// Submitter
        /// </summary>
        Submitter,

        // non-top level records

        /// <summary>
        /// Submission
        /// </summary>
        Submission,

        /// <summary>
        /// Event
        /// </summary>
        Event,

        /// <summary>
        /// Family Event
        /// </summary>
        FamilyEvent,

        /// <summary>
        /// Place
        /// </summary>
        Place,

        /// <summary>
        /// source Citation
        /// </summary>
        SourceCitation,

        /// <summary>
        /// Latter Day Saints Spouse Sealing record for a married couple.
        /// </summary>
        SpouseSealing,

        /// <summary>
        /// Family Link
        /// </summary>
        FamilyLink,

        /// <summary>
        /// Association
        /// </summary>
        Association,

        /// <summary>
        /// Name
        /// </summary>
        Name,

        /// <summary>
        /// Individual Event
        /// </summary>
        IndividualEvent,

        /// <summary>
        /// Date
        /// </summary>
        Date,

        /// <summary>
        /// Repository Citation
        /// </summary>
        RepositoryCitation,

        // GEDCOM allows custom records, beginning with _

        /// <summary>
        /// Custom Record
        /// </summary>
        CustomRecord,

        /// <summary>
        /// Header
        /// </summary>
        Header
    }
}