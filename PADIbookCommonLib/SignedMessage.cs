using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PADIbookCommonLib
{
    [Serializable]
    abstract public class SignedMessage
    {
        public byte[] Signature { get; set; }
        public SignedMessage() { }
        public SignedMessage(byte[] signature) 
        {
            Signature = signature;
        }
    }
}
