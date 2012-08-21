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
        public void TestTwoRedOneWhiteOneBlank()
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

        [TestMethod]
        public void DuplicateColorInSolutionTwoRedTwoWhite()
        {
            Move _guess = new Move(
                ColorSelection.FindColorSwatchByColorName("Blue"),
                ColorSelection.FindColorSwatchByColorName("Red"),
                ColorSelection.FindColorSwatchByColorName("Blue"),
                ColorSelection.FindColorSwatchByColorName("White"));
            Move _solution = new Move(
                ColorSelection.FindColorSwatchByColorName("Blue"),
                ColorSelection.FindColorSwatchByColorName("White"),
                ColorSelection.FindColorSwatchByColorName("Blue"),
                ColorSelection.FindColorSwatchByColorName("Red"));
            var result = GameEngine.TestGuessAgainstSolution(_guess, _solution);
            Assert.AreEqual<int>(result.NumberOfReds, 2);
            Assert.AreEqual<int>(result.NumberOfWhites, 2);
            Assert.AreEqual<int>(result.NumberOfEmpties, 0);
            Assert.IsFalse(result.IsSolved);
        }

        [TestMethod]
        public void DuplicateWhitePegsResultingInOne()
        {
            Move _guess = new Move(
                ColorSelection.FindColorSwatchByColorName("Blue"),
                ColorSelection.FindColorSwatchByColorName("Blue"),
                ColorSelection.FindColorSwatchByColorName("Blue"),
                ColorSelection.FindColorSwatchByColorName("White"));
            Move _solution = new Move(
                ColorSelection.FindColorSwatchByColorName("White"),
                ColorSelection.FindColorSwatchByColorName("Orange"),
                ColorSelection.FindColorSwatchByColorName("White"),
                ColorSelection.FindColorSwatchByColorName("Red"));
            var result = GameEngine.TestGuessAgainstSolution(_guess, _solution);
            Assert.AreEqual<int>(result.NumberOfReds, 0);
            Assert.AreEqual<int>(result.NumberOfWhites, 1);
            Assert.AreEqual<int>(result.NumberOfEmpties, 3);
            Assert.IsFalse(result.IsSolved);
        }

        [TestMethod]
        public void OneRedOneWhite()
        {
            Move _guess = new Move(
                ColorSelection.FindColorSwatchByColorName("White"),
                ColorSelection.FindColorSwatchByColorName("Yellow"),
                ColorSelection.FindColorSwatchByColorName("Blue"),
                ColorSelection.FindColorSwatchByColorName("Orange"));
            Move _solution = new Move(
                ColorSelection.FindColorSwatchByColorName("Red"),
                ColorSelection.FindColorSwatchByColorName("White"),
                ColorSelection.FindColorSwatchByColorName("Blue"),
                ColorSelection.FindColorSwatchByColorName("Green"));
            var result = GameEngine.TestGuessAgainstSolution(_guess, _solution);
            Assert.AreEqual<int>(result.NumberOfReds, 1);
            Assert.AreEqual<int>(result.NumberOfWhites, 1);
            Assert.AreEqual<int>(result.NumberOfEmpties, 2);
            Assert.IsFalse(result.IsSolved);
        }

        [TestMethod]
        public void OneWhiteOneRed()
        {
            Move _guess = new Move(
                ColorSelection.FindColorSwatchByColorName("Green"),
                ColorSelection.FindColorSwatchByColorName("Red"),
                ColorSelection.FindColorSwatchByColorName("Red"),
                ColorSelection.FindColorSwatchByColorName("White"));
            Move _solution = new Move(
                ColorSelection.FindColorSwatchByColorName("Red"),
                ColorSelection.FindColorSwatchByColorName("Blue"),
                ColorSelection.FindColorSwatchByColorName("Red"),
                ColorSelection.FindColorSwatchByColorName("Yellow"));
            var result = GameEngine.TestGuessAgainstSolution(_guess, _solution);
            Assert.AreEqual<int>(result.NumberOfReds, 1);
            Assert.AreEqual<int>(result.NumberOfWhites, 1);
            Assert.AreEqual<int>(result.NumberOfEmpties, 2);
            Assert.IsFalse(result.IsSolved);
        }
    }
}
