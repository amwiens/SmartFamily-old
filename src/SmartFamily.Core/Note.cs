using SmartFamily.Core.Common;

namespace SmartFamily.Core
{
    /// <summary>
    /// Note is a class that represents a Note.
    /// </summary>
    public class Note : OwnedEntity
    {
        public Note()
            : base(string.Empty)
        { }

        public Note(string treeId)
            : base(treeId)
        { }

        /// <summary>
        /// The text of the Note.
        /// </summary>
        public string Text { get; set; }
    }
}