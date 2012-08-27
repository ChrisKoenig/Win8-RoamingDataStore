using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace GameLogic.Tests
{
    [TestClass]
    public class ColorSwatchTests
    {
        [TestMethod]
        public void GetNextColorEasy()
        {
            var code = "R";
            var expectedNext = "O";
            var actualNext = ColorSelection.GetNextColor(code);
            Assert.AreEqual<string>(expectedNext, actualNext);
        }

        [TestMethod]
        public void GetNextColorWrap()
        {
            var code = "W";
            var expectedNext = "R";
            var actualNext = ColorSelection.GetNextColor(code);
            Assert.AreEqual<string>(expectedNext, actualNext);
        }

        [TestMethod]
        public void ConvertCodeToColor()
        {
            string theCode = "R";
            ColorSelection swatch = ColorSelection.FindColorSwatchByColorCode(theCode);
            Assert.AreEqual<Color>(swatch.ColorColor, Colors.Red);
        }

        [TestMethod]
        public void ConvertNameToColor()
        {
            string theName = "Red";
            ColorSelection swatch = ColorSelection.FindColorSwatchByColorName(theName);
            Assert.AreEqual<Color>(swatch.ColorColor, Colors.Red);
        }


        [TestMethod]
        public void ConvertColorToCode()
        {
            Color theColor = Colors.Red;
            ColorSelection swatch = ColorSelection.FindColorSwatchByColorColor(theColor);
            Assert.AreEqual<string>(swatch.ColorCode, "R");
        }
    }
}
