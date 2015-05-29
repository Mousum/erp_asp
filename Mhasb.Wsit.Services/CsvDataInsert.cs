using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using Mhasb.Domain.Commons;

namespace Mhasb.Services
{
    public static class CsvDataInsert<T>
    {
        public static List<T> DataList(string resourceName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    CsvReader csvReader = new CsvReader(reader);
                    csvReader.Configuration.WillThrowOnMissingField = false;
                    var countries = csvReader.GetRecords<T>().ToList();
                    return countries;
                }
            }
        }
    }
}
