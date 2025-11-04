using System;
using System.Collections.Generic;
using System.Linq;
using HSE_Bank.Core;

namespace HSE_Bank.Services.Analytics
{
    /// <summary>
    /// Реализация Visitor для аналитики финансов.
    /// Посещает объекты доменной модели и собирает статистику.
    /// </summary>
    public class AnalyticsVisitor : IFinancialVisitor
    {
        private decimal _totalIncome = 0;
        private decimal _totalExpense = 0;
        private int _accountCount = 0;
        private int _transactionCount = 0;
        private Dictionary<string, decimal> _categoryTotals = new();

        public void Visit(Account account)
        {
            _accountCount++;
        }

        public void Visit(Transaction transaction)
        {
            _transactionCount++;
            if (transaction.IsIncome())
                _totalIncome += transaction.Amount;
            else
                _totalExpense += transaction.Amount;
        }

        public void Visit(TransactionCategory category)
        {
            if (!_categoryTotals.ContainsKey(category.Name))
                _categoryTotals[category.Name] = 0;
        }

        public void PrintReport()
        {
            Console.WriteLine("ОТЧЕТ");
            Console.WriteLine($"Общий доход:        {_totalIncome:C}");
            Console.WriteLine($"Общие расходы:      {_totalExpense:C}");
            Console.WriteLine($"Баланс:             {(_totalIncome - _totalExpense):C}");
            Console.WriteLine($"Счетов:             {_accountCount}");
            Console.WriteLine($"Операций:           {_transactionCount}");
            Console.WriteLine();
        }
    }
}