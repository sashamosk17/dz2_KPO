using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HSE_Bank.Core;

namespace HSE_Bank.IO.Import
{
    public class CsvReader : IDataReader
    {
        public T Read<T>(string filePath) where T : class
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Файл {filePath} не найден");

            var lines = File.ReadAllLines(filePath);
            if (lines.Length == 0)
                return null;

            var result = new List<object>();

            for (int i = 1; i < lines.Length; i++)
            {
                var values = lines[i].Split(',');
                if (typeof(T) == typeof(Account))
                {
                    result.Add(new Account(int.Parse(values[0]), values[1], decimal.Parse(values[2])));
                }
                else if (typeof(T) == typeof(TransactionCategory))
                {
                    result.Add(new TransactionCategory(int.Parse(values[0]), values[1], values[2]));
                }
                else if (typeof(T) == typeof(Transaction))
                {
                    result.Add(new Transaction(
                        int.Parse(values[0]),
                        values[1],
                        int.Parse(values[2]),
                        decimal.Parse(values[3]),
                        DateTime.Parse(values[4]),
                        values[5],
                        int.Parse(values[6])
                    ));
                }
            }

            return result as T;
        }
    }
}
