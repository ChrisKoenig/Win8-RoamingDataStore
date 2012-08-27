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

            var game = GameEngine.CreateSampleGame(_solution,
                           () => OnVictoryFlag = true,
                           () => OnFailureFlag = true);

            var result = game.RecordGuess(_guess);

            Assert.AreEqual<int>(result.NumberOfReds, 4);
            Assert.AreEqual<int>(result.NumberOfWhites, 0);
            Assert.IsTrue(result.IsSolved);
            Assert.IsTrue(OnVictoryFlag);
            Assert.IsFalse(OnFailureFlag);

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

            var game = GameEngine.CreateSampleGame(_solution,
                           () => OnVictoryFlag = true,
                           () => OnFailureFlag = true);

            GameMoveResult result = new GameMoveResult();

            for (int i = 0; i < Game.MAX_MOVES_ALLOWED; i++)
            {
                result = game.RecordGuess(_guess);                
            }

            // just test the last one
            Assert.AreEqual<int>(result.NumberOfReds, 1);
            Assert.AreEqual<int>(result.NumberOfWhites, 0);
            Assert.IsFalse(result.IsSolved);
            Assert.IsFalse(OnVictoryFlag);
            Assert.IsTrue(OnFailureFlag);

        }
    
    }
}
