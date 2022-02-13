﻿using SmartFamily.Gedcom.Common;
using SmartFamily.Gedcom.Records;

namespace SmartFamily.Gedcom.Structures
{
    /// <summary>
    /// The ExternalID class provides a rich object to define
    /// External IDs.
    /// </summary>
    public class GEDCOMExternalIDStructure : GEDCOMRecord
    {
        #region Constructors

        /// <summary>
        /// Constructs a <see cref="GEDCOMExternalIDStructure"/> from a <see cref="GEDCOMRecord"/>.
        /// </summary>
        /// <param name="record">A <see cref="GEDCOMRecord"/>.</param>
        public GEDCOMExternalIDStructure(GEDCOMRecord record)
            : base(record)
        {
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets the External ID
        /// </summary>
        public string ExternalID
        {
            get => Data;
        }

        /// <summary>
        /// Gets the External ID Type
        /// </summary>
        public ExternalIDType Type
        {
            get
            {
                ExternalIDType type = ExternalIDType.UserDefined;
                switch (TagName)
                {
                    case GEDCOMTag.AFN:
                        type = ExternalIDType.AncestralFileNo;
                        break;

                    case GEDCOMTag.RIN:
                        type = ExternalIDType.AutomatedRecord;
                        break;

                    case GEDCOMTag.RFN:
                        type = ExternalIDType.PermanentRecordFileNumber;
                        break;
                }
                return type;
            }
        }

        /// <summary>
        /// Gets the ExternalID TypeDetail for User Defined types.
        /// </summary>
        public string TypeDetail
        {
            get => ChildRecords.GetRecordData(GEDCOMTag.TYPE);
        }

        #endregion Public Properties
    }
}