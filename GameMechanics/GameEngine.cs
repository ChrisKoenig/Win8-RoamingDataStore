using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace RoamingDataStore
{
    public class GameEngine
    {
        private enum SlotStatus
        {
            Empty, Red, White
        }
        public static GameMoveResult TestGuessAgainstSolution(Move _guess, Move _solution)
        {

            var SlotOneStatus = SlotStatus.Empty;
            var SlotTwoStatus = SlotStatus.Empty;
            var SlotThreeStatus = SlotStatus.Empty;
            var SlotFourStatus = SlotStatus.Empty;


            if (_guess.SlotOne == _solution.SlotOne) SlotOneStatus = SlotStatus.Red;
            if (_guess.SlotTwo == _solution.SlotTwo) SlotTwoStatus = SlotStatus.Red;
            if (_guess.SlotThree == _solution.SlotThree) SlotThreeStatus = SlotStatus.Red;
            if (_guess.SlotFour == _solution.SlotFour) SlotFourStatus = SlotStatus.Red;



            // calculcate everything
            GameMoveResult result = new GameMoveResult();
            if (SlotOneStatus == SlotStatus.Red) result.NumberOfReds += 1;
            if (SlotTwoStatus == SlotStatus.Red) result.NumberOfReds += 1;
            if (SlotThreeStatus == SlotStatus.Red) result.NumberOfReds += 1;
            if (SlotFourStatus == SlotStatus.Red) result.NumberOfReds += 1;

            return result;
        }

        private Game _game;

        public void StartGame()
        {
            _game = new Game();
        }

        public void SubmitGuess(Move _move)
        {
            GameMoveResult _result = _game.RecordGuess(_move);
            if (_result.IsSolved)
            {
                // trigger game end
            }
            else
            {
                if (_game.NumberOfMovesLeft == 0)
                {
                    // trigger end of guesses
                }
            }
        }

    }
}