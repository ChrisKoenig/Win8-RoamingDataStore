using System;
using Windows.UI;
using System.Linq;
using System.Collections.Generic;

namespace GameLogic
{
    public class ColorHelper
    {
        public static Color GetColorFromName(string ColorName)
        {
            switch (ColorName)
            {
                case "Red":
                    return Colors.Red;
                case "Orange":
                    return Colors.Orange;
                case "Yellow":
                    return Colors.Yellow;
                case "Green":
                    return Colors.Green;
                case "Blue":
                    return Colors.Blue;
                case "White":
                    return Colors.White;
                default:
                    return Colors.Black;
            }
        }
    }
}
