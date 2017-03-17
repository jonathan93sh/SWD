using System;
using System.Collections.Generic;
using System.Linq;

using SimpleMvvmToolkit;

namespace DenSorteBog
{
    public class SorteBogModel : ModelBase<SorteBogModel>
    {
        public SorteBogModel()
        { }
        public SorteBogModel(SorteBogModel other)
        {
            PersonName = other.PersonName;
            MoneyValue = other.MoneyValue;
        }

        private string personName;

        public string PersonName
        {
            get { return personName; }
            set
            {
                personName = value;
                NotifyPropertyChanged(m => m.PersonName);
            }
        }

        private int moneyValue;

        public string MoneyValue
        {
            get { return moneyValue.ToString(); }
            set
            {
                moneyValue = int.Parse(value);
                NotifyPropertyChanged(m => m.MoneyValue);
            }
        }
    }
}
