using Xunit;
using HSE_Bank.Core;
using HSE_Bank.Operations;
using HSE_Bank.Operations.AccountOperations;

namespace HSE_Bank.Tests
{
    /// <summary>
    /// Unit тесты для операций (Command pattern).
    /// Тестируют Execute и Undo для отдельных команд.
    /// </summary>
    public class OperationTests
    {
        [Fact]
        public void CreateAccountOperation_ExecuteShouldAddAccount()
        {
            var accounts = new List<Account>();
            var factory = new BankFactory();
            var operation = new CreateAccountOperation(factory, accounts, "Тестовый счет", 5000);

            operation.Execute();

            Assert.Single(accounts);
            Assert.Equal("Тестовый счет", accounts[0].Name);
            Assert.Equal(5000, accounts[0].Balance);
        }

        [Fact]
        public void CreateAccountOperation_UndoShouldRemoveAccount()
        {
            var accounts = new List<Account>();
            var factory = new BankFactory();
            var operation = new CreateAccountOperation(factory, accounts, "Тестовый счет", 5000);

            operation.Execute();
            operation.Undo();

            Assert.Empty(accounts);
        }

        [Fact]
        public void DeleteAccountOperation_ExecuteShouldRemoveAccount()
        {
            var accounts = new List<Account>();
            var account = new Account(1, "Счет", 5000);
            accounts.Add(account);

            var operation = new DeleteAccountOperation(accounts, account);
            operation.Execute();

            Assert.Empty(accounts);
        }

        [Fact]
        public void DeleteAccountOperation_UndoShouldRestoreAccount()
        {
            var accounts = new List<Account>();
            var account = new Account(1, "Счет", 5000);
            accounts.Add(account);

            var operation = new DeleteAccountOperation(accounts, account);
            operation.Execute();
            operation.Undo();

            Assert.Single(accounts);
            Assert.Equal("Счет", accounts[0].Name);
        }
    }
}
