using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PADIbookCommonLib
{
    [Serializable]
    public class ObjectFile
    {
        private String _fileName;
        private String _content;
        private double _size;
        //private String _checksum;
       
        public String FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        public String Content
        {
            get { return _content; }
            set { _content = value; }
        }

        public Double Size
        {
            get { return _size; }
            set { _size = value; }
        }

        public ObjectFile(String name, String content, double size)
        {
            _fileName = name;
            _content = content;
            _size = size;
        }

        public ObjectFile()
        {
        }
    
    }
}
