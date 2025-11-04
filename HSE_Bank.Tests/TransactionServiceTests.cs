using Xunit;
using HSE_Bank.Core;
using HSE_Bank.Operations;
using HSE_Bank.Services;

namespace HSE_Bank.Tests
{
    /// <summary>
    /// Unit тесты для TransactionService.
    /// Тестируют создание, удаление, валидацию транзакций.
    /// </summary>
    public class TransactionServiceTests
    {
        private DataRepository CreateRepositoryWithSampleData()
        {
            var repository = new DataRepository();
            repository.Accounts.Add(new Account(1, "Основной", 10000));
            repository.Categories.Add(new TransactionCategory(1, "Income", "Зарплата"));
            repository.Categories.Add(new TransactionCategory(2, "Expense", "Кафе"));
            return repository;
        }

        [Fact]
        public void CreateTransaction_WithValidData_ShouldAddTransactionToRepository()
        {
            var repository = CreateRepositoryWithSampleData();
            var factory = new BankFactory();
            var operationManager = new OperationManager();
            var accountService = new AccountService(repository, factory, operationManager);
            var categoryService = new CategoryService(repository, factory, operationManager);
            var transactionService = new TransactionService(repository, factory, operationManager,
                                                           accountService, categoryService);

            transactionService.CreateTransaction("Income", 1, 5000, "Зарплата", 1);

            Assert.Single(repository.Transactions);
            Assert.Equal("Income", repository.Transactions[0].Type);
            Assert.Equal(5000, repository.Transactions[0].Amount);
        }

        [Fact]
        public void CreateTransaction_WithNonexistentAccount_ShouldThrowException()
        {
            var repository = CreateRepositoryWithSampleData();
            var factory = new BankFactory();
            var operationManager = new OperationManager();
            var accountService = new AccountService(repository, factory, operationManager);
            var categoryService = new CategoryService(repository, factory, operationManager);
            var transactionService = new TransactionService(repository, factory, operationManager,
                                                           accountService, categoryService);

            Assert.Throws<InvalidOperationException>(() =>
                transactionService.CreateTransaction("Income", 999, 1000, "Описание", 1));
        }

        [Fact]
        public void CreateTransaction_WithNonexistentCategory_ShouldThrowException()
        {
            var repository = CreateRepositoryWithSampleData();
            var factory = new BankFactory();
            var operationManager = new OperationManager();
            var accountService = new AccountService(repository, factory, operationManager);
            var categoryService = new CategoryService(repository, factory, operationManager);
            var transactionService = new TransactionService(repository, factory, operationManager,
                                                           accountService, categoryService);

            Assert.Throws<InvalidOperationException>(() =>
                transactionService.CreateTransaction("Income", 1, 1000, "Описание", 999));
        }

        [Fact]
        public void CreateTransaction_WithInsufficientFunds_ShouldThrowException()
        {
            var repository = CreateRepositoryWithSampleData();
            var factory = new BankFactory();
            var operationManager = new OperationManager();
            var accountService = new AccountService(repository, factory, operationManager);
            var categoryService = new CategoryService(repository, factory, operationManager);
            var transactionService = new TransactionService(repository, factory, operationManager,
                                                           accountService, categoryService);

            // Счет имеет баланс 10000, пытаемся потратить 50000
            Assert.Throws<InvalidOperationException>(() =>
                transactionService.CreateTransaction("Expense", 1, 50000, "Большой расход", 2));
        }

        [Fact]
        public void CalculateBalance_ShouldReturnCorrectBalanceAfterTransactions()
        {
            var repository = CreateRepositoryWithSampleData();
            var factory = new BankFactory();
            var operationManager = new OperationManager();
            var accountService = new AccountService(repository, factory, operationManager);
            var categoryService = new CategoryService(repository, factory, operationManager);
            var transactionService = new TransactionService(repository, factory, operationManager,
                                                           accountService, categoryService);

            // Начальный баланс: 10000
            transactionService.CreateTransaction("Income", 1, 5000, "Доход", 1);
            // Баланс: 10000 + 5000 = 15000
            transactionService.CreateTransaction("Expense", 1, 2000, "Расход", 2);
            // Баланс: 15000 - 2000 = 13000

            decimal balance = transactionService.CalculateBalance(1);

            Assert.Equal(13000, balance);
        }

        [Fact]
        public void DeleteTransaction_ShouldRemoveTransactionFromRepository()
        {
            var repository = CreateRepositoryWithSampleData();
            var factory = new BankFactory();
            var operationManager = new OperationManager();
            var accountService = new AccountService(repository, factory, operationManager);
            var categoryService = new CategoryService(repository, factory, operationManager);
            var transactionService = new TransactionService(repository, factory, operationManager,
                                                           accountService, categoryService);

            transactionService.CreateTransaction("Income", 1, 1000, "Временная операция", 1);
            transactionService.DeleteTransaction(1);

            Assert.Empty(repository.Transactions);
        }
    }
}
