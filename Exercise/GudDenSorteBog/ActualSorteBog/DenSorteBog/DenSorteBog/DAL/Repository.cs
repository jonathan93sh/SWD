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
        public static void WritePersons(IList<Person> Persons)
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(@"Persons.json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, Persons);
            }
        }

        internal static ObservableCollection<Person> ReadPersons()
        {
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(@"Persons.json"))
                {

                    json = sr.ReadToEnd();
                }
            }
            catch (FileNotFoundException)
            {
                return new ObservableCollection<Person>();
            }

            return JsonConvert.DeserializeObject<ObservableCollection<Person>>(json);
        }
    }
}
