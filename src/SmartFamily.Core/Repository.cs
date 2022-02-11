using SmartFamily.Core.Common;

namespace SmartFamily.Core
{
    /// <summary>
    /// The Repository class represents a repository of Family Tree information (this could be a website or a library for example).
    /// </summary>
    public class Repository : Entity
    {
        public Repository()
            : base(string.Empty)
        { }

        public Repository(string treeId)
            : base(treeId)
        { }

        /// <summary>
        /// The Address of the Repository.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// The name of the Repository.
        /// </summary>
        public string Name { get; set; }
    }
}