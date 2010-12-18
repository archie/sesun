using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PADIbookCommonLib
{
    [Serializable]
    public class UserEntry : IComparable<UserEntry>
    {
        private string _address;
        private int _port;
        private string _node_id;
        private string _pubkey;

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public int Port 
        {
            get { return _port; }
            set { _port = value; }
        }

        public string NodeId
        {
            get { return _node_id; }
            set { _node_id = value; }
        }

        public string PubKey
        {
            get { return _pubkey; }
            set { _pubkey = value; }
        }

        override public string ToString()
        {
            return Address + Port + NodeId + PubKey;
        }

        int IComparable.CompareTo(UserEntry e)
        {
            return String.Compare(this.NodeId, e.NodeId);
        }
    }
}
