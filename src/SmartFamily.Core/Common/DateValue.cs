using System.Text;

namespace SmartFamily.Core.Common
{
    public class DateValue
    {
        public int Day { get; set; }

        public string Month { get; set; }

        public int Year { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            if (Day > 0)
            {
                sb.Append(Day);
                sb.Append(" ");
            }

            if (!String.IsNullOrEmpty(Month))
            {
                sb.Append(Month);
                sb.Append(" ");
            }

            if (Year > 0)
            {
                sb.Append(Year);
                sb.Append(" ");
            }

            return sb.ToString();
        }
    }
}