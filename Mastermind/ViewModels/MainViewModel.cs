using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using GameLogic;
using Mastermind.Helpers;
using Mastermind.Messages;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Windows.Storage;

namespace Mastermind.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private const string STR_Gamejson = "game.json";
        private const string STR_Movesjson = "moves.json";
        private const string DATE_FORMAT = "yyyy-M-dd hh:nn:ss";

        #region Backing Stores

        private bool _IsBusy;
        private bool _GameLocked;
        private string _MoveSlotFour;
        private string _MoveSlotThree;
        private string _MoveSlotTwo;
        private string _MoveSlotOne;
        private ObservableCollection<PlayerMoveViewModel> _Moves = new ObservableCollection<PlayerMoveViewModel>();

        #endregion Backing Stores

        #region MoveSlot Properties

        public ObservableCollection<PlayerMoveViewModel> Moves
        {
            get
            {
                return _Moves;
            }
            set
            {
                _Moves = value;
                RaisePropertyChanged(() => this.Moves);
            }
        }

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

        #endregion MoveSlot Properties

        #region ToggleButton Commands

        public RelayCommand ToggleButonOneCommand { get; private set; }

        public RelayCommand ToggleButonTwoCommand { get; private set; }

        public RelayCommand ToggleButonThreeCommand { get; private set; }

        public RelayCommand ToggleButonFourCommand { get; private set; }

        #endregion ToggleButton Commands

        private Game _game;

        public RelayCommand SubmitGuessCommand { get; private set; }

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
        
        public MainViewModel()
        {
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
            Messenger.Default.Register<LoadSavedGameMessage>(this, (message) => LoadSavedGame());
            Messenger.Default.Register<GameBoardReadyMessage>(this, (message) => {
                if (StorageHelper.GameInProgress)
                {
                    Messenger.Default.Send<AskForGameRestoreMessage>(new AskForGameRestoreMessage());
                }
                else
                {
                    StartNewGame();
                }
            });

        }

        private void LoadSavedGame()
        {
            LoadSavedGame(ApplicationData.Current);
        }

        private async void LoadSavedGame(ApplicationData appData)
        {
            try
            {
                _game = await StorageHelper.GetObjectFromRoamingFolder<Game>(appData, STR_Gamejson);
                _game.OnFailure += _game_OnFailure;
                _game.OnVictory += _game_OnVictory;
                Moves.Clear();
                var moves = await StorageHelper.GetObjectFromRoamingFolder<ObservableCollection<PlayerMoveViewModel>>(appData, STR_Movesjson);
                foreach (var move in moves)
                {
                    Moves.Add(move);
                }
                MoveSlotOne = StorageHelper.GetObjectFromSetting<string>(appData, "MoveSlotOne");
                MoveSlotTwo = StorageHelper.GetObjectFromSetting<string>(appData, "MoveSlotTwo");
                MoveSlotThree = StorageHelper.GetObjectFromSetting<string>(appData, "MoveSlotThree");
                MoveSlotFour = StorageHelper.GetObjectFromSetting<string>(appData, "MoveSlotFour");
            }
            catch (Exception ex)
            {
                // show error message
                ClearSavedGameState();
                Messenger.Default.Send<ErrorLoadingGameMessage>(new ErrorLoadingGameMessage(ex));

                // start new game
                StartNewGame();
            }
        }

        void _game_OnVictory(object sender, EventArgs e)
        {
            ClearSavedGameState();
            Messenger.Default.Send<VictoryMessage>(new VictoryMessage());
            GameLocked = true;
        }

        void _game_OnFailure(object sender, EventArgs e)
        {
            ClearSavedGameState();
            Messenger.Default.Send<FailureMessage>(new FailureMessage());
            GameLocked = true;
        }

        private void StartNewGame()
        {
            IsBusy = true;
            ClearSavedGameState();
            _game = GameEngine.CreateRandomGame();
            Moves.Clear();
            SaveGameState();
            IsBusy = false;
        }

        private void DataChangeHandler(ApplicationData appData, object o)
        {
            IsBusy = true;
            LoadSavedGame(appData);
            IsBusy = false;
        }

        private string CycleColor(string ColorCode)
        {
            string newColor = ColorSelection.GetNextColor(ColorCode);
            return newColor;
        }

        private void ClearSavedGameState()
        {
            StorageHelper.ClearGameState();
        }

        private void SubmitGuess()
        {
            IsBusy = true;

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

            SaveGameState();

            IsBusy = false;
        }

        private void SaveGameState()
        {
            StorageHelper.SetGameInProgress();
            StorageHelper.SaveObjectToRoamingFolder(STR_Gamejson, _game);
            StorageHelper.SaveObjectToRoamingFolder(STR_Movesjson, Moves);
            StorageHelper.PutObjectToSetting<string>("MoveSlotOne", MoveSlotOne);
            StorageHelper.PutObjectToSetting<string>("MoveSlotTwo", MoveSlotTwo);
            StorageHelper.PutObjectToSetting<string>("MoveSlotThree", MoveSlotThree);
            StorageHelper.PutObjectToSetting<string>("MoveSlotFour", MoveSlotFour);
        }

    }
}