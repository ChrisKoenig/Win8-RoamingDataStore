using GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Mastermind.Converters
{
    class CodeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // from code to color
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
