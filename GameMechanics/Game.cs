using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;

namespace RoamingDataStore
{
    public class Game : ObservableObject
    {

        /// <summary>
        /// Initializes a new instance of the Game class.
        /// </summary>
        public Game()
        {
            GenerateSolution();
            Moves = new ObservableCollection<Move>();
        }

        private Move _solution;
        private void GenerateSolution()
        {
            Random random = new Random();
            int min = 0;
            int max = ColorSelection.ColorSwatches.Count();
            _solution = new Move(
                ColorSelection.ColorSwatches[random.Next(min, max)],
                ColorSelection.ColorSwatches[random.Next(min, max)],
                ColorSelection.ColorSwatches[random.Next(min, max)],
                ColorSelection.ColorSwatches[random.Next(min, max)]);
        }

        public ObservableCollection<Move> Moves { get; set; }

        public int NumberOfMovesLeft
        {
            get
            {
                return 10 - Moves.Count;
            }
        }

        private Move _currentMove;
        public Move CurrentMove
        {
            get
            {
                return _currentMove;
            }
            set
            {
                if (_currentMove == value)
                    return;
                _currentMove = value;
                RaisePropertyChanged(() => this.CurrentMove);
            }
        }

        internal GameMoveResult RecordGuess(Move _move)
        {
            Moves.Add(_move);
            return GameEngine.TestGuessAgainstSolution(_move, _solution);
        }
    }
}
