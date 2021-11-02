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
        public List<List<string>> VariablesNTypes = new List<List<string>>();//1-variable 2-type 3-pk if exist ,if not ->null, 4- unique index if exist if not->null, 5-nullable

        public DBTableStructure(string _Name)
        {
            Name = _Name;
        }

        public void LogTable()
        {
            Console.WriteLine("//////////////////////");
            Console.WriteLine(Name);
            Console.WriteLine($"Lenght: {VariablesNTypes.Count}");
            Console.WriteLine("----------------------");
            
            foreach (var itr in VariablesNTypes)
            {
                Console.WriteLine($"{itr[0]} | {itr[1]} | {itr[2]} | {itr[3]} | {itr[4]} | {itr[5]}");
            }
        }
    }
}
