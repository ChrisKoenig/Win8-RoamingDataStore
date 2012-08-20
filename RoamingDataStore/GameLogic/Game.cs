using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;

namespace RoamingDataStore.GameLogic
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
            _solution = new Move()
            {
                SlotOne = (ColorSelection)random.Next(0, 3),
                SlotTwo = (ColorSelection)random.Next(0, 3),
                SlotThree = (ColorSelection)random.Next(0, 3),
                SlotFour = (ColorSelection)random.Next(0, 3),
            };
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

        internal bool IsSolution(Move _move)
        {
            return (_solution.SlotOne == _move.SlotOne &&
                    _solution.SlotTwo == _move.SlotTwo &&
                    _solution.SlotThree == _move.SlotThree &&
                    _solution.SlotTwo == _move.SlotFour);
        }

        internal GameMoveResult SubmitGuess(Move _move)
        {
            GameMoveResult _result = new GameMoveResult();
            
            if (_move.SlotOne == _solution.SlotFour) _result.NumberOfReds += 1;
            if (_move.SlotTwo == _solution.SlotFour) _result.NumberOfReds += 1;
            if (_move.SlotThree == _solution.SlotFour) _result.NumberOfReds += 1;
            if (_move.SlotFour == _solution.SlotFour) _result.NumberOfReds += 1;

            return _result;

        }
    }
}
