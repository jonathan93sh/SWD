using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HomeMade.Observer;

namespace DenSorteBog.ServiceAgent
{
    public interface ISorteBogServiceAgent
    {

        SorteBogModel CreateSorteBog();
        List<SorteBogModel> funcTestSortBog();
    }
}
