using System;
using System.Linq;
using System.Collections.Generic;

namespace GameLogic
{
    public class Game
    {

        public static int MAX_MOVES_ALLOWED = 12;

        private GameMove _solution;
        public List<GameMove> Moves { get; set; }

        public event EventHandler OnVictory;
        public event EventHandler OnFailure;

        public Game()
        {
            InitBasicGame();
            GenerateSolution();
        }

        internal Game(GameMove solution)
        {
            InitBasicGame();
            _solution = solution;
        }

        private void InitBasicGame()
        {
            Moves = new List<GameMove>();
        }

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

        public GameMoveResult RecordGuess(GameMove move)
        {
            Moves.Add(move);
            var result = GameEngine.TestGuessAgainstSolution(move, _solution);
            result.SequenceNumber = Moves.Count;
            if (result.IsSolved)
            {
                IsSolved = true;
                if (OnVictory != null) OnVictory(this, new EventArgs());
            }
            else
            {
                IsSolved = false;
                if (Moves.Count >= Game.MAX_MOVES_ALLOWED)
                {
                    if (OnFailure != null) OnFailure(this, new EventArgs());
                }
            }
            return result;
        }

        #region Properties 

        public GameMove Solution
        {
            get
            {
                return _solution;
            }
            set
            {
                _solution = value;
            }
        }

        public int NumberOfMovesLeft
        {
            get
            {
                return Game.MAX_MOVES_ALLOWED - Moves.Count;
            }
        }

        #endregion 
    
        public bool IsSolved { get; set; }
    }
}
