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

        public string funcMoneyValue
        {
            get { return moneyValue.ToString(); }
            set
            {
                moneyValue = int.Parse(value);
                NotifyPropertyChanged(m => m.funcMoneyValue);
            }
        }
    }
}
