using HSE_Bank.Core;
using System.Collections.Generic;

namespace HSE_Bank.Core
{
    public interface IDataRepository
    {
        List<Account> Accounts { get; }
        List<TransactionCategory> Categories { get; }
        List<Transaction> Transactions { get; }
    }
}