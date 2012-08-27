using System;
using System.Collections.Generic;
using System.Linq;

namespace GameLogic
{
    public class Game
    {
        public Game(Action doVictory, Action doFailure)
        {
            if (doVictory == null || doFailure == null)
            {
                throw new ArgumentException("Must supply values for both doVictory and doFailure");
            }
            doVictoryAction = doVictory;
            doFailureAction = doFailure;
            GenerateSolution();
            Moves = new List<GameMove>();
        }

        internal Game(GameMove solution, Action doVictory, Action doFailure)
        {
            // TODO: Complete member initialization
            _solution = solution;
            if (doVictory == null || doFailure == null)
            {
                throw new ArgumentException("Must supply values for both doVictory and doFailure");
            }
            doVictoryAction = doVictory;
            doFailureAction = doFailure;
            Moves = new List<GameMove>();  
        }

        private GameMove _solution;
        private readonly Action doVictoryAction;
        private readonly Action doFailureAction;
        public List<GameMove> Moves { get; set; }
        public int NumberOfMovesAllowed { get; set; }

        private void GenerateSolution()
        {
            Random random = new Random();
            int max = ColorSelection.ColorSwatches.Count();
            _solution = new GameMove(
                ColorSelection.ColorSwatches[random.Next(0, max)],
                ColorSelection.ColorSwatches[random.Next(0, max)],
                ColorSelection.ColorSwatches[random.Next(0, max)],
                ColorSelection.ColorSwatches[random.Next(0, max)]);
        }

        public int NumberOfMovesLeft
        {
            get
            {
                return NumberOfMovesAllowed - Moves.Count;
            }
        }

        public GameMoveResult RecordGuess(GameMove move)
        {
            Moves.Add(move);
            var result = GameEngine.TestGuessAgainstSolution(move, _solution);
            result.SequenceNumber = Moves.Count;
            if (result.IsSolved)
            {
                doVictoryAction.Invoke();
            }
            else
            {
                if (Moves.Count >= NumberOfMovesAllowed)
                {
                    doFailureAction.Invoke();
                }
            }
            return result;
        }
    }
}
