using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PADIbookCommonLib
{
    [Serializable]
    public class QueryByFile : Query
    {
        private string _lowestId;

        public QueryByFile(String name, List<string> uris, List<string> contactingServerUri,string lowestId,DateTime date)
            : base(name , uris, contactingServerUri,date)
        {
            _lowestId = lowestId;
        }

        public QueryByFile() : base()
        {
        }

        public string LowestId
        {
            get { return _lowestId; }
            set { _lowestId = value; }
        }

        override public string ToString()
        {
            return Name + Uris.ElementAt(0) + ContactingServerUri.ElementAt(0) + LowestId + Id.ToString();
        }

        override public bool CompareTo(Query o)
        {
            QueryByFile q2 = (QueryByFile)o;
            if (this.Name.CompareTo(q2.Name) == 0 &&
                            this.LowestId.CompareTo(q2.LowestId) == 0 &&
                            this.Uris.ElementAt(0).CompareTo(q2.Uris.ElementAt(0)) == 0)
                return true;
            else
                return false;
        }

    }
}
