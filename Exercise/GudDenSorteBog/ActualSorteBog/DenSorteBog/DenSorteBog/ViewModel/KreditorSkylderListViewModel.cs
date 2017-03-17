using System;
using System.Windows;
using System.Windows.Input;
using System.Threading;
using System.Collections.ObjectModel;
using System.Collections.Generic;

// Toolkit namespace
using SimpleMvvmToolkit;

// Toolkit extension methods
using SimpleMvvmToolkit.ModelExtensions;

namespace DenSorteBog.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// Use the <strong>mvvmprop</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// </summary>
    public class KreditorSkylderListViewModel : ViewModelDetailBase<KreditorSkylderListViewModel, SorteBogModel>
    {
        
        // TODO: Add a member for IXxxServiceAgent
        private ServiceAgent.ISorteBogServiceAgent serviceAgent;

        SorteBogModel default_ = new SorteBogModel() { MoneyValue = "0", PersonName = "" };

        // Default ctor
        public KreditorSkylderListViewModel() {
            base.Model = default_;
            
        }

        // TODO: ctor that accepts IXxxServiceAgent
        public KreditorSkylderListViewModel(ServiceAgent.ISorteBogServiceAgent serviceAgent)
        {
            base.Model = default_;
            this.serviceAgent = serviceAgent;
        }

        // TODO: Add events to notify the view or obtain data from the view
        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;


        // TODO: Add properties using the mvvmprop code snippet
        private ObservableCollection<SorteBogModel> sorteBog;
        public ObservableCollection<SorteBogModel> TestSortBog
        {
            get { return sorteBog; }
            set
            {
                sorteBog = value;
                NotifyPropertyChanged(vm => vm.TestSortBog);
            }
        }


        private SorteBogModel _SelectedPerson;
        public SorteBogModel SelectedPerson
        {
            get { return _SelectedPerson; }
            set
            {
                base.Model = value;
                _SelectedPerson = value;
                NotifyPropertyChanged(m => m.SelectedPerson);
            }
        }


        private bool _KreditorChecked;
        public bool KreditorChecked
        {
            get { return _KreditorChecked; }
            set
            {
                _KreditorChecked = value;

                if (KreditorChecked == SkylderChecked)
                    SkylderChecked = !KreditorChecked;

                NotifyPropertyChanged(m => m.KreditorChecked);
            }
        }

        private bool _SkylderChecked;
        public bool SkylderChecked
        {
            get { return _SkylderChecked; }
            set
            {
                _SkylderChecked = value;
                if (KreditorChecked == SkylderChecked)
                    KreditorChecked = !SkylderChecked;
                
                NotifyPropertyChanged(m => m.SkylderChecked);
            }
        }





        // TODO: Add methods that will be called by the view
        

        public void funcTestSortBog()
        {
            //var products = serviceAgent.funcTestSortBog();
            TestSortBog = serviceAgent.funcTestSortBog();
            KreditorChecked = true;
        }

        public void funcRemoveSelectedPerson()
        {
            if (SelectedPerson != null)
            {
                base.Model = default_;
                TestSortBog.Remove(SelectedPerson);
            }
        }

        public void AddNewPerson()
        {
            TestSortBog.Add(new SorteBogModel(this.Model));
        }

        public void windowClosed()
        {
            serviceAgent.saveData();
        }

        public void testGaelViewModel()
        {
            MessageBus.Default.Notify(MessageTokens.Navigation, this, new NotificationEventArgs(PageNames.Home));

            SendMessage(MessageTokens.Navigation, new NotificationEventArgs<string>(MessageTokens.Navigation, "MyMessage"));
        }

        // TODO: Optionally add callback methods for async calls to the service agent
        
        // Helper method to notify View of an error
        private void NotifyError(string message, Exception error)
        {
            // Notify view of an error
            Notify(ErrorNotice, new NotificationEventArgs<Exception>(message, error));
        }
    }
}