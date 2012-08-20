using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;

namespace RoamingDataStore.GameLogic
{
    public class Move : ObservableObject
    {
        private ColorSelection _slotOne;
        private ColorSelection _slotTwo;
        private ColorSelection _slotThree;
        private ColorSelection _slotFour;

        public ColorSelection SlotOne
        {
            get
            {
                return _slotOne;
            }
            set
            {
                if (_slotOne == value)
                    return;
                _slotOne = value;
                RaisePropertyChanged(() => this.SlotOne);
            }
        }

        public ColorSelection SlotTwo
        {
            get
            {
                return _slotTwo;
            }
            set
            {
                if (_slotTwo == value)
                    return;
                _slotTwo = value;
                RaisePropertyChanged(() => this.SlotTwo);
            }
        }

        public ColorSelection SlotThree
        {
            get
            {
                return _slotThree;
            }
            set
            {
                if (_slotThree == value)
                    return;
                _slotThree = value;
                RaisePropertyChanged(() => this.SlotThree);
            }
        }

        public ColorSelection SlotFour
        {
            get
            {
                return _slotFour;
            }
            set
            {
                if (_slotFour == value)
                    return;
                _slotFour = value;
                RaisePropertyChanged(() => this.SlotFour);
            }
        }

    }
}
