using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Tests
{
    [TestClass]
    public class GameTests
    {
        private bool OnVictoryFlag = false;
        private bool OnFailureFlag = false;


        [TestMethod]
        public void TestSampleGameSuccess()
        {
            OnVictoryFlag = false;
            OnFailureFlag = false;

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

            var game = GameEngine.CreateSampleGame(_solution);
            game.OnVictory += (s, e) => OnVictoryFlag = true;
            game.OnFailure += (s, e) => OnFailureFlag = true;

            var result = game.RecordGuess(_guess);

            Assert.AreEqual<int>(4, result.NumberOfReds, "Reds");
            Assert.AreEqual<int>(0, result.NumberOfWhites, "Whites");
            Assert.AreEqual<int>(0, result.NumberOfEmpties, "Empties");
            Assert.IsTrue(result.IsSolved, "Solved");
            Assert.IsTrue(OnVictoryFlag, "Victory");
            Assert.IsFalse(OnFailureFlag, "Failure");

        }


        [TestMethod]
        public void TestSampleGameFailure()
        {
            OnVictoryFlag = false;
            OnFailureFlag = false;

            GameMove _solution = new GameMove(
                ColorSelection.FindColorSwatchByColorName("Blue"),
                ColorSelection.FindColorSwatchByColorName("Orange"),
                ColorSelection.FindColorSwatchByColorName("Red"),
                ColorSelection.FindColorSwatchByColorName("White"));

            GameMove _guess = new GameMove(
                ColorSelection.FindColorSwatchByColorName("Blue"),
                ColorSelection.FindColorSwatchByColorName("Blue"),
                ColorSelection.FindColorSwatchByColorName("Blue"),
                ColorSelection.FindColorSwatchByColorName("Blue"));

            var game = GameEngine.CreateSampleGame(_solution);
            game.OnVictory += (s, e) => OnVictoryFlag = true;
            game.OnFailure += (s, e) => OnFailureFlag = true;

            GameMoveResult result = new GameMoveResult();

            for (int i = 0; i < Game.MAX_MOVES_ALLOWED; i++)
            {
                result = game.RecordGuess(_guess);
            }

            // just test the last one
            Assert.AreEqual<int>(1, result.NumberOfReds, "Reds");
            Assert.AreEqual<int>(0, result.NumberOfWhites, "Whites");
            Assert.AreEqual<int>(3, result.NumberOfEmpties, "Empties");
            Assert.IsFalse(result.IsSolved, "Solved");
            Assert.IsFalse(OnVictoryFlag, "OnVictory");
            Assert.IsTrue(OnFailureFlag, "OnFailure");

        }




    }
}
