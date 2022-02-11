using SmartFamily.Core.Common;

namespace SmartFamily.Core
{
    /// <summary>
    /// Represents a source in a Family Tree
    /// </summary>
    public class Source : Entity
    {
        public Source()
            : base(string.Empty)
        { }

        public Source(string treeId)
            : base(treeId)
        {
            Author = string.Empty;
            Publisher = string.Empty;
            Title = string.Empty;
            RepositoryId = string.Empty;
        }

        /// <summary>
        /// Gets or sets the Author of the Source.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the Publisher of the Source.
        /// </summary>
        public string Publisher { get; set; }

        /// <summary>
        /// Gets or sets the Id of the Repository where the Source can be found.
        /// </summary>
        public string RepositoryId { get; set; }

        /// <summary>
        /// Gets or sets the Repository where the Source can be found.
        /// </summary>
        public Repository Repository { get; set; }

        /// <summary>
        /// Gets or sets the Title of the Source.
        /// </summary>
        public string Title { get; set; }
    }
}