using System;
using System.Collections.Generic;

namespace HSE_Bank.IO.Export
{
    /// <summary>
    /// Контекст для паттерна Strategy (GoF).
    /// Выбирает стратегию записи по расширению файла.
    /// Принцип Low Coupling: не привязана к конкретной реализации записи.
    /// </summary>
    public class FileWriter
    {
        private Dictionary<string, IDataWriter> _strategies;

        public FileWriter()
        {
            _strategies = new Dictionary<string, IDataWriter>()
            {
                { ".csv", new CsvWriter() },
                { ".json", new JsonWriter() },
                { ".yaml", new YamlWriter() }
            };
        }

        public void WriteFile<T>(string filePath, List<T> data) where T : class
        {
            string extension = Path.GetExtension(filePath).ToLower();

            if (!_strategies.ContainsKey(extension))
                throw new NotSupportedException($"Формат {extension} не поддерживается");

            IDataWriter strategy = _strategies[extension];
            strategy.Write(filePath, data);
        }
    }
}
