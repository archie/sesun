using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PADIbookCommonLib
{
    public class SignedLookupResponse : SignedMessage
    {
        public List<RedirectionFile> FileList { get; set; }
        public string Username { get; set; }
        public string Uri { get; set; }
        public SignedLookupResponse(string username, string uri, 
            List<RedirectionFile> filelist, byte[] signature)
            : base(signature)
        {
            Username = username;
            Uri = uri;
            FileList = filelist;
        }
    }
}
