using System;
using System.Linq;
using GalaSoft.MvvmLight;
using System.Collections.Generic;

namespace RoamingDataStore.ViewModels
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
