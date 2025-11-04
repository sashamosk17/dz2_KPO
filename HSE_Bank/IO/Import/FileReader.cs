using System;

namespace HSE_Bank.IO.Import
{
    /// <summary>
    /// Контекст для паттерна Strategy (GoF).
    /// Выбирает нужную стратегию чтения в зависимости от типа файла.
    /// Принцип Low Coupling: слабая связь между файлом и форматом.
    /// </summary>
    public class FileReader
    {
        private Dictionary<string, IDataReader> _strategies;

        public FileReader()
        {
            _strategies = new Dictionary<string, IDataReader>()
            {
                { ".csv", new CsvReader() },
                { ".json", new JsonReader() },
                { ".yaml", new YamlReader() }
            };
        }

        public T ReadFile<T>(string filePath) where T : class
        {
            string extension = Path.GetExtension(filePath).ToLower();

            if (!_strategies.ContainsKey(extension))
                throw new NotSupportedException($"Формат {extension} не поддерживается");

            IDataReader strategy = _strategies[extension];
            return strategy.Read<T>(filePath);
        }
    }
}
