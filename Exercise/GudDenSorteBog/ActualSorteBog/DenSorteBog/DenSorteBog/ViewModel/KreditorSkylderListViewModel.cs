using System;
using System.Windows;
using System.Windows.Input;
using System.Threading;
using System.Collections.ObjectModel;

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

        // Default ctor
        public KreditorSkylderListViewModel() { }

        // TODO: ctor that accepts IXxxServiceAgent
        public KreditorSkylderListViewModel(ServiceAgent.ISorteBogServiceAgent serviceAgent)
        {
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

        

        public void funcTestSortBog()
        {
            var products = serviceAgent.funcTestSortBog();
            TestSortBog = new ObservableCollection<SorteBogModel>(products);
        }


        // TODO: Add methods that will be called by the view
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
        // TODO: Optionally add callback methods for async calls to the service agent
        
        // Helper method to notify View of an error
        private void NotifyError(string message, Exception error)
        {
            // Notify view of an error
            Notify(ErrorNotice, new NotificationEventArgs<Exception>(message, error));
        }
    }
}