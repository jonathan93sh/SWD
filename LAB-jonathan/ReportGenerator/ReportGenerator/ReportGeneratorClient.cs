using System;

namespace ReportGenerator
{
    internal class ReportGeneratorClient
    {
        private static void Main()
        {
            var db = new EmployeeDB();

            // Add some employees
            db.AddEmployee(new Employee("Anne", 3000, 20));
            db.AddEmployee(new Employee("Berit", 2000, 55));
            db.AddEmployee(new Employee("Christel", 1000, 24));

            dataCollectorEmployeeDB collector = new dataCollectorEmployeeDB(db);
            consolePrint printer = new consolePrint();
            Compiler compiler = new Compiler("Name");

            ReportGenerator RG = new ReportGenerator(printer, collector, compiler);

            // Create a default (name-first) report
            RG.start();

            Console.WriteLine("");
            Console.WriteLine("");

            compiler.ChangeFirst = "Salary";
            // Create a salary-first report
            RG.start();
            Console.WriteLine("");
            Console.WriteLine("");

            compiler.ChangeFirst = "Age";
            //age first
            RG.start();
            while (true) ;
        }
    }
}