using GalaSoft.MvvmLight.Messaging;
using Mastermind.Helpers;
using Mastermind.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Mastermind
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Messenger.Default.Register<VictoryMessage>(this, (message) => ShowVictory());
            Messenger.Default.Register<FailureMessage>(this, (message) => ShowFailure());
            Messenger.Default.Register<ErrorLoadingGameMessage>(this, (message) => ShowError(message.Error));
        }

        private void ShowError(Exception exception)
        {
            UICommand okCommand = new UICommand("OK", (cmd) =>
            {
                Messenger.Default.Send<StartNewGameMessage>(new StartNewGameMessage());
            }, 1);
            ShowMessage("Error loading game", exception.Message, new UICommand[] { okCommand });
        }

        private void ShowFailure()
        {
            UICommand newCommand = new UICommand("Start New Game", (cmd) =>
            {
                Messenger.Default.Send<StartNewGameMessage>(new StartNewGameMessage());
            }, 1);

            UICommand exitCommand = new UICommand("Exit Mastermind", (cmd) =>
            {
                App.Current.Exit();
            }, 2);

            ShowMessage("You Lost!", "Would you like to play again?", new UICommand[] { newCommand, exitCommand });
        }

        private void ShowVictory()
        {
            UICommand newCommand = new UICommand("Start New Game", (cmd) =>
            {
                Messenger.Default.Send<StartNewGameMessage>(new StartNewGameMessage());
            }, 1);

            UICommand exitCommand = new UICommand("Exit Mastermind", (cmd) =>
            {
                App.Current.Exit();
            }, 2);

            ShowMessage("You Won!", "Would you like to play again?", new UICommand[] { newCommand, exitCommand });
        }

        private async void ShowMessage(string title, string content, UICommand[] commands)
        {
            var dialog = new MessageDialog(content, title);

            foreach (var command in commands)
            {
                dialog.Commands.Add(command);
            }

            var result = await dialog.ShowAsync();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            CheckForGameRestore();
        }

        private async void CheckForGameRestore()
        {
            if (!StorageHelper.GameInProgress)
                return;

            var dialog = new MessageDialog("There is currently a saved game in progress - would you like to continue?", "Game in Progress");

            UICommand restoreCommand = new UICommand("Restore Game", (cmd) =>
            {
                Messenger.Default.Send<LoadSavedGameMessage>(new LoadSavedGameMessage());
            }, 1);

            UICommand newCommand = new UICommand("Start New Game", (cmd) =>
            {
                Messenger.Default.Send<StartNewGameMessage>(new StartNewGameMessage());
            }, 2);

            dialog.Commands.Add(restoreCommand);
            dialog.Commands.Add(newCommand);
            dialog.DefaultCommandIndex = 1;

            await dialog.ShowAsync();
        }

    }
}
