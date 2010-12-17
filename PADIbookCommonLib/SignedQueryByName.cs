using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PADIbookCommonLib
{
    [Serializable]
    public class SignedQueryByName : SignedMessage 
    {
        public QueryByName Query { get; set; }

        public SignedQueryByName(QueryByName query, byte[] signature)
            : base(signature)
        {
            Query = query;
        }

        public SignedQueryByName()
            : base()
        {
        }
    }
}
