using System;
using HSE_Bank.Core;

namespace HSE_Bank.Operations.TransactionOperations
{
    public class CreateTransactionOperation : IOperation
    {
        private BankFactory _factory;
        private List<Transaction> _transactions;
        private Transaction _createdTransaction;
        private Account _account;

        public CreateTransactionOperation(BankFactory factory, List<Transaction> transactions,
                                        string type, int bankAccountId, decimal amount,
                                        string description, int categoryId, Account account)
        {
            _factory = factory;
            _transactions = transactions;
            _account = account;
            _createdTransaction = factory.CreateTransaction(type, bankAccountId, amount, description, categoryId);
        }

        public void Execute()
        {
            if (_createdTransaction.IsIncome())
                _account.Deposit(_createdTransaction.Amount);
            else if (_createdTransaction.IsExpense())
                _account.Withdraw(_createdTransaction.Amount);

            _transactions.Add(_createdTransaction);
        }

        public void Undo()
        {
            _transactions.Remove(_createdTransaction);

            if (_createdTransaction.IsIncome())
                _account.Withdraw(_createdTransaction.Amount);
            else if (_createdTransaction.IsExpense())
                _account.Deposit(_createdTransaction.Amount);
        }
    }
}
