using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using HSE_Bank.Core;

namespace HSE_Bank.IO.Export
{
    public class CsvWriter : IDataWriter
    {
        public void Write<T>(string filePath, List<T> data) where T : class
        {
            if (data.Count == 0)
            {
                File.WriteAllText(filePath, "");
                return;
            }

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                PropertyInfo[] properties = typeof(T).GetProperties();
                string header = string.Join(",", Array.ConvertAll(properties, p => p.Name));
                writer.WriteLine(header);

                foreach (var item in data)
                {
                    var values = new List<string>();
                    foreach (PropertyInfo prop in properties)
                    {
                        values.Add(prop.GetValue(item)?.ToString() ?? "");
                    }
                    writer.WriteLine(string.Join(",", values));
                }
            }
        }
    }
}
