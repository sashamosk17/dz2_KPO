using System;
using System.Collections.Generic;
using System.IO;
using HSE_Bank.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace HSE_Bank.IO.Export
{
    public class YamlWriter : IDataWriter
    {
        public void Write<T>(string filePath, List<T> data) where T : class
        {
            var serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            string yaml = serializer.Serialize(data);
            File.WriteAllText(filePath, yaml);
        }
    }
}
