using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
