using System;
using System.Collections.Generic;
using System.Linq;

namespace GameLogic
{
    public class GameMove
    {
        private readonly ColorSelection[] _slots;

        public GameMove()
        {
        }

        public GameMove(ColorSelection slotOne, ColorSelection slotTwo, ColorSelection slotThree, ColorSelection slotFour)
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
