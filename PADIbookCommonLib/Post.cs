using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PADIbookCommonLib
{
    [Serializable]
    public class Post
    {
        private String _text;
        private DateTime _timeStamp;
        private String _autor;

        public Post() { }

        public Post(String post,String autor) {
            this._timeStamp = DateTime.Now;
            this._text=post;
            this._autor = autor;
        }

        public String Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public String Autor
        {
            get { return _autor; }
            set { _autor = value; }
        }
        
        public DateTime TimeStamp
        {
            get { return this._timeStamp; }
            set { this._timeStamp = value; }

        }
        
    
    }
}
