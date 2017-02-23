using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportGenerator
{
    interface iprinter
    {
        void print(List<string> strings);
    }

    class consolePrint : iprinter
    {
        public void print(List<string> strings)
        {
            if(strings.Count() == 0)
            {
                Console.WriteLine("Nothing");
                return;
            }
            foreach(string s in strings)
            {
                Console.WriteLine(s);
            }
            
        }
    }
}
