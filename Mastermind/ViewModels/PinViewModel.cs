using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml.Media;

namespace Mastermind.ViewModels
{
    public class PinViewModel : ObservableObject
    {

        public PinViewModel(string pinColor)
        {
            PinColor = pinColor;
        }

        private string _PinColor;

        public string PinColor 
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
        
     }
}
