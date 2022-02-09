using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFamily.Gedcom.Enums
{
    /// <summary>
    /// GEDCOM event types.
    /// </summary>
    public enum GedcomEventType
    {
        /// <summary>
        /// Generic event.
        /// </summary>
        GenericEvent = 0,

        // Family Events

        /// <summary>
        /// Annulment
        /// </summary>
        /// <remarks>
        /// Declaring a marriage void from the beginning (never existed).
        /// </remarks>
        ANUL,

        /// <summary>
        /// Census
        /// </summary>
        /// <remarks>
        /// The event of the periodic count of the population for a designated locality,
        /// such as a national or state Census.
        /// </remarks>
        CENS_FAM,

        /// <summary>
        /// Divorce
        /// </summary>
        /// <remarks>
        /// An event of dissolving a marriage through civil action.
        /// </remarks>
        DIV,

        /// <summary>
        /// Divorce Filed
        /// </summary>
        /// <remarks>
        /// Ann event of filing for a divorce by a spouse.
        /// </remarks>
        DIVF,

        /// <summary>
        /// Engagement
        /// </summary>
        /// <remarks>
        /// An event of recording or announcing an agreement between two people to become married.
        /// </remarks>
        ENGA,

        /// <summary>
        /// Marriage BANN
        /// </summary>
        /// <remarks>
        /// An event of an official public notice given that two people inted to marry.
        /// </remarks>
        MARB,
    }
}