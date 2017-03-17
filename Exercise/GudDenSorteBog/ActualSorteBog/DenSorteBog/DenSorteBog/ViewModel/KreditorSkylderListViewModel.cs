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

using DenSorteBog.Model;

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
        //private ServiceAgent.ISorteBogServiceAgent serviceAgent;
        private Model.Person default_ = new Model.Person() { money = 0.0, name = "" };

        public Model.Person onPagePerson { get { return default_; } set { default_ = value; NotifyPropertyChanged<Person>(mv => mv.onPagePerson); } }

        public string onPagePersonName { get { return onPagePerson.name; } set { onPagePerson.name = value; NotifyPropertyChanged<String>(mv => mv.onPagePersonName); } }

        public string onPagePersonMoney { get { return onPagePerson.money.ToString() ; } set { onPagePerson.money = double.Parse(value); NotifyPropertyChanged<String>(mv => mv.onPagePersonMoney); } }


        // Default ctor
        public KreditorSkylderListViewModel() {
            
        }


        public KreditorSkylderListViewModel(SorteBogModel model)
        {
            base.Model = model;
        }


        // TODO: ctor that accepts IXxxServiceAgent
        /*public KreditorSkylderListViewModel(ServiceAgent.ISorteBogServiceAgent serviceAgent)
        {
            base.Model = default_;
            this.serviceAgent = serviceAgent;
        }*/

        // TODO: Add events to notify the view or obtain data from the view
        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;


        // TODO: Add properties using the mvvmprop code snippet

        private Person _SelectedPerson;
        public Person SelectedPerson
        {
            get { return _SelectedPerson; }
            set
            {
                if (value == null)
                    return;

                _SelectedPerson = value;
                NotifyPropertyChanged(m => m.SelectedPerson);
            }
        }





        // TODO: Add methods that will be called by the view

        public void funcRemoveSelectedPerson()
        {
            if (SelectedPerson != null)
            {
                Model.RemovePerson(SelectedPerson);
            }
        }

        public void AddNewPerson()
        {
            if (onPagePerson == null)
                return;

            Model.AddNewPerson(new Person(onPagePerson));
        }

       /* private void startChildView()
        {
            Window childView = new GaeldsposterView();

            childView.Show();

            System.Windows.Threading.Dispatcher.Run();

        }*/
        Window childView = new GaeldsposterView();
        public void windowLoaded() 
        {

            /*Thread otherWindow = new Thread(new ThreadStart(startChildView));

            otherWindow.SetApartmentState(ApartmentState.STA);

            otherWindow.IsBackground = true;

            otherWindow.Start();*/
            

            childView.Show();
        }

        public void windowClosed()
        {
            Model.gemDenSorteBog();
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