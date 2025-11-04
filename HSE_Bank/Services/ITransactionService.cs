using System;
using System.Collections.Generic;
using HSE_Bank.Core;

namespace HSE_Bank.Services
{
    /// <summary>
    /// Интерфейс сервиса для работы с транзакциями.
    /// Принцип DIP (Dependency Inversion): зависимость от абстракции.
    /// </summary>
    public interface ITransactionService
    {
        void CreateTransaction(string type, int bankAccountId, decimal amount, string description, int categoryId);
        void DeleteTransaction(int transactionId);
        Transaction GetTransactionById(int id);
        List<Transaction> GetAllTransactions();
        List<Transaction> GetTransactionsByAccountId(int accountId);
        decimal CalculateBalance(int accountId);
        void Undo();
        void Redo();
    }
}
