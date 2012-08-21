using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;

namespace RoamingDataStore
{
    public class Move : ObservableObject
    {
        private ColorSelection[] _slots;

        public Move()
        {
        }

        public Move(ColorSelection slotOne, ColorSelection slotTwo, ColorSelection slotThree, ColorSelection slotFour)
        {
            _slots = new ColorSelection[] { slotOne, slotTwo, slotThree, slotFour };
        }

        public ColorSelection[] Slots
        {
            get
            {
                return _slots;
            }
        }
    }
}
