using System;

namespace HSE_Bank.Core
{
    /// <summary>
    /// Паттерн Factory (GoF): инкапсулирует создание объектов с автоматической генерацией ID.
    /// Принцип GRASP Creator: фабрика создает объекты, которые она знает, как инициализировать.
    /// </summary>
    public class BankFactory
    {
        private int _accountIdCounter = 1;
        private int _categoryIdCounter = 1;
        private int _transactionIdCounter = 1;

        public Account CreateAccount(string name, decimal initialBalance = 0)
        {
            return new Account(_accountIdCounter++, name, initialBalance);
        }

        public TransactionCategory CreateCategory(string type, string name)
        {
            ValidateCategoryType(type);
            return new TransactionCategory(_categoryIdCounter++, type, name);
        }

        public Transaction CreateTransaction(string type, int bankAccountId, decimal amount,
                                            string description, int categoryId)
        {
            ValidateTransactionType(type);
            return new Transaction(
                _transactionIdCounter++,
                type,
                bankAccountId,
                amount,
                DateTime.Now,
                description,
                categoryId
            );
        }

        private void ValidateCategoryType(string type)
        {
            if (!type.Equals("Income", StringComparison.OrdinalIgnoreCase) &&
                !type.Equals("Expense", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Тип категории должен быть 'Income' или 'Expense'");
            }
        }

        private void ValidateTransactionType(string type)
        {
            if (!type.Equals("Income", StringComparison.OrdinalIgnoreCase) &&
                !type.Equals("Expense", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Тип транзакции должен быть 'Income' или 'Expense'");
            }
        }

        public void ResetCounters()
        {
            _accountIdCounter = 1;
            _categoryIdCounter = 1;
            _transactionIdCounter = 1;
        }
    }
}
