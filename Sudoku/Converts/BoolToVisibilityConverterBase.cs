using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Sudoku.Converts
{
    /// <summary>
    /// Bei Multibinding werden die Werte mit "und" verknüpft
    /// </summary>
    public abstract class BoolToVisibilityConverterBase : IValueConverter, IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Visibility) || !ValueAsBool(value, CheckInput))
            {
                return null;
            }

            var valueAsBool = ValueAsBool(value, GetBoolValue);

            if (CheckForInvert(parameter))
            {
                valueAsBool = !valueAsBool;
            }

            var resultIfNotVisible = Visibility.Collapsed;

            if (CheckForHide(parameter))
            {
                resultIfNotVisible = Visibility.Hidden;
            }

            return valueAsBool ? Visibility.Visible : resultIfNotVisible;
        }

        private bool ValueAsBool(IEnumerable<object> value, Func<object, bool> map)
        {
            return value.Aggregate(true, (current, currentValue) => current & map(currentValue));
        }

        protected abstract bool GetBoolValue(object value);


        protected abstract bool CheckInput(object value);


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(new[] {value}, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(bool) || !(value is Visibility))
            {
                return null;
            }

            var valueAsVisibility = (Visibility)value;

            bool result = valueAsVisibility == Visibility.Visible;

            if (CheckForInvert(parameter))
            {
                result = !result;
            }

            return result;
        }

        private static bool CheckForInvert(object parameter)
        {
            var parameterAsString = parameter as string;
            return parameterAsString != null && parameterAsString.ToLowerInvariant().Contains("inv");
        }

        private static bool CheckForHide(object parameter)
        {
            var parameterAsString = parameter as string;
            return parameterAsString != null && parameterAsString.ToLowerInvariant().Contains("hid");
        }



        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
