using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using DenSorteBog.DAL;

using SimpleMvvmToolkit;

namespace DenSorteBog.Model
{
    public class SorteBogModel : ModelBase<SorteBogModel>
    {

        public SorteBogModel()
        {
            if(Persons_ == null || skyldere_ == null)
            {
                Persons_ = new ObservableCollection<Person>();
                skyldere_ = new ObservableCollection<Person>();
                ObservableCollection<Person> tmp = Repository.ReadPersons();

                foreach (Person p in Repository.ReadPersons())
                {
                    AddNewPerson(p);
                }
            }


            

        }

        private static ObservableCollection<Person> Persons_ = null;

        private static ObservableCollection<Person> skyldere_ = null;


        public ObservableCollection<Person> Persons { get { return Persons_; }
            set { Persons_ = value;}
        }

        public ObservableCollection<Person> PersonsSkyldere 
        {
            get 
            {
                return skyldere_;  
            }
            private set
            {

                skyldere_ = value;
            }
            
        }

        private static double _totalGaeld;
        public double totalGaeld
        {
            get { return _totalGaeld; }
            set
            {
                _totalGaeld = value;
                NotifyPropertyChanged(m => m.totalGaeld);
            }
        }

        public void RemovePerson(Person p)
        {
            if (p != null)
            {
                Persons.Remove(p);
                PersonsSkyldere.Remove(p);
            }
            calcTotalGaeld();
        }

        public void AddNewPerson(Person p)
        {
            Persons.Add(p);
            if(p.money < 0)
            {
                PersonsSkyldere.Add(p);
            }
            calcTotalGaeld();
        }

        private void calcTotalGaeld()
        {
            double tmpGaeld = 0;
            foreach (Person p in skyldere_)
            {
                tmpGaeld += p.money;
            }
            totalGaeld = tmpGaeld;
        }

        public bool nameExist(string name)
        {
            foreach(Person p in Persons)
            {
                if (p.name == name)
                    return true;
            }
            return false;
        }

        public void ChangePerson(Person p, string newName, double value)
        {
            RemovePerson(p);

            AddNewPerson(new Person() { name = newName, money = value });
        }

        public void gemDenSorteBog()
        {
            Repository.WritePersons(Persons_);
        }
    }
}
