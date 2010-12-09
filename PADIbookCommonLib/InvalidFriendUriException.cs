using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PADIbookCommonLib
{
    public class InvalidFriendUriException : FriendException
    {
        public InvalidFriendUriException(string message)
            : base(message)
        {
            
        }
    }
}
