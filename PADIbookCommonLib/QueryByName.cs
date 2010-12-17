using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PADIbookCommonLib
{
    [Serializable]
    public class QueryByName : Query
    {
        public QueryByName(String name, List<string> uris, List<string> contactingServerUri,DateTime date)
            : base(name , uris, contactingServerUri,date)
        {

        }

        public QueryByName() : base(){ }

        override public string ToString()
        {
            return Name + Uris.ElementAt(0) + ContactingServerUri.ElementAt(0) + Id.ToString();
        }

        override public bool CompareTo(Query o)
        {
            QueryByName q2 = (QueryByName)o;
            if (this.Name.CompareTo(q2.Name) == 0 && 
                this.Uris.ElementAt(0).CompareTo(q2.Uris.ElementAt(0)) == 0 &&
                this.Id.CompareTo(q2.Id) == 0)
                return true;
            else
                return false;
        }
    }
}
