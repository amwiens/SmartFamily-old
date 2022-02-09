using SmartFamily.Gedcom.Enums;

namespace SmartFamily.Gedcom
{
    /// <summary>
    /// Maps a date/date range indicator from the GEDCOM file to it's enum.
    /// </summary>
    public class GedcomDatePeriodParserMapping
    {
        /// <summary>
        /// Gets or sets the date period that this element maps to.
        /// </summary>
        public GedcomDatePeriod MapsTo { get; set; }

        /// <summary>
        /// Gets or sets the text that is searched for in the GEDCOM date line.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the position of the text that is searched for.
        /// </summary>
        public GedcomDatePeriodPosition TextPosition { get; set; }
    }
}