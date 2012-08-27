using GalaSoft.MvvmLight.Messaging;
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
        }

        private void ShowFailure()
        {
            ShowMessage("You Lost!");
        }

        private void ShowVictory()
        {
            ShowMessage("You Won!");
        }

        private async void ShowMessage(string message)
        {
            var dialog = new MessageDialog("You Lost!  Close to start a new game", "You Lost!");

            UICommand newCommand = new UICommand("Start New Game", (cmd) =>
            {
                Messenger.Default.Send<StartNewGameMessage>(new StartNewGameMessage());
            }, 1);

            UICommand exitCommand = new UICommand("Exit Mastermind", (cmd) =>
            {
                App.Current.Exit();
            }, 2);

            dialog.Commands.Add(newCommand);
            dialog.Commands.Add(exitCommand);
            dialog.DefaultCommandIndex = 1;

            var result = await dialog.ShowAsync();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

    }
}
