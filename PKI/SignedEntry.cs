using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PKI
{
    [Serializable]
    public class SignedEntry
    {
        private UserEntry _entry;
        private byte[] _signature;

        public UserEntry Entry
        {
            get { return _entry; }
            set { _entry = value; }
        }

        public byte[] Signature
        {
            get { return _signature; }
            set { _signature = value; }
        }
    }
}
