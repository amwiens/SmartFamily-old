using SmartFamily.Gedcom.Common;
using SmartFamily.Gedcom.Records;

namespace SmartFamily.Gedcom.Structures
{
    /// <summary>
    /// The <see cref="GEDCOMFamilyLinkStructure"/> class models the GEDCOM Child to Family Link Structure
    /// and the Spouse to Family Link Structure.
    /// </summary>
    /// <remarks>
    ///   <h2>GEDCOM 5.5 Link Structure</h2>
    ///   n FAMC @<XREF:FAM>@                       {1:1} - Family<br />
    ///     +1 PEDI <PEDIGREE_LINKAGE_TYPE>         {0:M} - Pedigree<br />
    ///     +1 <<NOTE_STRUCTURE>>                   {0:M} - <i>see GEDCOMStructure - Notes</i><br />
    ///
    ///   n FAMS @<XREF:FAM>@                       {1:1} - Family<br />
    ///     +1 <<NOTE_STRUCTURE>>                   {0:M} - <i>see GEDCOMStructure - Notes</i><br />
    /// </remarks>
    public class GEDCOMFamilyLinkStructure : GEDCOMStructure
    {
        #region Constructors

        /// <summary>
        /// Constructs a <see cref="GEDCOMFamilyLinkStructure"/> from a <see cref="GEDCOMRecord"/>.
        /// </summary>
        /// <param name="record">A <see cref="GEDCOMRecord"/>.</param>
        public GEDCOMFamilyLinkStructure(GEDCOMRecord record)
            : base(record)
        {
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets the Associated Family
        /// </summary>
        public string Family
        {
            get => XRefId;
        }

        /// <summary>
        /// Gets the Type of Link (Child or Spouse)
        /// </summary>
        public FamilyLinkType LinkType
        {
            get
            {
                if (Id == "FAMC")
                {
                    return FamilyLinkType.Child;
                }
                else
                {
                    return FamilyLinkType.Spouse;
                }
            }
        }

        /// <summary>
        /// Gets the Pedigree ([ adopted | birth | foster | sealing ])
        /// </summary>
        public string Pedigree
        {
            get => ChildRecords.GetRecordData(GEDCOMTag.PEDI);
        }

        #endregion Public Properties
    }
}