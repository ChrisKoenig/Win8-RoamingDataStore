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
            Move _guess = new Move() { SlotOne = ColorSelection.Blue, SlotTwo = ColorSelection.Orange, SlotThree = ColorSelection.Red, SlotFour = ColorSelection.White };
            Move _solution = new Move() { SlotOne = ColorSelection.Blue, SlotTwo = ColorSelection.Orange, SlotThree = ColorSelection.Red, SlotFour = ColorSelection.White };
            var result = GameEngine.TestGuessAgainstSolution(_guess, _solution);
            Assert.AreEqual<int>(result.NumberOfReds, 4);
            Assert.AreEqual<int>(result.NumberOfWhites, 0);
            Assert.IsTrue(result.IsSolved);
        }

        [TestMethod]
        public void TestTwoRedOneWhite()
        {
            Move _guess = new Move(
                ColorSelection.Blue, 
                ColorSelection.Orange, 
                ColorSelection.Red, 
                ColorSelection.White);
            Move _solution = new Move(
                ColorSelection.Blue, 
                ColorSelection.Orange, 
                ColorSelection.Yellow, 
                ColorSelection.Red);
            var result = GameEngine.TestGuessAgainstSolution(_guess, _solution);
            Assert.AreEqual<int>(result.NumberOfReds, 2);
            Assert.AreEqual<int>(result.NumberOfWhites, 1);
            Assert.IsTrue(result.IsSolved);
        }
    }
}
