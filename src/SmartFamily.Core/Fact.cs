using SmartFamily.Core.Common;

namespace SmartFamily.Core
{
    /// <summary>
    /// Fact is a class that represents a generic Fact (or attribute in GEDCOM)
    /// </summary>
    public class Fact : OwnedEntity
    {
        public Fact()
            : base(string.Empty)
        { }

        public Fact(string treeId)
            : base(treeId)
        {
            Citations = new List<Citation>();
        }

        /// <summary>
        /// Gets or sets the Citations for the Fact.
        /// </summary>
        public IList<Citation> Citations { get; set; }

        /// <summary>
        /// The date of the fact (if the fact is an event).
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// The type of the Fact.
        /// </summary>
        public FactType FactType { get; set; }

        /// <summary>
        /// The place for the fact.
        /// </summary>
        public string Place { get; set; }
    }
}