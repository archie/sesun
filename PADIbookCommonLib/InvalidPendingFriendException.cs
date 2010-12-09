using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PADIbookCommonLib
{
    public class InvalidPendingFriendException : FriendException
    {
        public InvalidPendingFriendException(string message) : base(message)
        {
        }
    }
}
