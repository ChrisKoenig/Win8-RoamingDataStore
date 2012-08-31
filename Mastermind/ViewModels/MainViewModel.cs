using System;
using GameLogic;
using Windows.Storage;
using GalaSoft.MvvmLight;
using Mastermind.Helpers;
using Mastermind.Messages;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;

namespace Mastermind.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private const string STR_Gamejson = "game.json";
        private const string STR_Movesjson = "moves.json";

        #region Backing Stores

        private Game _game;
        private Visibility _SolutionVisibility;
        private ObservableCollection<PlayerMoveViewModel> _Moves = new ObservableCollection<PlayerMoveViewModel>();
        private string _MoveSlotFour;
        private string _MoveSlotThree;
        private string _MoveSlotTwo;
        private string _MoveSlotOne;

        #endregion Backing Stores

        #region Properties

        public Game CurrentGame
        {
            get
            {
                return _game;
            }
            set
            {
                _game = value;
                RaisePropertyChanged(() => this.CurrentGame);
            }
        }

        public Visibility SolutionVisibility
        {
            get { return _SolutionVisibility; }
            set
            {
                _SolutionVisibility = value;
                RaisePropertyChanged(() => this.SolutionVisibility);
            }
        }

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

        #endregion Properties

        #region Commands

        public RelayCommand ToggleSolutionVisibilityCommand { get; set; }
        public RelayCommand ToggleButonOneCommand { get; private set; }
        public RelayCommand ToggleButonTwoCommand { get; private set; }
        public RelayCommand ToggleButonThreeCommand { get; private set; }
        public RelayCommand ToggleButonFourCommand { get; private set; }
        public RelayCommand SubmitGuessCommand { get; private set; }
        public RelayCommand StartNewGameCommand { get; private set; }

        #endregion Commands

        public MainViewModel()
        {
            SubmitGuessCommand = new RelayCommand(() => SubmitGuess());
            StartNewGameCommand = new RelayCommand(() => StartNewGame());
            ToggleSolutionVisibilityCommand = new RelayCommand(() => ToggleSolutionVisibility());

            ToggleButonOneCommand = new RelayCommand(() => MoveSlotOne = CycleColor(MoveSlotOne));
            ToggleButonTwoCommand = new RelayCommand(() => MoveSlotTwo = CycleColor(MoveSlotTwo));
            ToggleButonThreeCommand = new RelayCommand(() => MoveSlotThree = CycleColor(MoveSlotThree));
            ToggleButonFourCommand = new RelayCommand(() => MoveSlotFour = CycleColor(MoveSlotFour));

            ApplicationData.Current.DataChanged += DataChangeHandler;

            Messenger.Default.Register<StartNewGameMessage>(this, (message) => StartNewGame());
            Messenger.Default.Register<LoadSavedGameMessage>(this, (message) => LoadSavedGame());
            Messenger.Default.Register<GameBoardReadyMessage>(this, (message) =>
            {
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
                CurrentGame = await StorageHelper.GetObjectFromRoamingFolder<Game>(appData, STR_Gamejson);
                CurrentGame.OnFailure += _game_OnFailure;
                CurrentGame.OnVictory += _game_OnVictory;
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
        }

        void _game_OnFailure(object sender, EventArgs e)
        {
            ClearSavedGameState();
            Messenger.Default.Send<FailureMessage>(new FailureMessage());
        }

        private void StartNewGame()
        {
            ClearSavedGameState();
            
            CurrentGame = GameEngine.CreateRandomGame();
            CurrentGame.OnFailure += _game_OnFailure;
            CurrentGame.OnVictory += _game_OnVictory;
            
            Moves.Clear();

            SolutionVisibility = Visibility.Collapsed;

            MoveSlotOne = "R";
            MoveSlotTwo = "R";
            MoveSlotThree = "R";
            MoveSlotFour = "R";

            //NOTE: I used to call out to save the game state, but have since remove this based on confusion to users
            //      when starting a new game and not playing any moves. Now, the game state is only saved and the
            //      GameInProgress indicators are set when the first move is made.
        }

        private void DataChangeHandler(ApplicationData appData, object o)
        {
            // TODO: Make a decision here to load the saved game data or not based on some rules. For now, just load it if you find it.
            LoadSavedGame(appData);
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

        }

        private async void SaveGameState()
        {
            StorageHelper.SetGameInProgress();
            await StorageHelper.SaveObjectToRoamingFolder(STR_Gamejson, _game);
            await StorageHelper.SaveObjectToRoamingFolder(STR_Movesjson, Moves);
            StorageHelper.PutObjectToSetting<string>("MoveSlotOne", MoveSlotOne);
            StorageHelper.PutObjectToSetting<string>("MoveSlotTwo", MoveSlotTwo);
            StorageHelper.PutObjectToSetting<string>("MoveSlotThree", MoveSlotThree);
            StorageHelper.PutObjectToSetting<string>("MoveSlotFour", MoveSlotFour);
        }

        private void ToggleSolutionVisibility()
        {
            SolutionVisibility = SolutionVisibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        }

    }
}