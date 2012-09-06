using System;
using System.Linq;
using System.Collections.Generic;

namespace GameLogic
{
    public class GameEngine
    {

        public static GameMoveResult TestGuessAgainstSolution(GameMove theGuess, GameMove theSolution)
        {
            var result = new GameMoveResult();
            var placeTracker = new string[theGuess.Slots.Length];

            for (int position = 0; position < theGuess.Slots.Count(); position++)
            {
                var currentSlot = theGuess.Slots[position];

                // check the current slot against the corresponding color
                if (currentSlot.ColorCode == theSolution.Slots[position].ColorCode)
                {
                    result.NumberOfReds++;
                    placeTracker[position] = "R";
                }
                else
                {
                    // now check to see if you can find the corresponding color somewhere else
                    int index = 0;
                    while (index >= 0)
                    {
                        index = Array.FindIndex<ColorSelection>(theSolution.Slots, index, (color) => color.ColorCode == currentSlot.ColorCode);
                        if (index >= 0)
                        {
                            if (placeTracker[index] == null)
                            {
                                result.NumberOfWhites++;
                                placeTracker[index] = "W";
                                index = -1; // reset
                            }
                            else
                            {
                                // shift up one to increment the search
                                index++;
                            }
                        }
                    }
                }
            }
            result.NumberOfEmpties = theSolution.Slots.Count() - result.NumberOfReds - result.NumberOfWhites;
            return result;
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
