using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeMade.Observer;

namespace DenSorteBog.ServiceAgent
{
    public interface ISorteBogServiceAgent
    {

        SorteBogModel CreateSorteBog();
        ObservableCollection<SorteBogModel> funcTestSortBog();



        //List<SorteBogModel> funcRemoveSelectedPerson();
    }
}
