using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PADIbookCommonLib
{
    [Serializable]
    public class QueryByInterest : Query
    {
        private String _interest;

        public String Interest
        {
            get { return _interest; }
            set { _interest = value; }
        }

        public QueryByInterest(String name,String interest, List<String> uris, List<string> contactingServerUri)
            : base(name,uris,contactingServerUri)
        {
            _interest = interest;
        }
    }
}
