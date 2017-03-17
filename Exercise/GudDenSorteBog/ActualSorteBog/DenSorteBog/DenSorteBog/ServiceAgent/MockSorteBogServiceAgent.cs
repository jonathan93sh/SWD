using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HomeMade.Observer;
using DenSorteBog.Model;

// Toolkit namespace
using SimpleMvvmToolkit;

// Toolkit extension methods
using SimpleMvvmToolkit.ModelExtensions;

namespace DenSorteBog.ServiceAgent
{
    public class MockSorteBogServiceAgent : ISorteBogServiceAgent
    {
        //laver en eller anden sortbog...

        private static ObservableCollection<SorteBogModel> sorteBog = null;

        public ObservableCollection<SorteBogModel> SorteBog { get { return sorteBog; } set { sorteBog = value; } }

        public SorteBogModel CreateSorteBog()
        {
            return new SorteBogModel{};
        }

        public ObservableCollection<SorteBogModel> funcTestSortBog()
        {
            
            if (sorteBog == null)
            {
                sorteBog = new ObservableCollection<SorteBogModel>(new List<SorteBogModel>
            {
                new SorteBogModel { MoneyValue = "-2546", PersonName = "Jens Jensen" },
                new SorteBogModel { MoneyValue = "2842", PersonName = "Bent Bentsen" },
                new SorteBogModel { MoneyValue = "31235", PersonName = "Lars Larsen" },
                new SorteBogModel { MoneyValue = "-20000", PersonName = "Indiana Jones" },
                new SorteBogModel { MoneyValue = "510283", PersonName = "Pablo Escobar" }
            });
            }

            return sorteBog;


        }

    }
}