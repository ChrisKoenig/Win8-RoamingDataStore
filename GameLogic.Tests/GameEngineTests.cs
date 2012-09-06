using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace GameLogic.Tests
{
    [TestClass]
    public class GameEngineTests
    {
        [TestMethod]
        public void TestExactSolution()
        {
            GameMove _guess = new GameMove(
                ColorSelection.FindColorSwatchByColorName("Blue"),
                ColorSelection.FindColorSwatchByColorName("Orange"),
                ColorSelection.FindColorSwatchByColorName("Red"),
                ColorSelection.FindColorSwatchByColorName("White"));
            GameMove _solution = new GameMove(
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
            GameMove _guess = new GameMove(
                ColorSelection.FindColorSwatchByColorName("Blue"),
                ColorSelection.FindColorSwatchByColorName("Orange"),
                ColorSelection.FindColorSwatchByColorName("Red"),
                ColorSelection.FindColorSwatchByColorName("White"));
            GameMove _solution = new GameMove(
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
            GameMove _guess = new GameMove(
                ColorSelection.FindColorSwatchByColorName("Blue"),
                ColorSelection.FindColorSwatchByColorName("Red"),
                ColorSelection.FindColorSwatchByColorName("Blue"),
                ColorSelection.FindColorSwatchByColorName("White"));
            GameMove _solution = new GameMove(
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
            GameMove _guess = new GameMove(
                ColorSelection.FindColorSwatchByColorName("Blue"),
                ColorSelection.FindColorSwatchByColorName("Blue"),
                ColorSelection.FindColorSwatchByColorName("Blue"),
                ColorSelection.FindColorSwatchByColorName("White"));
            GameMove _solution = new GameMove(
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
            GameMove _guess = new GameMove(
                ColorSelection.FindColorSwatchByColorName("White"),
                ColorSelection.FindColorSwatchByColorName("Yellow"),
                ColorSelection.FindColorSwatchByColorName("Blue"),
                ColorSelection.FindColorSwatchByColorName("Orange"));
            GameMove _solution = new GameMove(
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
            GameMove _guess = new GameMove(
                ColorSelection.FindColorSwatchByColorName("Green"),
                ColorSelection.FindColorSwatchByColorName("Red"),
                ColorSelection.FindColorSwatchByColorName("Red"),
                ColorSelection.FindColorSwatchByColorName("White"));
            GameMove _solution = new GameMove(
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
