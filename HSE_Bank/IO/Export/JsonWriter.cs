using System;
using System.Collections.Generic;
using System.IO;
using HSE_Bank.Core;
using Newtonsoft.Json;

namespace HSE_Bank.IO.Export
{
    public class JsonWriter : IDataWriter
    {
        public void Write<T>(string filePath, List<T> data) where T : class
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
}
