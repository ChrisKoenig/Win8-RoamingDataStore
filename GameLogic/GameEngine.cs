using System;
using System.Linq;
using System.Collections.Generic;

namespace GameLogic
{
    public class GameEngine
    {

        public static GameMoveResult TestGuessAgainstSolution(GameMove _guess, GameMove _solution)
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

        private static string CheckGuess(string code, string guess)
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

        public static Game CreateSampleGame(GameMove solution)
        {
            return new Game(solution);
        }

        public static Game CreateRandomGame()
        {
            return new Game();
        }

    }
}
