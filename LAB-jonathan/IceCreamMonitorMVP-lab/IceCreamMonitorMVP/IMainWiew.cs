using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IceCreamMonitorMVP.Model;

namespace IceCreamMonitorMVP
{
    public interface IMainWiew
    {
        void InitMeasurements(IList<string> stationIds);

        void ShowError(string errorMsg);

        void SetVariance(string variance);

        void SetTarget(string target);

        void SetStation(string stationId);

        void SetDate(DateTime date);

        void SetActual(string actual);

        void SetVarianceColor(System.Windows.Media.Color color);
    }
}
