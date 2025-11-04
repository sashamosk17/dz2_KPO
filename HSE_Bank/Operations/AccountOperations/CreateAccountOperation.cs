using System;
using HSE_Bank.Core;

namespace HSE_Bank.Operations.AccountOperations
{
    /// <summary>
    /// Команда для создания банковского счета.
    /// Паттерн Command (GoF): каждая операция — отдельный класс, реализующий интерфейс.
    /// </summary>
    public class CreateAccountOperation : IOperation
    {
        private BankFactory _factory;
        private List<Account> _accounts;
        private Account _createdAccount;

        public CreateAccountOperation(BankFactory factory, List<Account> accounts, string accountName, decimal initialBalance = 0)
        {
            _factory = factory;
            _accounts = accounts;
            _createdAccount = factory.CreateAccount(accountName, initialBalance);
        }

        public void Execute()
        {
            _accounts.Add(_createdAccount);
        }

        public void Undo()
        {
            _accounts.Remove(_createdAccount);
        }
    }
}
