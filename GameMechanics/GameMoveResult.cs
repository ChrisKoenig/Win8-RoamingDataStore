using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;

namespace RoamingDataStore
{
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
