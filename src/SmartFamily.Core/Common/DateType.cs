namespace SmartFamily.Core.Common
{
    public enum DateType
    {
        /// <summary>
        /// Exact Date - Day, Month, Year
        /// </summary>
        Exact = 1,

        /// <summary>
        /// About Date
        /// </summary>
        About = 2,

        /// <summary>
        /// Estimated Date
        /// </summary>
        Estimated = 3,

        /// <summary>
        /// Calculated Date
        /// </summary>
        Calculated = 4,

        /// <summary>
        /// From Date
        /// </summary>
        From = 5,

        /// <summary>
        /// To Date
        /// </summary>
        To = 6,

        /// <summary>
        /// From Date1 to Date2
        /// </summary>
        FromTo = 7,

        /// <summary>
        /// Before Date
        /// </summary>
        Before = 8,

        /// <summary>
        /// After Date
        /// </summary>
        After = 9,

        /// <summary>
        /// Between Date1 and Date2
        /// </summary>
        Between = 10,
    }
}