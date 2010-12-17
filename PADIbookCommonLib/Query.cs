using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PADIbookCommonLib
{
    [Serializable]
    public class Query
    {
        private DateTime _id;
        private List<string> _contactingServerUri;
        private List<string> _uris;
        private String _name;
        

        public Query() { }

        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public DateTime Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public List<string> ContactingServerUri
        {
            get { return _contactingServerUri; }
            set { _contactingServerUri = value; }
        }

        public List<string> Uris
        {
            get { return _uris; }
            set { _uris = value; }
        }

        public void refreshTimeStamp()
        {
            _id = DateTime.Now;
        }

        public Query(List<string> uris, List<string> contactingServerUri,DateTime id)
        {
            _uris = uris;
            _contactingServerUri = contactingServerUri;
            _id = id;
            _name = "";
        }
        public Query(String name, List<string> uris, List<string> contactingServerUri,DateTime id)
        {
            _uris = uris;
            _contactingServerUri = contactingServerUri;
            _id = id;
            _name = name;
        }
    }
}
