using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using DenSorteBog.Model;

namespace DenSorteBog.ServiceAgent
{
    interface IServiceAgent
    {
        ObservableCollection<Person> getPersonsDB();
    }
}
