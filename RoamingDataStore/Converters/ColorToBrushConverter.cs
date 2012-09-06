using System;
using GameLogic;
using System.Linq;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using System.Collections.Generic;

namespace RoamingDataStore.Converters
{
    class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var colorName = value.ToString();
            var color = ColorSelection.GetColorForColorName(colorName);
            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var brush = (SolidColorBrush)value;
            return brush.Color;
        }
    }
}
