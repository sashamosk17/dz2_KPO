using System;
using HSE_Bank.Core;

namespace HSE_Bank.Operations.AccountOperations
{
    public class UpdateAccountOperation : IOperation
    {
        private Account _account;
        private string _newName;
        private string _oldName;

        public UpdateAccountOperation(Account account, string newName)
        {
            _account = account;
            _oldName = account.Name;
            _newName = newName;
        }

        public void Execute()
        {
            _account.Name = _newName;
        }

        public void Undo()
        {
            _account.Name = _oldName;
        }
    }
}
