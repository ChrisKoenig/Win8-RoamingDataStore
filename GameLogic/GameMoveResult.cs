using System;
using System.Collections.Generic;
using System.Linq;

namespace GameLogic
{

    public class GameMoveResult
    {

        public int NumberOfEmpties { get; set; }
        public int NumberOfWhites { get; set; }
        public int NumberOfReds { get; set; }

        public bool IsSolved
        {
            get
            {
                return NumberOfReds == 4;
            }
        }

        public int SequenceNumber { get; set; }
    }
}