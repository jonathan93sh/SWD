using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenSorteBog
{
    public class MockSorteBogServiceAgent : ISorteBogServiceAgent
    {
        //laver en eller anden sortbog...
        public SorteBogModel CreateSorteBog()
        {
            return new SorteBogModel{};
        }
    }
}