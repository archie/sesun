using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PADIbookCommonLib
{
    [Serializable]
    public class QueryByGenderAge : Query
    {
        private String _gender;
        private int _age;

        public String Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public QueryByGenderAge(String gender, int age, List<string> uris, List<string> contactingServerUri)
            : base(uris, contactingServerUri)
        {
            _gender = gender;
            _age = age;
        }
    }
}   
