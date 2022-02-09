namespace SmartFamily.Gedcom.Enums
{
    /// <summary>
    /// Defines the current error status the parser is in.
    /// </summary>
    public enum GedcomErrorState
    {
        /// <summary>
        /// No error has occured.
        /// </summary>
        NoError = 0,

        /// <summary>
        /// A level value was expected but not found.
        /// </summary>
        LevelExpected,

        /// <summary>
        /// Delimeter after level not found.
        /// </summary>
        LevelMissingDelim,

        /// <summary>
        /// The level value is invalid.
        /// </summary>
        LevelInvalid,

        /// <summary>
        /// Delimeter after XrefID not found.
        /// </summary>
        XrefIDMissingDelim,

        /// <summary>
        /// The ID is too long, can be at most 22 characters.
        /// </summary>
        XrefIDTooLong,

        /// <summary>
        /// A GEDCOM tag name (or custom tag name) was expected but not found.
        /// </summary>
        TagExpected,

        /// <summary>
        /// Delimeter, or newline after the tag was not found.
        /// </summary>
        TagMissingDelimOrTerm,

        /// <summary>
        /// Value expected but not found.
        /// </summary>
        LineValueExpected,

        /// <summary>
        /// Newline after line value not found.
        /// </summary>
        LineValueMissingTerm,

        /// <summary>
        /// The line value is invalid.
        /// </summary>
        LineValueInvalid,

        /// <summary>
        /// Deliminator in GEDCOM is a single space, this error will occur
        /// when a multi space delimiter is detected.
        /// </summary>
        InvalidDelim,

        /// <summary>
        /// An unknown error has occurred while parsing.
        /// </summary>
        UnknownError,
    }
}