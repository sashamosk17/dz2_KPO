using System;
using System.Collections.Generic;
using System.Linq;
using HSE_Bank.Core;
using HSE_Bank.Operations;
using HSE_Bank.Operations.TransactionOperations;

namespace HSE_Bank.Services
{
    /// <summary>
    /// Реализация сервиса для управления транзакциями.
    /// Паттерн Facade (GoF): предоставляет унифицированный интерфейс для работы с транзакциями.
    /// </summary>
    public class TransactionService : ITransactionService
    {
        private DataRepository _repository;
        private BankFactory _factory;
        private OperationManager _operationManager;
        private IAccountService _accountService;
        private ICategoryService _categoryService;

        public TransactionService(DataRepository repository, BankFactory factory, OperationManager operationManager,
                                 IAccountService accountService, ICategoryService categoryService)
        {
            _repository = repository;
            _factory = factory;
            _operationManager = operationManager;
            _accountService = accountService;
            _categoryService = categoryService;
        }

        public void CreateTransaction(string type, int bankAccountId, decimal amount,
                                     string description, int categoryId)
        {
            Account account = _accountService.GetAccountById(bankAccountId);
            if (account == null)
                throw new InvalidOperationException($"Счет с ID {bankAccountId} не найден");

            if (type.Equals("Expense", StringComparison.OrdinalIgnoreCase) && account.Balance < amount)
                throw new InvalidOperationException($"Недостаточно средств. На счете: {account.Balance:C}, требуется: {amount:C}");

            TransactionCategory category = _categoryService.GetCategoryById(categoryId);
            if (category == null)
                throw new InvalidOperationException($"Категория с ID {categoryId} не найдена");

            if (!category.Type.Equals(type, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException($"Тип '{type}' не соответствует типу категории '{category.Type}'");

            var operation = new CreateTransactionOperation(_factory, _repository.Transactions, type, bankAccountId,
                                                          amount, description, categoryId, account);
            _operationManager.ExecuteOperation(operation);
        }

        public void DeleteTransaction(int transactionId)
        {
            Transaction transaction = GetTransactionById(transactionId);
            if (transaction != null)
            {
                var operation = new DeleteTransactionOperation(_repository.Transactions, transaction);
                _operationManager.ExecuteOperation(operation);
            }
        }

        public Transaction GetTransactionById(int id)
        {
            return _repository.Transactions.FirstOrDefault(t => t.Id == id);
        }

        public List<Transaction> GetAllTransactions()
        {
            return _repository.Transactions;
        }

        public List<Transaction> GetTransactionsByAccountId(int accountId)
        {
            return _repository.Transactions.Where(t => t.BankAccountId == accountId).ToList();
        }

        public decimal CalculateBalance(int accountId)
        {
            Account account = _accountService.GetAccountById(accountId);
            if (account == null)
                return 0;
            return account.Balance;
        }


        public void Undo()
        {
            _operationManager.Undo();
        }

        public void Redo()
        {
            _operationManager.Redo();
        }
    }
}
