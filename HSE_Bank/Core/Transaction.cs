using System;

namespace HSE_Bank.Core
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public int BankAccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; }

        public Transaction() { }

        public Transaction(int id, string type, int bankAccountId, decimal amount,
                          DateTime date, string description, int categoryId)
        {
            Id = id;
            Type = type;
            BankAccountId = bankAccountId;
            Amount = amount;
            Date = date;
            Description = description;
            CategoryId = categoryId;
        }

        public bool IsIncome() => Type.Equals("Income", StringComparison.OrdinalIgnoreCase);
        public bool IsExpense() => Type.Equals("Expense", StringComparison.OrdinalIgnoreCase);

        public override string ToString()
        {
            return $"Операция #{Id}: {Type} {Amount:C} на счет #{BankAccountId} - {Description} ({Date:dd.MM.yyyy})";
        }
    }
}
