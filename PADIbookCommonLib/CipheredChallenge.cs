using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PADIbookCommonLib
{
    [Serializable]
    public class CipheredChallenge
    {
        private byte[] _challenge;
        private string _senderid;
        private byte[] _signature;

        public byte[] Signature
        {
            get { return _signature; }
            set { _signature = value; }
        }

        public byte[] EncryptedResponse
        {
            get { return _challenge; }
            set { _challenge = value; }
        }

        public string Sender
        {
            get { return _senderid; }
            set { _senderid = value; }
        }

    }
}
