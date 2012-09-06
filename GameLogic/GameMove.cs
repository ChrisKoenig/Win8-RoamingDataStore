using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace GameLogic
{
    public class GameMove
    {
        private ColorSelection[] _slots;

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
            set
            {
                _slots = value;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder(4);
            foreach (var item in _slots)
            {
                sb.Append(item.ColorCode);
            }
            return sb.ToString();
        }
    }
}
