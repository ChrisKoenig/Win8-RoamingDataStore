using System;
using System.Linq;
using System.Collections.Generic;

namespace RoamingDataStore.Messages
{
    public class ErrorLoadingGameMessage 
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
