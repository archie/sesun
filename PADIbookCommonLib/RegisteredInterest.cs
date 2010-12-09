using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PADIbookCommonLib
{
    [Serializable]
    public class RegisteredInterest
    {
        private Interest _interest;
        private String _ringNextUri;
        private String _ringBeforeUri;

        public Interest Interest
        {
            get { return _interest; }
            set { _interest = value; }
        }

        public String RingNextUri
        {
            get { return _ringNextUri; }
            set { _ringNextUri = value; }
        }

        public String RingBeforeUri
        {
            get { return _ringBeforeUri; }
            set { _ringBeforeUri = value; }
        }

        public RegisteredInterest(Interest inter, String before, String next)
        {
            _interest = inter;
            _ringBeforeUri = before;
            _ringNextUri = next;

        }
    }
}
