using GalaSoft.MvvmLight;
using GameLogic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Mastermind.ViewModels
{
    public class PinViewModel : ObservableObject
    {
        private SolidColorBrush _PinColor;

        public SolidColorBrush PinColor
        {
            get { return _PinColor; }
            set
            {
                if (_PinColor == value)
                    return;
                _PinColor = value;
                RaisePropertyChanged(() => this.PinColor);
            }
        }
        
        public PinViewModel(Color color)
        {
            PinColor = new SolidColorBrush(color);
        }
    }

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

        public ObservableCollection<PinViewModel> Results { get; set; }

        public GameMoveResult ResultData
        {
            get { return _ResultData; }
            set
            {
                if (_ResultData == value)
                    return;
                _ResultData = value;
                Results = new ObservableCollection<PinViewModel>();
                for (int i = 0; i < value.NumberOfReds; i++)
                {
                    Results.Add(new PinViewModel(Colors.Red));
                }
                for (int i = 0; i < value.NumberOfWhites; i++)
                {
                    Results.Add(new PinViewModel(Colors.White));
                } 
                RaisePropertyChanged(() => this.ResultData);
            }
        }


    }
}
