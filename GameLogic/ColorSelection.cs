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
        public Color ColorColor
        {
            get
            {
                return GameLogic.ColorHelper.GetColorFromName(ColorName);
             }
        }

        public static ColorSelection[] ColorSwatches = new ColorSelection[] {
                new ColorSelection() { ColorCode = "R", ColorName = "Red"},
                new ColorSelection() { ColorCode = "O", ColorName = "Orange"},
                new ColorSelection() { ColorCode = "Y", ColorName = "Yellow"},
                new ColorSelection() { ColorCode = "G", ColorName = "Green"},
                new ColorSelection() { ColorCode = "B", ColorName = "Blue"},
                new ColorSelection() { ColorCode = "W", ColorName = "White"},
            };


        public static ColorSelection FindColorSwatchByColorCode(string code)
        {
            return ColorSwatches.Single(color => color.ColorCode == code);
        }

        public static ColorSelection FindColorSwatchByColorName(string name)
        {
            return ColorSwatches.Single(color => color.ColorName == name);
        }

        public static ColorSelection FindColorSwatchByColorColor(Color color)
        {
            return ColorSwatches.Single(swatch => swatch.ColorColor.Equals(color));
        }


        public static string GetNextColor(string ColorCode)
        {
            // find swatch for selected color code
            var swatch = ColorSwatches.Single(s => s.ColorCode == ColorCode);

            // find index of swatch
            var index = Array.IndexOf(ColorSwatches, swatch);

            // add one to index
            index++;

            // if index > number of colors, reset to 0
            if (index > ColorSwatches.Count() - 1)
                index = 0;

            // pull color string from selected color
            var newSwatch = ColorSwatches[index];

            // return the result
            return newSwatch.ColorCode;

        }

    }
}
