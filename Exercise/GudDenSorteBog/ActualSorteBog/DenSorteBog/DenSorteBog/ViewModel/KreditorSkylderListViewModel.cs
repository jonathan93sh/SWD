using System;
using System.Windows;
using System.Windows.Input;
using System.Threading;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
// Toolkit namespace
using SimpleMvvmToolkit;

// Toolkit extension methods
using SimpleMvvmToolkit.ModelExtensions;

using DenSorteBog.Model;

using MvvmFoundation.Wpf;

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

        private Model.Person default_ = new Model.Person() { money = 0.0, name = "" };

        public Model.Person onPagePerson { 
            get {

                return default_; 
            } 
            set { 
                default_ = value; 
                NotifyPropertyChanged<Person>(mv => mv.onPagePerson); 
            } 
        }

        public string onPagePersonName { get { return onPagePerson.name; } set { onPagePerson.name = value; NotifyPropertyChanged<String>(mv => mv.onPagePersonName); } }

        public string onPagePersonMoney { get { return onPagePerson.money.ToString() ; } 
            set { 
                double tmp;
                if(double.TryParse(value, out tmp))
                {
                    onPagePerson.money = tmp;
                }
                else
                {
                    onPagePerson.money = 0.0;
                }
                NotifyPropertyChanged<String>(mv => mv.onPagePersonMoney); 
            } 
        }


        // Default ctor
        private KreditorSkylderListViewModel() {
            
        }


        public KreditorSkylderListViewModel(SorteBogModel model)
        {
            base.Model = model;
        }
        // TODO: Add events to notify the view or obtain data from the view
        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;


        // TODO: Add properties using the mvvmprop code snippet

        private Person _SelectedPerson;
        public Person SelectedPerson
        {
            get { return _SelectedPerson; }
            set
            {

                if(value != null)
                {
                    onPagePersonName = value.name;
                    onPagePersonMoney = value.money.ToString();
                }
                _SelectedPerson = value;

                NotifyPropertyChanged(mv => mv.SelectedPerson);
            }
        }





        // TODO: Add methods that will be called by the view
        ICommand _RemoveSelectedPersonCommand;
        public ICommand RemoveSelectedPersonCommand
        {
            get { return _RemoveSelectedPersonCommand ?? (_RemoveSelectedPersonCommand = new RelayCommand(RemoveSelectedPerson, RemoveSelectedPersonCanExecute)); }
        }


        void RemoveSelectedPerson()
        {
            if (SelectedPerson != null)
            {
                Model.RemovePerson(SelectedPerson);
                SelectedPerson = null;
            }
        }

        bool RemoveSelectedPersonCanExecute()
        {
            if(SelectedPerson != null)
                return true;

            return false;

        }


        ICommand _AddNewPersonCommand;
        public ICommand AddNewPersonCommand
        {
            get { return _AddNewPersonCommand ?? (_AddNewPersonCommand = new RelayCommand(AddNewPerson, AddNewPersonCanExecute)); }
        }

        void AddNewPerson()
        {
            if (onPagePerson == null)
                return;

            Model.AddNewPerson(new Person(onPagePerson));
            
        }

        bool AddNewPersonCanExecute()
        {
            if (onPagePerson == null || onPagePerson.money == 0.0 || onPagePersonMoney == "" || onPagePersonName == "" || Model.nameExist(onPagePersonName))
                return false;


            return true;
        }



        ICommand _ChangePersonCommand;
        public ICommand ChangePersonCommand
        {
            get { return _ChangePersonCommand ?? (_ChangePersonCommand = new RelayCommand(ChangePerson, ChangePersonCanExecute)); }
        }

        void ChangePerson()
        {
            if (SelectedPerson == null)
                return;

            Model.ChangePerson(SelectedPerson, onPagePerson.name, onPagePerson.money);
            onPagePersonName = "";
            onPagePersonMoney = "0.0";
        }

        bool ChangePersonCanExecute()
        {
            if (SelectedPerson == null || onPagePerson.money == 0.0 || onPagePersonMoney == "" || onPagePersonName == "")
                return false;


            return true;
        }


        GaeldsposterView childView = new GaeldsposterView();
        public void windowLoaded() 
        {
            childView.Show();
        }

        public void windowClosed()
        {
            Model.gemDenSorteBog();
            childView.AreClosing = true;
            childView.Close();
        }

        // Helper method to notify View of an error
        private void NotifyError(string message, Exception error)
        {
            // Notify view of an error
            Notify(ErrorNotice, new NotificationEventArgs<Exception>(message, error));
        }
    }
}