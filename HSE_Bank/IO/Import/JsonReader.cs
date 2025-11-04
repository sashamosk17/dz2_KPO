using System;
using System.Collections.Generic;
using System.IO;
using HSE_Bank.Core;
using Newtonsoft.Json;

namespace HSE_Bank.IO.Import
{
    public class JsonReader : IDataReader
    {
        public T Read<T>(string filePath) where T : class
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Файл {filePath} не найден");

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
