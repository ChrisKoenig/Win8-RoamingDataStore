using System;
using GameLogic;
using Windows.UI;
using System.Linq;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using System.Collections.Generic;

namespace RoamingDataStore.Converters
{
    class CodeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // from code to color
            if (value == null) return new SolidColorBrush(Colors.Purple);
            string theCode = value.ToString();
            Color swatch = ColorSelection.GetColorForColorCode(theCode);
            return new SolidColorBrush(swatch);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // from color to code
            SolidColorBrush theBrush = (SolidColorBrush)value;
            Color theColor = theBrush.Color;
            string  swatch = ColorSelection.GetColorCodeForColor(theColor);
            return swatch;
        }
    }
}
