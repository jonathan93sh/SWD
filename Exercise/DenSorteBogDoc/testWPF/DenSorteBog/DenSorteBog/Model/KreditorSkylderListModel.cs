using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SimpleMvvmToolkit;

namespace DenSorteBog.Model
{
    public class KreditorSkylderListModel : ModelBase<KreditorSkylderListModel>
    {
        private string _testString = "test"; 
        public string testString
        {
            get { return _testString; }
            set
            {
                _testString = value;
                NotifyPropertyChanged(m => m.testString);
            }
        }

    }
}
