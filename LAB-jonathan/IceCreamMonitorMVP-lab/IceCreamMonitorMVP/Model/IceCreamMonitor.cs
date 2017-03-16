using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCreamMonitorMVP.Model
{
    public enum VarianceRange
    {
        low, normal, high
    }

    public class IceCreamMonitor
    {
        public event EventHandler newStationId;
        ObservableCollection<Measurement> measurements;
        SortedSet<string> stationIds;

        public IceCreamMonitor()
        {
            stationIds = new SortedSet<string>();
            measurements = DAL.Repository.ReadMeasurements();
            if (measurements == null)
            {
                measurements = new ObservableCollection<Measurement>();
            }
            else
            {
                foreach (var meas in measurements)
                    stationIds.Add(meas.StationId);
            }
            Target = Properties.Settings.Default.Target;
        }

        internal Measurement GetMeasurement(string stationId, DateTime date, out bool found)
        {
            var meas = measurements.Where(measurement => measurement.StationId == stationId && measurement.Date == date)
                .OrderBy(s => s.Date)
                .Select(s => s);
            if (meas.Count() > 0)
            {
                found = true;
                return meas.First();
            }
            else
            {
                found = false;
                return new Measurement("", DateTime.MinValue, -1);
            }
        }

        internal IList<string> GetStationIds()
        {
            return stationIds.ToList();
        }

        internal IList<Measurement> GetMeasurements()
        {
            return measurements;
        }

        internal void AddMeasurement(string stationId, DateTime date, int actual)
        {
            measurements.Add(new Measurement(stationId, date, actual));
            bool ins = stationIds.Add(stationId);
            if (ins)
                if (newStationId != null)
                    newStationId(this, new EventArgs());
        }

        internal void SaveMeasurements()
        {
            DAL.Repository.WriteMeasurements(measurements);
        }

        internal int CalculateVariance(int actualVal, out VarianceRange range)
        {
            int variance = actualVal - Target;
            range = VarianceRange.normal;

            if (actualVal < Target * 0.9)
                range = VarianceRange.low;
            else if (actualVal > Target * 1.05)
                range = VarianceRange.high;

            return variance;
        }

        public int Target { get; set; }

        internal bool ValidateDate(DateTime date, out string errorMsg)
        {
            errorMsg = "";
            bool noError = true;

            if (date < new DateTime(2013, 1, 1))
            {
                noError = false;
                errorMsg = "Date must not be earlier then January 1st 2013!";
            }
            else if (date > DateTime.Now)
            {
                noError = false;
                errorMsg = "Date cannot be a date in the future!";
            }

            return noError;
        }

        internal bool ValidateMeasurementValue(int actualVal, out string errorMsg)
        {
            errorMsg = "";
            bool noError = true;

            if (actualVal < 0)
            {
                noError = false;
                errorMsg = "The actual value cannot be less than 0!";
            }
            else if (actualVal > 1000)
            {
                noError = false;
                errorMsg = "The actual value cannot be greater than 1000!";
            }
            return noError;
        }
    }
}
