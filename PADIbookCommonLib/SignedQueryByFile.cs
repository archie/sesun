using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PADIbookCommonLib
{
    [Serializable]
    public class SignedQueryByFile : SignedMessage
    {
        public QueryByFile Query { get; set; }
        public SignedQueryByFile(QueryByFile query, byte[] signature)
            : base(signature)
        {
            Query = query;
        }
    }
}
