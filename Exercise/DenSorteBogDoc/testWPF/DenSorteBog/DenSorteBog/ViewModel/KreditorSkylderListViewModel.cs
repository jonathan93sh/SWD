using System;
using System.Windows;
using System.Threading;
using System.Collections.ObjectModel;

using DenSorteBog.Model;
using DenSorteBog.ServiceAgent;
using DenSorteBog.Locators;
using DenSorteBog;
// Toolkit namespace
using SimpleMvvmToolkit;

namespace DenSorteBog.ViewModel
{
    /// <summary>
    /// This class extends ViewModelDetailBase which implements IEditableDataObject.
    /// <para>
    /// Specify type being edited <strong>DetailType</strong> as the second type argument
    /// and as a parameter to the seccond ctor.
    /// </para>
    /// <para>
    /// Use the <strong>mvvmprop</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// </summary>
    public class KreditorSkylderListViewModel : ViewModelDetailBase<KreditorSkylderListViewModel, KreditorSkylderListModel>
    {
        // TODO: Add a member for IXxxServiceAgent
        private IKreditorSkylderListServiceAgent serviceAgent;

        // Default ctor
        public KreditorSkylderListViewModel() { }

        // TODO: Ctor to set base.Model to DetailType
        public KreditorSkylderListViewModel(KreditorSkylderListModel model)
        {
            base.Model = model;
        }

        // TODO: ctor that accepts IXxxServiceAgent
        public KreditorSkylderListViewModel(IKreditorSkylderListServiceAgent serviceAgent)
        {
            this.serviceAgent = serviceAgent;
           
        }

        // TODO: Add events to notify the view or obtain data from the view
        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;

        // TODO: Add properties using the mvvmprop code snippet

        // TODO: Add methods that will be called by the view
        private GaeldsposterView otherView = new GaeldsposterView();
        public void visGaeldsposter()
        {
            otherView.ShowDialog(); // blok skal i en tråd
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