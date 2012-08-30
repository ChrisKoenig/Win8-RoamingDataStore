using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mastermind.Messages
{
    public class ErrorLoadingGameMessage : Exception
    {
        private Exception ex;

        public ErrorLoadingGameMessage(Exception ex)
        {
            // TODO: Complete member initialization
            this.ex = ex;
        }

        public Exception Error
        {
            get { return ex; }
        }
    }
}
