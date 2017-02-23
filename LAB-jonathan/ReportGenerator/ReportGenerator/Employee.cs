namespace ReportGenerator
{
    public class Employee
    {
        public Employee() : this("", 0, 0)
        {
        }

        public Employee(string name, uint salary, uint age)
        {
            Name = name;
            Salary = salary;
            Age = age;
        }

        public string Name { get; private set; }
        public uint Salary { get; private set; }
        public uint Age { get; private set; }
    }
}