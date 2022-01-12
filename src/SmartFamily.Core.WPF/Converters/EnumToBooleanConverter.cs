using System;
using System.Globalization;
using System.Windows.Data;

namespace SmartFamily.Core.WPF.Converters
{
    /// <summary>
    /// Enum to boolean converter.
    /// </summary>
    public class EnumToBooleanConverter : IValueConverter
    {
        public Type EnumType { get; set; }

        /// <summary>
        /// Converts the enum to boolean.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <param name="targetType">Type of the object to convert to.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="culture">Culture.</param>
        /// <returns>Boolean value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is string enumString)
            {
                if (Enum.IsDefined(EnumType, value))
                {
                    var enumValue = Enum.Parse(EnumType, enumString);

                    return enumValue.Equals(value);
                }
            }

            return false;
        }

        /// <summary>
        /// Converts the boolean to an enum.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <param name="targetType">Type of the object to convert to.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="culture">Culture.</param>
        /// <returns>Enum value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is string enumString)
            {
                return Enum.Parse(EnumType, enumString);
            }

            return null;
        }
    }
}