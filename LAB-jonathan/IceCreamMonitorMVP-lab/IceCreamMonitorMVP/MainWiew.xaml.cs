using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using IceCreamMonitorMVP.Model;

namespace IceCreamMonitorMVP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWiew : Window, IMainWiew
    {
        MainPresenter presenter;

        #region Constuction
        public MainWiew()
        {
            InitializeComponent();
            presenter = new MainPresenter(this, new Model.IceCreamMonitor());
        }

        #endregion

        #region interfaceImplementering
        public void InitMeasurements(IList<string> measurements)
        {
            lbxStations.ItemsSource = measurements;
        }

        public void ShowError(string errorMsg)
        {
            MessageBox.Show(errorMsg, "Data not valid");
        }

        #endregion

        #region eventhandlers
        private void lbxStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string stationId = lbxStations.SelectedValue as string;
            presenter.ChangeStation(stationId);
        }

        private void btnNewMeasurement_Click(object sender, RoutedEventArgs e)
        {
            if (!dpDate.SelectedDate.HasValue)
                ShowError("You must enter a date for the measurement");
            else
                presenter.NewMeasurement(tbxStationId.Text, dpDate.SelectedDate.Value, tbxActual.Text);
        }

       



        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = presenter.Closing();
        }

        #endregion

        public void SetVariance(string variance)
        {
            tbxVariance.Text = variance;
        }


        public void SetTarget(string target)
        {
            tbxTarget.Text = target;
        }


        public void SetStation(string stationId)
        {
            tbxStationId.Text = stationId;
        }


        public void SetDate(DateTime date)
        {
            dpDate.SelectedDate = date;
        }


        public void SetActual(string actual)
        {
            tbxActual.Text = actual;
        }


        public void SetVarianceColor(Color color)
        {
            tbxVariance.Foreground = new SolidColorBrush(color);
        }

        private void tbxActual_LostFocus(object sender, RoutedEventArgs e)
        {
            if (presenter != null)
                presenter.ActualChanged(tbxActual.Text);
        }

        private void dpDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (presenter != null)
                presenter.DateChanged(tbxStationId.Text, dpDate.SelectedDate.Value);
        }
    }
}
