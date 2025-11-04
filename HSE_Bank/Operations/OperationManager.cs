using System;
using System.Collections.Generic;

namespace HSE_Bank.Operations
{
    /// <summary>
    /// Менеджер операций для реализации Undo/Redo функциональности.
    /// Использует паттерн Command для инкапсуляции операций.
    /// </summary>
    public class OperationManager
    {
        private Stack<IOperation> _undoStack = new Stack<IOperation>();
        private Stack<IOperation> _redoStack = new Stack<IOperation>();

        public void ExecuteOperation(IOperation operation)
        {
            operation.Execute();
            _undoStack.Push(operation);
            _redoStack.Clear();
        }

        public void Undo()
        {
            if (_undoStack.Count > 0)
            {
                var operation = _undoStack.Pop();
                operation.Undo();
                _redoStack.Push(operation);
            }
        }

        public void Redo()
        {
            if (_redoStack.Count > 0)
            {
                var operation = _redoStack.Pop();
                operation.Execute();
                _undoStack.Push(operation);
            }
        }

        public int GetUndoCount()
        {
            return _undoStack.Count;
        }

        public int GetRedoCount()
        {
            return _redoStack.Count;
        }

        public bool CanUndo()
        {
            return _undoStack.Count > 0;
        }

        public bool CanRedo()
        {
            return _redoStack.Count > 0;
        }
    }
}
