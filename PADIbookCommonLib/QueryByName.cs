using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PADIbookCommonLib
{
    [Serializable]
    public class QueryByName : Query
    {
        public QueryByName(String name, List<string> uris, List<string> contactingServerUri)
            : base(name , uris, contactingServerUri)
        {

        }

        public QueryByName() : base(){ }
    }
}
