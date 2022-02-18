using SmartFamily.Gedcom.Common;
using SmartFamily.Gedcom.Records;

using System.Text.RegularExpressions;

namespace SmartFamily.Gedcom.Structures
{
    /// <summary>
    /// The <see cref="GEDCOMNameStructure"/> class models the GEDCOM Personal Name Structure.
    /// </summary>
    /// <remarks>
    ///   <h2>GEDCOM 5.5 Personal Name Structure</h2>
    ///   n  NAME <NAME_PERSONAL>						{1:1} - FullName<br />
    ///     +1 NPFX <NAME_PIECE_PREFIX>				    {0:1} - Prefix<br />
    ///     +1 GIVN <NAME_PIECE_GIVEN>				    {0:1} - GivenName<br />
    ///     +1 NICK <NAME_PIECE_NICKNAME>		        {0:1} - NickName<br />
    ///     +1 SPFX <NAME_PIECE_SURNAME_PREFIX>          {0:1} - LastNamePrefix<br />
    ///     +1 SURN <NAME_PIECE_SURNAME>			        {0:1} - LastName<br />
    ///     +1 NSFX <NAME_PIECE_SUFFIX>				    {0:1} - Suffix<br />
    ///     +1 <<SOURCE_CITATION>>					    {0:M} - <i>see GEDCOMStructure - SourceCitations</i><br />
    ///     +1 <<NOTE_STRUCTURE>>					    {0:M} - <i>see GEDCOMStructure - Notes</i><br />
    /// </remarks>
    public class GEDCOMNameStructure : GEDCOMStructure
    {
        // Expression pattern used to parse the Name record.
        private readonly Regex nameReg = new Regex(@"(?<first>[\w\s]*)/(?<last>[\S]*)/");

        #region Constructors

        /// <summary>
        /// Constructs a <see cref="GEDCOMNameStructure"/>.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="level"></param>
        public GEDCOMNameStructure(string name, int level)
            : base(new GEDCOMRecord(level, "", "", "NAME", name))
        {
        }

        /// <summary>
        /// Constructs a <see cref="GEDCOMNameStructure"/> from a <see cref="GEDCOMRecord"/>.
        /// </summary>
        /// <param name="record">A <see cref="GEDCOMRecord"/>.</param>
        public GEDCOMNameStructure(GEDCOMRecord record)
            : base(record)
        {
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the Full Name.
        /// </summary>
        public string FullName
        {
            get => Data;
            set => Data = value;
        }

        /// <summary>
        /// Gets or sets the Given Name.
        /// </summary>
        public string GivenName
        {
            get
            {
                string givenName = ChildRecords.GetRecordData(GEDCOMTag.GIVN);
                if (string.IsNullOrEmpty(givenName) && !string.IsNullOrEmpty(FullName))
                {
                    Match match = nameReg.Match(FullName);
                    givenName = match.Groups["first"].Value.Trim();
                }
                return givenName;
            }
            set => SetChildData(GEDCOMTag.GIVN, value);
        }

        /// <summary>
        /// Gets or sets the Last Name.
        /// </summary>
        public string LastName
        {
            get
            {
                string lastName = ChildRecords.GetRecordData(GEDCOMTag.SURN);
                if (string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(FullName))
                {
                    Match match = nameReg.Match(FullName);
                    lastName = match.Groups["last"].Value.Trim();
                }
                return lastName;
            }
            set => SetChildData(GEDCOMTag.SURN, value);
        }

        /// <summary>
        /// Gets or sets the Nickname.
        /// </summary>
        public string NickName
        {
            get => GetChildData(GEDCOMTag.NICK);
            set => SetChildData(GEDCOMTag.NICK, value);
        }

        /// <summary>
        /// Gets or sets the Prefix.
        /// </summary>
        public string Prefix
        {
            get => GetChildData(GEDCOMTag.NPFX);
            set => SetChildData(GEDCOMTag.NPFX, value);
        }

        /// <summary>
        /// Gets or sets the Suffix.
        /// </summary>
        public string Suffix
        {
            get => GetChildData(GEDCOMTag.NSFX);
            set => SetChildData(GEDCOMTag.NSFX, value);
        }

        /// <summary>
        /// Gets or sets the Last Name Prefix.
        /// </summary>
        public string LastNamePrefix
        {
            get => GetChildData(GEDCOMTag.SPFX);
            set => SetChildData(GEDCOMTag.SPFX, value);
        }

        #endregion Public Properties
    }
}