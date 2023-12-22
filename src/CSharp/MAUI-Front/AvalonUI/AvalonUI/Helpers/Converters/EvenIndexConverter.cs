using System.Collections;
using System.Globalization;

namespace AvalonUI.Helpers.Converters;
public class EvenIndexConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values == null || values.Any(x => x == null))
            return default;
        if (((IList)values[1]).IndexOf(values[0]) % 2 == 0)
            return Color.Parse("#1F000000");
        return Colors.White;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
