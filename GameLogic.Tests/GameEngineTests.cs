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
            Assert.AreEqual<int>(4, result.NumberOfReds, "Reds");
            Assert.AreEqual<int>(0, result.NumberOfWhites, "Whites");
            Assert.AreEqual<int>(0, result.NumberOfEmpties, "Empties");
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
            Assert.AreEqual<int>(2, result.NumberOfReds, "Reds");
            Assert.AreEqual<int>(1, result.NumberOfWhites, "Whites");
            Assert.AreEqual<int>(1, result.NumberOfEmpties, "Empties");
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
            Assert.AreEqual<int>(2, result.NumberOfReds, "Reds");
            Assert.AreEqual<int>(2, result.NumberOfWhites, "Whites");
            Assert.AreEqual<int>(0, result.NumberOfEmpties, "Empties");
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
            Assert.AreEqual<int>(0, result.NumberOfReds, "Reds");
            Assert.AreEqual<int>(1, result.NumberOfWhites, "Whites");
            Assert.AreEqual<int>(3, result.NumberOfEmpties, "Blanks");
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
            Assert.AreEqual<int>(1, result.NumberOfReds, "Reds");
            Assert.AreEqual<int>(1, result.NumberOfWhites, "Whites");
            Assert.AreEqual<int>(2, result.NumberOfEmpties, "Empties");
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
            Assert.AreEqual<int>(1, result.NumberOfReds, "Reds");
            Assert.AreEqual<int>(1, result.NumberOfWhites, "Whites");
            Assert.AreEqual<int>(2, result.NumberOfEmpties, "Empties");
            Assert.IsFalse(result.IsSolved);
        }


        [TestMethod]
        public void TestAllSameColorSolution()
        {
            GameMove _solution = new GameMove(
                ColorSelection.FindColorSwatchByColorName("Blue"),
                ColorSelection.FindColorSwatchByColorName("Blue"),
                ColorSelection.FindColorSwatchByColorName("Blue"),
                ColorSelection.FindColorSwatchByColorName("Blue"));

            GameMove _guess = new GameMove(
                ColorSelection.FindColorSwatchByColorName("Orange"),
                ColorSelection.FindColorSwatchByColorName("Blue"),
                ColorSelection.FindColorSwatchByColorName("Orange"),
                ColorSelection.FindColorSwatchByColorName("Blue"));

            var result = GameEngine.TestGuessAgainstSolution(_guess, _solution);

            // just test the last one
            Assert.AreEqual<int>(2, result.NumberOfReds, "Reds");
            Assert.AreEqual<int>(0, result.NumberOfWhites, "Whites");
            Assert.AreEqual<int>(2, result.NumberOfEmpties, "Empties");

        }

    }
}
