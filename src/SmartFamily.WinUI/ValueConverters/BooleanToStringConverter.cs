using Microsoft.UI.Xaml.Data;

namespace SmartFamily.WinUI.ValueConverters;

internal sealed class BooleanToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is not bool bValue)
        {
            return string.Empty;
        }
        if (parameter is not string formatString)
        {
            return bValue.ToString();
        }

        // false:LOCALIZE|StringRes:true:STANDARD|Some string

        var valueSplit = formatString.Split(':');

        if (bValue)
        {
            var splitOption = valueSplit[3].Split('|');
            if (splitOption[0] == "LOCALIZE")
            {
                return splitOption[1]; // TODO: Localize
            }
            else
            {
                return splitOption[1];
            }
        }
        else
        {
            var splitOption = valueSplit[1].Split('|');
            if (splitOption[0] == "LOCALIZE")
            {
                return splitOption[1]; // TODO: Localize
            }
            else
            {
                return splitOption[1];
            }
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}