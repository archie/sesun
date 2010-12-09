using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PADIbookCommonLib
{
    [Serializable]
    public class MessageRegister :Message
    {
        private List<Interest> _interests;

        public List<Interest> Interests
        {
            get { return _interests; }
            set { _interests = value; }
        }

        public MessageRegister(String name, List<String> uris, List<String> contactingServerUri)
            : base(name, uris, contactingServerUri)
        { _interests = new List<Interest>(); }
    }
}
