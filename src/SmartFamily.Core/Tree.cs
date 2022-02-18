namespace SmartFamily.Core
{
    /// <summary>
    /// Represents a Family Tree
    /// </summary>
    public class Tree
    {
        public Tree()
        {
            Name = string.Empty;
            Title = string.Empty;
            Description = string.Empty;
            HomeIndividualId = -1;
            ImageId = -1;
            LastViewedIndividualId = -1;
            OwnerId = -1;
        }

        /// <summary>
        /// The Id of the Tree.
        /// </summary>
        public string TreeId { get; set; }

        /// <summary>
        /// The Description of the Tree.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The Id of the current home or root individual.
        /// </summary>
        public int HomeIndividualId { get; set; }

        /// <summary>
        /// The Id of the image displayed on the main view.
        /// </summary>
        public int ImageId { get; set; }

        /// <summary>
        /// The Id of the last viewed individual.
        /// </summary>
        public int LastViewedIndividualId { get; set; }

        /// <summary>
        /// The Name of the Tree.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The owner of the tree (this Id can be used to associate trees with "users").
        /// An implementation system will manage the "owning" of a tree.
        /// </summary>
        public int OwnerId { get; set; }

        /// <summary>
        /// The Title of the Tree.
        /// </summary>
        public string Title { get; set; }
    }
}