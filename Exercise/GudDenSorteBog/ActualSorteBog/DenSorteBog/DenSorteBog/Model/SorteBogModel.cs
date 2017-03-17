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

        private static ObservableCollection<Person> Persons_ = Repository.ReadMeasurements();

        private ObservableCollection<Person> skyldere_ = new ObservableCollection<Person>();


        public ObservableCollection<Person> Persons { get { return Persons_; }
            set { Persons_ = value; NotifyPropertyChanged<ObservableCollection<Person>>(m => m.Persons); ObservableCollection<Person> PersonsSkylderetmp = Persons; PersonsSkyldere = Persons; }
        }

        public ObservableCollection<Person> PersonsSkyldere 
        {
            get 
            {
                
                skyldere_.Clear();
                foreach (var item in Persons)
                {
                    if (item.money < 0.0 && item != null)
                    {
                        skyldere_.Add(item);
                    }
                }
                return skyldere_;  
            }
            private set
            {
                
                /*skyldere_ = new ObservableCollection<Person>();
                foreach (var item in value)
                {
                    if (item.money < 0.0)
                    {
                        skyldere_.Add(item);
                    }
                }*/
                NotifyPropertyChanged<ObservableCollection<Person>>(m => m.PersonsSkyldere);
                
            }
            
        }

        public void RemovePerson(Person p)
        {
            if (p != null)
            {
                Persons.Remove(p);
            }
        }

        public void AddNewPerson(Person p)
        {
            Persons.Add(p);
        }

        public void gemDenSorteBog()
        {
            Repository.WriteMeasurements(Persons_);
        }
    }
}
