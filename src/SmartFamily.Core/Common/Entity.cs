namespace SmartFamily.Core.Common
{
    /// <summary>
    /// Entities are the main objects in the Family Tree (represented by Records in GEDCOM),
    /// Individuals, Families, Sources and Repositories are entities.
    /// While not represented by records in GEDCOM Facts are also Entities (as they have Notes)
    /// </summary>
    public abstract class Entity : BaseEntity
    {
        /// <summary>
        /// Constructs an Entity.
        /// </summary>
        /// <param name="treeId"></param>
        protected Entity(string treeId)
            : base(treeId)
        {
            Multimedia = new List<MultimediaLink>();
            Notes = new List<Note>();
        }

        /// <summary>
        /// A list of Multimedia Links
        /// </summary>
        public IList<MultimediaLink> Multimedia { get; set; }

        /// <summary>
        /// A list of Notes
        /// </summary>
        public IList<Note> Notes { get; set; }
    }
}