using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCreamMonitorMVP.Model
{
    public class Measurement
    {
        public Measurement(string stationId, DateTime date, int actual)
        {
            this.stationId = stationId;
            this.date = date;
            this.actual = actual;
        }

        private string stationId;

        public string StationId
        {
            get { return stationId; }
            set { stationId = value; }
        }

        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        private int actual;

        public int Actual
        {
            get { return actual; }
            set { actual = value; }
        }
        
    }
}
