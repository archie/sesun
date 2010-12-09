using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PADIbookCommonLib
{
    public class FriendException : Exception
    {
        public FriendException(string message) : base(message)
        {
        }
    }
}