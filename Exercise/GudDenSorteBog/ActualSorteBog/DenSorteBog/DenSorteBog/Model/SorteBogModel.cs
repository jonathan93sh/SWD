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
            ObservableCollection<Person> tmp = Repository.ReadMeasurements();

            foreach(Person p in Repository.ReadMeasurements())
            {
                AddNewPerson(p);
            }

        }

        private static ObservableCollection<Person> Persons_ = new ObservableCollection<Person>();

        private ObservableCollection<Person> skyldere_ = new ObservableCollection<Person>();


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

        private double _totalGaeld;
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
            _totalGaeld = 0;
            foreach (Person p in skyldere_)
            {
                _totalGaeld += p.money;
            }
            NotifyPropertyChanged(m => m.totalGaeld);
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

        public void gemDenSorteBog()
        {
            Repository.WriteMeasurements(Persons_);
        }
    }
}
