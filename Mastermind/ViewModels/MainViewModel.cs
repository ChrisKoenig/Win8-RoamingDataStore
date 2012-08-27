using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GameLogic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind.ViewModels
{
    public class MainViewModel : ObservableObject
    {

        #region Backing Stores

        private string _MoveSlotFour;
        private string _MoveSlotThree;
        private string _MoveSlotTwo;
        private string _MoveSlotOne;

        #endregion

        #region MoveSlot Properties

        public string MoveSlotOne
        {
            get { return _MoveSlotOne; }
            set
            {
                if (_MoveSlotOne == value)
                    return;
                _MoveSlotOne = value;
                RaisePropertyChanged(() => this.MoveSlotOne);
            }
        }

        public string MoveSlotTwo
        {
            get { return _MoveSlotTwo; }
            set
            {
                if (_MoveSlotTwo == value)
                    return;
                _MoveSlotTwo = value;
                RaisePropertyChanged(() => this.MoveSlotTwo);
            }
        }

        public string MoveSlotThree
        {
            get { return _MoveSlotThree; }
            set
            {
                if (_MoveSlotThree == value)
                    return;
                _MoveSlotThree = value;
                RaisePropertyChanged(() => this.MoveSlotThree);
            }
        }


        public string MoveSlotFour
        {
            get { return _MoveSlotFour; }
            set
            {
                if (_MoveSlotFour == value)
                    return;
                _MoveSlotFour = value;
                RaisePropertyChanged(() => this.MoveSlotFour);
            }
        }

        #endregion

        #region ToggleButton Commands

        public RelayCommand ToggleButonOneCommand { get; private set; }
        public RelayCommand ToggleButonTwoCommand { get; private set; }
        public RelayCommand ToggleButonThreeCommand { get; private set; }
        public RelayCommand ToggleButonFourCommand { get; private set; }

        #endregion

        private readonly Game _game;
        public ObservableCollection<PlayerMoveViewModel> Moves { get; private set; }
        public RelayCommand SubmitGuessCommand { get; private set; }

        public MainViewModel()
        {
            Moves = new ObservableCollection<PlayerMoveViewModel>();
            SubmitGuessCommand = new RelayCommand(() => SubmitGuess());

            MoveSlotOne = "R";
            MoveSlotTwo = "R";
            MoveSlotThree = "R";
            MoveSlotFour = "R";

            ToggleButonOneCommand = new RelayCommand(() => MoveSlotOne = CycleColor(MoveSlotOne));
            ToggleButonTwoCommand = new RelayCommand(() => MoveSlotTwo = CycleColor(MoveSlotTwo));
            ToggleButonThreeCommand = new RelayCommand(() => MoveSlotThree = CycleColor(MoveSlotThree));
            ToggleButonFourCommand = new RelayCommand(() => MoveSlotFour = CycleColor(MoveSlotFour));

            _game = GameEngine.CreateRandomGame(OnVictory, OnFailure);
        }

        private string CycleColor(string ColorCode)
        {
            string newColor = ColorSelection.GetNextColor(ColorCode);
            return newColor;
        }

        private void OnVictory()
        {
            //throw new NotImplementedException();
        }

        private void OnFailure()
        {
            //throw new NotImplementedException();
        }

        private void SubmitGuess()
        {
            var pvm = new PlayerMoveViewModel();

            var guess = new GameMove(
                ColorSelection.FindColorSwatchByColorCode(_MoveSlotOne),
                ColorSelection.FindColorSwatchByColorCode(_MoveSlotTwo),
                ColorSelection.FindColorSwatchByColorCode(_MoveSlotThree),
                ColorSelection.FindColorSwatchByColorCode(_MoveSlotFour));

            var result = _game.RecordGuess(guess);

            pvm.MoveData = guess;
            pvm.ResultData = result;

            Moves.Add(pvm);

        }

    }
}