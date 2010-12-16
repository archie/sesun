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

    }
}
