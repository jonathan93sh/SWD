using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SimpleMvvmToolkit;


namespace DenSorteBog.Model
{
    public class MonitorModel : ModelBase<MonitorModel>
    {
        ObservableCollection<Person> persons_;

        public MonitorModel()
        {
            persons_ = new ObservableCollection<Person>();

            persons_.Add(new Person() { name = "Dr. Fuck", money = -18898.2 });
            persons_.Add(new Person() { name = "Dr. Who", money = 12.2 });
            persons_.Add(new Person() { name = "Dr. you", money = -324.2 });
            persons_.Add(new Person() { name = "Dr. HanSolo", money = 5.0 });
        }

        public ObservableCollection<Person> getPersons
        {
            get
            {
                return persons_;
            }
            set
            {
                persons_ = value;
                NotifyPropertyChanged<Person>(m => m.getPersons);
            }
        }

        


    }
}
