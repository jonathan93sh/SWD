using System;
using System.Collections.Generic;
using System.Linq;

using SimpleMvvmToolkit;

namespace DenSorteBog
{
    public class SorteBogModel : ModelBase<SorteBogModel>
    {

        private string personName;

        public string funcPersonName
        {
            get { return personName; }
            set
            {
                personName = value;
                NotifyPropertyChanged(m => m.funcPersonName);
            }
        }

        private int moneyValue;

        public int funcMoneyValue
        {
            get { return moneyValue; }
            set
            {
                moneyValue = value;
                NotifyPropertyChanged(m => m.funcMoneyValue);
            }
        }
    }
}
