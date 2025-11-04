using Xunit;
using HSE_Bank.Core;
using HSE_Bank.Operations;
using HSE_Bank.Services;

namespace HSE_Bank.Tests
{
    /// <summary>
    /// Unit тесты для Undo/Redo функциональности.
    /// Тестируют отмену и повтор операций.
    /// </summary>
    public class UndoRedoTests
    {
        [Fact]
        public void Undo_ShouldRemoveLastCreatedAccount()
        {
            var repository = new DataRepository();
            var factory = new BankFactory();
            var operationManager = new OperationManager();
            var accountService = new AccountService(repository, factory, operationManager);

            accountService.CreateAccount("Счет 1", 1000);
            Assert.Single(repository.Accounts);

            accountService.Undo();
            Assert.Empty(repository.Accounts);
        }

        [Fact]
        public void Redo_ShouldRestoreUndoneAccount()
        {
            var repository = new DataRepository();
            var factory = new BankFactory();
            var operationManager = new OperationManager();
            var accountService = new AccountService(repository, factory, operationManager);

            accountService.CreateAccount("Счет 1", 1000);
            accountService.Undo();
            accountService.Redo();

            Assert.Single(repository.Accounts);
            Assert.Equal("Счет 1", repository.Accounts[0].Name);
        }

        [Fact]
        public void Undo_AfterMultipleOperations_ShouldUndoOnlyLastOne()
        {
            var repository = new DataRepository();
            var factory = new BankFactory();
            var operationManager = new OperationManager();
            var accountService = new AccountService(repository, factory, operationManager);

            accountService.CreateAccount("Счет 1", 1000);
            accountService.CreateAccount("Счет 2", 2000);
            accountService.CreateAccount("Счет 3", 3000);

            accountService.Undo();

            Assert.Equal(2, repository.Accounts.Count);
            Assert.Equal("Счет 2", repository.Accounts[1].Name);
        }
    }
}
