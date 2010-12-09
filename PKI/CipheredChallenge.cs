using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PKI
{
    [Serializable]
    public class CipheredChallenge
    {
        private byte[] _challenge;
        private string _senderid;

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
