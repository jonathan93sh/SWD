using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenSorteBog.Model
{
    public class Person
    {

        public Person()
        { }

        public Person(Person other)
        {
            name = other.name;
            money = other.money;
        }

        private string name_;

        public string name { get { return name_; } set { name_ = value; } }

        private double money_;
        public double money { get { return money_; } set { money_ = value; } } 

    }
}
