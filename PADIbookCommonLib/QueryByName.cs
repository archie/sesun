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
    }
}
