using System;

namespace HSE_Bank.Core
{
    public class TransactionCategory
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public TransactionCategory() { }

        public TransactionCategory(int id, string type, string name)
        {
            Id = id;
            Type = type;
            Name = name;
        }

        public bool IsIncome() => Type.Equals("Income", StringComparison.OrdinalIgnoreCase);
        public bool IsExpense() => Type.Equals("Expense", StringComparison.OrdinalIgnoreCase);

        public override string ToString()
        {
            return $"Категория #{Id}: {Name} ({Type})";
        }
    }
}