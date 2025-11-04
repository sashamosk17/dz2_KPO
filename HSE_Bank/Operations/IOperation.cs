using System;

namespace HSE_Bank.Operations
{
    /// <summary>
    /// Паттерн Command (GoF): интерфейс для всех команд, обеспечивает инкапсуляцию запроса как объекта.
    /// </summary>
    public interface IOperation
    {
        void Execute();
        void Undo();
    }
}
