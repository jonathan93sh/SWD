using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using DenSorteBog.Model;

namespace DenSorteBog.ServiceAgent
{
    class ServiceAgentFake : IServiceAgent
    {

        static ObservableCollection<Person> notNicePersons_ = null;
        ObservableCollection<Person> getPersonsDB()
        {
            if(notNicePersons_ == null)
            {
                notNicePersons_ = new ObservableCollection<Person>();

                notNicePersons_.Add(new Person() { name = "Dr. Fuck", money = -18898.2 });
                notNicePersons_.Add(new Person() { name = "Dr. Who", money = 12.2 });
                notNicePersons_.Add(new Person() { name = "Dr. you", money = -324.2 });
                notNicePersons_.Add(new Person() { name = "Dr. HanSolo", money = 5.0 });
            }

            return notNicePersons_;
        }


    }
}
