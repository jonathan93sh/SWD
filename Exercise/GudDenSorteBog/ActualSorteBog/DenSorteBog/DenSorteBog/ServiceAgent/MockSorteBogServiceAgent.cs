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
                SorteBog = DAL.Repository.ReadMeasurements();

                if(SorteBog == null)
                {
                    SorteBog = new ObservableCollection<SorteBogModel>();
                }
            }

            return SorteBog;
        }

        public void saveData()
        {
            DAL.Repository.WriteMeasurements(sorteBog);
        }


    }
}