using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace RoamingDataStore.GameLogic
{
    public class GameEngine
    {
        private Game _game;

        public void StartGame()
        {
            _game = new Game();
        }

        public void SubmitGuess(Move _move)
        {
            GameMoveResult results = _game.SubmitGuess(_move);
            
            if (results.IsSolved)
            {
                // trigger endgame
            }
            else
            {
                if (_game.NumberOfMovesLeft == 0)
                {
                    // trigger failed solution
                }
            }
        }

    }

    public class GameMoveResult : ObservableObject
    {
        // Fields...
        private int _NumberOfReds;
        private int _NumberOfWhites;

        public int NumberOfWhites
        {
            get { return _NumberOfWhites; }
            set
            {
                _NumberOfWhites = value;
                RaisePropertyChanged(() => this.NumberOfWhites);
            }
        }


        public int NumberOfReds
        {
            get { return _NumberOfReds; }
            set
            {
                _NumberOfReds = value;
                RaisePropertyChanged(() => this.NumberOfReds);
            }
        }
        
        public bool IsSolved
        {
            get
            {
                return _NumberOfReds == 4;
            }
        }
    }
}