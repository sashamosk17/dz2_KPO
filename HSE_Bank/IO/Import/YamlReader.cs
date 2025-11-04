using System;
using System.Collections.Generic;
using System.IO;
using HSE_Bank.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace HSE_Bank.IO.Import
{
    public class YamlReader : IDataReader
    {
        public T Read<T>(string filePath) where T : class
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Файл {filePath} не найден");

            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            string yaml = File.ReadAllText(filePath);
            return deserializer.Deserialize<T>(yaml);
        }
    }
}
