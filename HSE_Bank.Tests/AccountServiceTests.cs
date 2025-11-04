using Xunit;
using HSE_Bank.Core;
using HSE_Bank.Operations;
using HSE_Bank.Services;

namespace HSE_Bank.Tests
{
    /// <summary>
    /// Unit тесты для AccountService.
    /// Тестируют создание, удаление, поиск счетов.
    /// </summary>
    public class AccountServiceTests
    {
        [Fact]
        public void CreateAccount_ShouldAddAccountToRepository()
        {
            var repository = new DataRepository();
            var factory = new BankFactory();
            var operationManager = new OperationManager();
            var accountService = new AccountService(repository, factory, operationManager);

            accountService.CreateAccount("Основной счет", 1000);

            Assert.Single(repository.Accounts);
            Assert.Equal("Основной счет", repository.Accounts[0].Name);
            Assert.Equal(1000, repository.Accounts[0].Balance);
        }

        [Fact]
        public void GetAccountById_ShouldReturnCorrectAccount()
        {
            var repository = new DataRepository();
            var factory = new BankFactory();
            var operationManager = new OperationManager();
            var accountService = new AccountService(repository, factory, operationManager);

            accountService.CreateAccount("Сбережения", 5000);
            var account = accountService.GetAccountById(1);

            Assert.NotNull(account);
            Assert.Equal("Сбережения", account.Name);
        }

        [Fact]
        public void GetAccountById_ShouldReturnNullForNonexistentAccount()
        {
            var repository = new DataRepository();
            var factory = new BankFactory();
            var operationManager = new OperationManager();
            var accountService = new AccountService(repository, factory, operationManager);

            var account = accountService.GetAccountById(999);

            Assert.Null(account);
        }

        [Fact]
        public void DeleteAccount_ShouldRemoveAccountFromRepository()
        {
            var repository = new DataRepository();
            var factory = new BankFactory();
            var operationManager = new OperationManager();
            var accountService = new AccountService(repository, factory, operationManager);

            accountService.CreateAccount("Счет для удаления", 500);
            accountService.DeleteAccount(1);

            Assert.Empty(repository.Accounts);
        }

        [Fact]
        public void GetAllAccounts_ShouldReturnAllCreatedAccounts()
        {
            var repository = new DataRepository();
            var factory = new BankFactory();
            var operationManager = new OperationManager();
            var accountService = new AccountService(repository, factory, operationManager);

            accountService.CreateAccount("Счет 1", 1000);
            accountService.CreateAccount("Счет 2", 2000);
            accountService.CreateAccount("Счет 3", 3000);

            var accounts = accountService.GetAllAccounts();

            Assert.Equal(3, accounts.Count);
        }
    }
}
