using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DenSorteBog.Model;
using Newtonsoft.Json;
using System.IO;
using System.Windows;

namespace DenSorteBog.DAL
{
    public static class Repository
    {
        public static void WriteMeasurements(IList<Person> measurements)
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(@"Measurements.json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, measurements);
            }
        }

        internal static ObservableCollection<Person> ReadMeasurements()
        {
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(@"Measurements.json"))
                {

                    json = sr.ReadToEnd();
                }
            }
            catch (FileNotFoundException)
            {
            }

            return JsonConvert.DeserializeObject<ObservableCollection<Person>>(json);
        }
    }
}
