using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork2
{
    public class DBTableStructure
    {
        public string Name;
        public List<string[]> VariablesNTypes = new List<string[]>();

        public DBTableStructure(string _Name)
        {
            Name = _Name;
        }
    }
}
