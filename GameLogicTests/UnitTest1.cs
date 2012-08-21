using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using RoamingDataStore;

namespace GameLogicTests
{
    [TestClass]
    public class SimpleGameTests
    {
        [TestMethod]
        public void TestExactSolution()
        {
            Move _guess = new Move(
                ColorSelection.FindColorSwatchByColorName("Blue"),
                ColorSelection.FindColorSwatchByColorName("Orange"),
                ColorSelection.FindColorSwatchByColorName("Red"),
                ColorSelection.FindColorSwatchByColorName("White"));
            Move _solution = new Move(
                ColorSelection.FindColorSwatchByColorName("Blue"),
                ColorSelection.FindColorSwatchByColorName("Orange"),
                ColorSelection.FindColorSwatchByColorName("Red"),
                ColorSelection.FindColorSwatchByColorName("White"));
            var result = GameEngine.TestGuessAgainstSolution(_guess, _solution);
            Assert.AreEqual<int>(result.NumberOfReds, 4);
            Assert.AreEqual<int>(result.NumberOfWhites, 0);
            Assert.IsTrue(result.IsSolved);
        }

        [TestMethod]
        public void TestTwoRedOneWhite()
        {
            Move _guess = new Move(
                ColorSelection.FindColorSwatchByColorName("Blue"),
                ColorSelection.FindColorSwatchByColorName("Orange"),
                ColorSelection.FindColorSwatchByColorName("Red"),
                ColorSelection.FindColorSwatchByColorName("White"));
            Move _solution = new Move(
                ColorSelection.FindColorSwatchByColorName("Blue"),
                ColorSelection.FindColorSwatchByColorName("Orange"),
                ColorSelection.FindColorSwatchByColorName("Yellow"),
                ColorSelection.FindColorSwatchByColorName("Red"));
            var result = GameEngine.TestGuessAgainstSolution(_guess, _solution);
            Assert.AreEqual<int>(result.NumberOfReds, 2);
            Assert.AreEqual<int>(result.NumberOfWhites, 1);
            Assert.AreEqual<int>(result.NumberOfEmpties, 1);
            Assert.IsFalse(result.IsSolved);
        }
    }
}
