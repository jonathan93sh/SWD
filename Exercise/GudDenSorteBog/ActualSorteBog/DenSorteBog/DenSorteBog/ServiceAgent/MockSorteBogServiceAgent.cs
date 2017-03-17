using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenSorteBog.ServiceAgent
{
    public class MockSorteBogServiceAgent : ISorteBogServiceAgent
    {
        //laver en eller anden sortbog...
        public SorteBogModel CreateSorteBog()
        {
            return new SorteBogModel{};
        }

        public List<SorteBogModel> funcTestSortBog()
        {
            return new List<SorteBogModel>
            {
                new SorteBogModel { funcMoneyValue = -2546, funcPersonName = "Jens Jensen" },
                new SorteBogModel { funcMoneyValue = 2842, funcPersonName = "Bent Bentsen" },
                new SorteBogModel { funcMoneyValue = 31235, funcPersonName = "Lars Larsen" },
                new SorteBogModel { funcMoneyValue = -20000, funcPersonName = "Indiana Jones" },
                new SorteBogModel { funcMoneyValue = 510283, funcPersonName = "Pablo Escobar" }
            };
        }
    }
}