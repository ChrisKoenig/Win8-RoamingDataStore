using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI;

namespace GameLogic
{
    public class ColorSelection
    {

        public string ColorCode { get; set; }
        public string ColorName { get; set; }
        public Color ColorColor { get; set; }

        public static ColorSelection[] ColorSwatches = new ColorSelection[] {
                new ColorSelection() { ColorCode = "R", ColorName = "Red", ColorColor = Colors.Red },
                new ColorSelection() { ColorCode = "O", ColorName = "Orange", ColorColor = Colors.Orange},
                new ColorSelection() { ColorCode = "Y", ColorName = "Yellow", ColorColor = Colors.Yellow },
                new ColorSelection() { ColorCode = "G", ColorName = "Green", ColorColor = Colors.Green },
                new ColorSelection() { ColorCode = "B", ColorName = "Blue", ColorColor = Colors.Blue },
                new ColorSelection() { ColorCode = "W", ColorName = "White", ColorColor = Colors.White},
            };


        public static ColorSelection FindColorSwatchByColorCode(string code)
        {
            return ColorSwatches.Single(color => color.ColorCode == code);
        }

        public static ColorSelection FindColorSwatchByColorName(string name)
        {
            return ColorSwatches.Single(color => color.ColorName == name);
        }

        public static ColorSelection FindColorSwatchByColorName(Color color)
        {
            return ColorSwatches.Single(swatch => swatch.ColorColor.Equals(color));
        }

    }
}
