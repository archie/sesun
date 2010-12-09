using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PADIbookCommonLib
{
    [Serializable]
    public class Friend
    {
        private string _name;
        private List<String> _uris;
        private bool _sucessorSwarm;

        public Friend() { _sucessorSwarm = false; }

        public Friend(string name)
        {
            this._name = name;
            this._uris = new List<String>();
            _sucessorSwarm = false;
        }

        public Friend(string name, List<string> uris)
        {
            _name = name;
            _uris = uris;
            _sucessorSwarm = false;
        }

        public Friend(string name, List<string> uris, bool isSucessorSwarm)
        {
            _name = name;
            _uris = uris;
            _sucessorSwarm = isSucessorSwarm;
        }

        public bool SucessorSwarm
        {
            get { return this._sucessorSwarm; }
            set { _sucessorSwarm = value; }
        }


        public String Name
        {
            get { return this._name; }
            set { _name = value; }
        }

        public List<String> Uris
        {
            get { return this._uris; }
            set { _uris = value; }
        }
        
        public void addUri(String uri)
        {
            this._uris.Add(uri);
        }

        public void removeUri(String uri)
        {
            this._uris.Remove(uri);
        }

        public void setPrimaryUri(string uri)
        {
            _uris[0] = uri;
        }
    }
}

