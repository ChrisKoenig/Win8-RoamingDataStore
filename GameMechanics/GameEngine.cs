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

        public static GameMoveResult TestGuessAgainstSolution(Move _guess, Move _solution)
        {
            // calculcate everything
            GameMoveResult result = new GameMoveResult();
            var _guessString = string.Join("", _guess.Slots.Select(s => s.ColorCode));
            var _solutionString = string.Join("", _solution.Slots.Select(s => s.ColorCode));
            var _check = CheckGuess(_solutionString, _guessString);
            result.NumberOfReds = _check.ToCharArray().Count(c => c == '+');
            result.NumberOfWhites = _check.ToCharArray().Count(c => c == '-');
            result.NumberOfEmpties = _check.ToCharArray().Count(c => c == 'X');
            return result;
        }

        public static string CheckGuess(string code, string guess)
        {
            char[] array = code.ToCharArray();
            int num = 0;
            char[] array2 = new char[]
	        {
		        'X',
		        'X',
		        'X',
		        'X'
	        };
            for (int i = 0; i < 4; i++)
            {
                char guessChar = guess[i];
                int num2 = Array.FindIndex<char>(array, (char codeChar) => codeChar == guessChar);
                if (num2 > -1)
                {
                    array2[num] = GetMarker(num2, i);
                    num++;
                    array[num2] = 'X';
                }
            }
            return new string(array2);
        }

        private static char GetMarker(int searchIndex, int guessIndex)
        {
            if (searchIndex != guessIndex)
            {
                return '-';
            }
            return '+';
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