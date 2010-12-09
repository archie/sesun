using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PADIbookCommonLib
{
    [Serializable]
    public class RedirectionFile
    {
        private String _fileNameHash;
        private String _uri;

        public String FileNameHash
        {
            get { return _fileNameHash; }
            set { _fileNameHash = value; }
        }

        public String Uri
        {
            get { return _uri; }
            set { _uri = value; }
        }

        public RedirectionFile(String name, String uri)
        {
            _uri = uri;
            _fileNameHash = name;
        }

        public RedirectionFile()
        {
        }

    }
}
