using System;
using System.Diagnostics;

namespace HSE_Bank.Operations.Decorators
{
    /// <summary>
    /// Паттерн Decorator: оборачивает операцию и измеряет время ее выполнения.
    /// Структурный паттерн, позволяет добавить ответственность к объекту динамически.
    /// </summary>
    public class TimeMeasuringOperationDecorator : IOperation
    {
        private readonly IOperation _operation;
        private readonly string _operationName;

        public TimeMeasuringOperationDecorator(IOperation operation, string operationName)
        {
            _operation = operation;
            _operationName = operationName;
        }

        public void Execute()
        {
            var stopwatch = Stopwatch.StartNew();
            _operation.Execute();
            stopwatch.Stop();
            Console.WriteLine($"[{_operationName}] Время выполнения: {stopwatch.ElapsedMilliseconds}ms");
        }

        public void Undo()
        {
            var stopwatch = Stopwatch.StartNew();
            _operation.Undo();
            stopwatch.Stop();
            Console.WriteLine($"[{_operationName}] Время отмены: {stopwatch.ElapsedMilliseconds}ms");
        }
    }
}
