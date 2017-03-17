using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenSorteBog
{
    public interface ISorteBogServiceAgent
    {
        SorteBogModel CreateSorteBog();
        List<SorteBogModel> funcTestSortBog();
    }
}
