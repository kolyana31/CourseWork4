using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork2
{
    public class DBType
    {
        public string name;
        public bool NeedParametrs;

        public DBType(string _name, bool need)
        {
            name = _name;
            NeedParametrs = need;
        }
    }
}
