using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IceCreamMonitorMVP.Model;
using System.Windows.Media;

namespace IceCreamMonitorMVP
{
    public class MainPresenter
    {
        private IceCreamMonitor iceCreamMonitor;
        private IMainWiew view;

        public MainPresenter(IMainWiew view, IceCreamMonitor iceCreamMonitor)
        {
            this.view = view;
            this.iceCreamMonitor = iceCreamMonitor;
            this.iceCreamMonitor.newStationId += iceCreamMonitor_newStationId;
            
            InitializeView();
        }

        void iceCreamMonitor_newStationId(object sender, EventArgs e)
        {
            view.InitMeasurements(iceCreamMonitor.GetStationIds());
        }

        internal void InitializeView()
        {
            view.SetTarget(iceCreamMonitor.Target.ToString());
            view.InitMeasurements(iceCreamMonitor.GetStationIds());

        }

        public void ActualChanged(string text)
        {

        }

        public void DateChanged(string text, DateTime date)
        {

        }

        public void ChangeStation(string stationId)
        {
            view.SetStation(stationId);
        }

        public void NewMeasurement(string stationId, DateTime date, string acText)
        {
            VarianceRange range;
            iceCreamMonitor.AddMeasurement(stationId, date, int.Parse(acText));
            view.SetVariance(iceCreamMonitor.CalculateVariance(int.Parse(acText), out range).ToString());
            switch(range)
            {
                case VarianceRange.low:
                    view.SetVarianceColor(Color.FromRgb(255, 0, 0));
                    break;
                case VarianceRange.high:
                    view.SetVarianceColor(Color.FromRgb(0, 255, 0));
                    break;
                case VarianceRange.normal:
                default:
                    view.SetVarianceColor(Color.FromRgb(0, 0, 255));
                    break;

            }

            
        }

        public bool Closing()
        {
            iceCreamMonitor.SaveMeasurements();

            return false;
        }

        // TODO
        // Implement the missing parts of the presenter
    }
}
