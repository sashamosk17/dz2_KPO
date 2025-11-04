using System;
using System.Collections.Generic;

namespace HSE_Bank.IO.Export
{
    /// <summary>
    /// Паттерн Strategy (GoF): интерфейс для различных стратегий записи данных.
    /// </summary>
    public interface IDataWriter
    {
        void Write<T>(string filePath, List<T> data) where T : class;
    }
}
