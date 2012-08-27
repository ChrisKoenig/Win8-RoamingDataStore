using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using GameLogic;
using Mastermind.Helpers;
using Mastermind.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.Storage;

namespace Mastermind.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private const string STR_Gamejson = "game.json";
        private const string STR_Movesjson = "moves.json";

        #region Backing Stores

        private bool _IsBusy;
        private bool _GameLocked;
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

        private Game _game;
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

            ApplicationData.Current.DataChanged += DataChangeHandler;

            Messenger.Default.Register<StartNewGameMessage>(this, (message) => StartNewGame());
            StartNewGame();
        }

        private void StartNewGame()
        {
            IsBusy = true;
            _game = GameEngine.CreateRandomGame(OnVictory, OnFailure);
            Moves.Clear();
            SaveState();
            IsBusy = false;
        }

        private async void DataChangeHandler(ApplicationData appData, object o)
        {
            IsBusy = true;

            _game = await StorageHelper.GetObjectFromRoamingFolder<Game>(appData, STR_Gamejson);
            Moves = await StorageHelper.GetObjectFromRoamingFolder<ObservableCollection<PlayerMoveViewModel>>(appData, STR_Movesjson);
            MoveSlotOne = StorageHelper.GetObjectFromSetting<string>(appData, "MoveSlotOne");
            MoveSlotTwo = StorageHelper.GetObjectFromSetting<string>(appData, "MoveSlotTwo");
            MoveSlotThree = StorageHelper.GetObjectFromSetting<string>(appData, "MoveSlotThree");
            MoveSlotFour = StorageHelper.GetObjectFromSetting<string>(appData, "MoveSlotFour");

            IsBusy = false;
        }

        private string CycleColor(string ColorCode)
        {
            string newColor = ColorSelection.GetNextColor(ColorCode);
            return newColor;
        }

        private void OnVictory()
        {
            Messenger.Default.Send<VictoryMessage>(new VictoryMessage());
            GameLocked = true;
        }

        private void OnFailure()
        {
            Messenger.Default.Send<FailureMessage>(new FailureMessage());
            GameLocked = true;
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

            DispatcherHelper.CheckBeginInvokeOnUI(() => Moves.Add(pvm));

            SaveState();
        }

        private void SaveState()
        {
            var appData = ApplicationData.Current;
            StorageHelper.SaveObjectToRoamingFolder(appData, STR_Gamejson, _game);
            StorageHelper.SaveObjectToRoamingFolder(appData, STR_Movesjson, Moves);
            StorageHelper.PutObjectToSetting<string>(appData, "MoveSlotOne", MoveSlotOne);
            StorageHelper.PutObjectToSetting<string>(appData, "MoveSlotTwo", MoveSlotTwo);
            StorageHelper.PutObjectToSetting<string>(appData, "MoveSlotThree", MoveSlotThree);
            StorageHelper.PutObjectToSetting<string>(appData, "MoveSlotFour", MoveSlotFour);
        }

        public bool GameLocked
        {
            get
            {
                return _GameLocked;
            }
            set
            {
                _GameLocked = value;
                RaisePropertyChanged(() => this.GameLocked);
            }
        }

        public bool IsBusy
        {
            get
            {
                return _IsBusy;
            }
            set
            {
                _IsBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

    }
}