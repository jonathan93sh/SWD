using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportGenerator
{
    class ReportGenerator
    {        
        private iprinter printer_;

        private iCollector collector_;

        private iCompiler compiler_;
        public ReportGenerator(iprinter printer, iCollector collector, iCompiler compiler)
        {
            printer_ = printer;
            collector_ = collector;
            compiler_ = compiler;
        }

        public void start()
        {
            List<Cdata> Cd = new List<Cdata>();

            for(int i = 0; i < collector_.count; i++)
            {
                Cd.Add(collector_.getData(i));
            }

            List<string> compilerData = new List<string>();

            compilerData = compiler_.combileData(Cd);
                

            printer_.print(compilerData);
        }
    }



    struct Cdata
    {
        public List<string> Type;
        public List<string> Value;
    };

    interface iCollector
    {
        int count{get;}
        Cdata getData(int i);
    }

    class dataCollectorEmployeeDB : iCollector
    {
         
        private EmployeeDB DB_;
        List<Employee> employees_ = new List<Employee>();

        public dataCollectorEmployeeDB(EmployeeDB DB)
        {
            Employee employee;

            DB_ = DB;
            DB_.Reset();
            while ((employee = DB_.GetNextEmployee()) != null)
            {
                employees_.Add(employee);
            }

        }

        public int count
        {
            get
            {
                return employees_.Count();
            }
        }

        public Cdata getData(int i)
        {
            Cdata dat = new Cdata();
            
            dat.Type = new List<string>();
            dat.Value = new List<string>();
            
            dat.Type.Add("Name");
            dat.Value.Add(employees_[i].Name);

            dat.Type.Add("Salary");
            dat.Value.Add(employees_[i].Salary.ToString());

            dat.Type.Add("Age");
            dat.Value.Add(employees_[i].Age.ToString());

            return dat;
        }

    }

    interface iCompiler
    {
        List<string> combileData(List<Cdata> data);
    }

    class Compiler : iCompiler
    {
        private String first_;
        public Compiler(String first)
        {
            ChangeFirst = first;
        }

        public string ChangeFirst
        {
            get
            {
                return first_;
            }

            set
            {
                first_ = value;
            }
        }

        public List<string> combileData(List<Cdata> data)
        {
            List<string> tmp = new List<string>();
            tmp.Add(first_ + "-first report");
            foreach(Cdata d in data)
            {
                int startItem = -1;
                tmp.Add("------------------");

                for(int i = 0; i < d.Type.Count(); i++)
                {
                    if(d.Type[i].ToLower() == first_.ToLower())
                    {
                        startItem = i;
                        tmp.Add(makeString(d.Type[i], d.Value[i]));
                    }
                }

                for (int i = 0; i < d.Type.Count(); i++)
                {
                    if (i != startItem)
                    {
                        tmp.Add(makeString(d.Type[i], d.Value[i]));
                    }
                }
                tmp.Add("------------------");

            }

            return tmp;
        }

        private string makeString(string type, string value)
        {
            return type + ": {0}" + value;
        }
    }

}
