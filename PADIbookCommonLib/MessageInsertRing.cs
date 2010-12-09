using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PADIbookCommonLib
{
    [Serializable]
    public class MessageInsertRing : Message
    {
        private String _before;
        private String _after;
        private Interest _interest;

        private int _actualize;

        public int Actualize
        {
            get { return _actualize; }
            set { _actualize = value; }
        }

        public Interest Interest
        {
            get { return _interest; }
            set { _interest = value; }
        }

        public String Before
        {
            get { return _before; }
            set { _before = value; }
        }

        public String After
        {
            get { return _after; }
            set { _after = value; }
        }

        public MessageInsertRing(int act,Interest inter,String before,String after,String name,List<String> uris,List<String> contactingServerUri)
            : base(name, uris, contactingServerUri)
        { _before = before; _after = after; _actualize = act; _interest = inter; }
    }

}
