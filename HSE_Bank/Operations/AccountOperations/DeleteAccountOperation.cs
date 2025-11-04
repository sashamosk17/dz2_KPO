using System;
using HSE_Bank.Core;

namespace HSE_Bank.Operations.AccountOperations
{
    public class DeleteAccountOperation : IOperation
    {
        private List<Account> _accounts;
        private Account _accountToDelete;

        public DeleteAccountOperation(List<Account> accounts, Account account)
        {
            _accounts = accounts;
            _accountToDelete = account;
        }

        public void Execute()
        {
            _accounts.Remove(_accountToDelete);
        }

        public void Undo()
        {
            _accounts.Add(_accountToDelete);
        }
    }
}
