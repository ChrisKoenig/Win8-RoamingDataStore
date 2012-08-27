using GalaSoft.MvvmLight;
using GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mastermind.ViewModels
{
    public class PlayerMoveViewModel : ObservableObject
    {
        // Fields...
        private GameMoveResult _ResultData;
        private GameMove _MoveData;

        public GameMove MoveData
        {
            get { return _MoveData; }
            set
            {
                if (_MoveData == value)
                    return;
                _MoveData = value;
                RaisePropertyChanged(() => this.MoveData);
            }
        }


        public GameMoveResult ResultData
        {
            get { return _ResultData; }
            set
            {
                if (_ResultData == value)
                    return;
                _ResultData = value;
                RaisePropertyChanged(() => this.ResultData);
            }
        }


    }
}
