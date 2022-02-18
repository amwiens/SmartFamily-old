using System.Text;

namespace SmartFamily.Core.Common
{
    public struct DateStructure
    {
        public DateType DateType { get; set; }

        public DateValue Date1 { get; set; }

        public DateValue Date2 { get; set; }

        public static DateStructure Parse(long dateValue)
        {
            var date = new DateStructure();

            return date;
        }

        public long ToInt64()
        {
            long longValue = -1;

            return longValue;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            switch (DateType)
            {
                case DateType.Exact:
                    sb.Append(Date1);
                    break;

                case DateType.About:
                    sb.Append("About ");
                    sb.Append(Date1);
                    break;

                case DateType.After:
                    sb.Append("After ");
                    sb.Append(Date1);
                    break;

                case DateType.Before:
                    sb.Append("Before ");
                    sb.Append(Date1);
                    break;

                case DateType.Between:
                    sb.Append("Between ");
                    sb.Append(Date1);
                    sb.Append(" And ");
                    sb.Append(Date2);
                    break;

                case DateType.Calculated:
                    sb.Append("Calc. ");
                    sb.Append(Date1);
                    break;

                case DateType.Estimated:
                    sb.Append("Est. ");
                    sb.Append(Date1);
                    break;

                case DateType.From:
                    sb.Append("From ");
                    sb.Append(Date1);
                    break;

                case DateType.To:
                    sb.Append("To ");
                    sb.Append(Date1);
                    break;

                case DateType.FromTo:
                    sb.Append("From ");
                    sb.Append(Date1);
                    sb.Append(" To ");
                    sb.Append(Date2);
                    break;
            }

            return sb.ToString();
        }
    }
}