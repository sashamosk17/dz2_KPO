using System;
using System.Collections.Generic;

namespace HSE_Bank.IO.Import
{
    /// <summary>
    /// Паттерн Strategy (GoF): интерфейс для различных стратегий чтения данных.
    /// </summary>
    public interface IDataReader
    {
        T Read<T>(string filePath) where T : class;
    }
}
