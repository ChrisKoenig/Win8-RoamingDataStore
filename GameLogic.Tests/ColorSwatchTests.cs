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
            Color swatch = ColorSelection.GetColorForColorCode(theCode);
            Assert.AreEqual<Color>(swatch, Colors.Red);
        }

        [TestMethod]
        public void ConvertNameToColor()
        {
            string theName = "Red";
            Color swatch = ColorSelection.GetColorForColorName(theName);
            Assert.AreEqual<Color>(swatch, Colors.Red);
        }


        [TestMethod]
        public void ConvertColorToCode()
        {
            Color theColor = Colors.Red;
            string swatch = ColorSelection.GetColorCodeForColor(theColor);
            Assert.AreEqual<string>(swatch, "R");
        }
    }
}
