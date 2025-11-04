using System;
using HSE_Bank.Core;

namespace HSE_Bank.Operations.TransactionOperations
{
    public class DeleteTransactionOperation : IOperation
    {
        private List<Transaction> _transactions;
        private Transaction _transactionToDelete;

        public DeleteTransactionOperation(List<Transaction> transactions, Transaction transaction)
        {
            _transactions = transactions;
            _transactionToDelete = transaction;
        }

        public void Execute()
        {
            _transactions.Remove(_transactionToDelete);
        }

        public void Undo()
        {
            _transactions.Add(_transactionToDelete);
        }
    }
}
