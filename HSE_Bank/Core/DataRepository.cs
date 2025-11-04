using HSE_Bank.Core;

public class DataRepository : IDataRepository
{
    public List<Account> Accounts { get; set; } = new();
    public List<TransactionCategory> Categories { get; set; } = new();
    public List<Transaction> Transactions { get; set; } = new();
}