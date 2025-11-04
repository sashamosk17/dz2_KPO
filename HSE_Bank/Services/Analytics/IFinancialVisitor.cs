using HSE_Bank.Core;

namespace HSE_Bank.Services.Analytics
{
    /// <summary>
    /// Паттерн Visitor: позволяет определить новую операцию без изменения классов элементов.
    /// Поведенческий паттерн для сложных структур данных.
    /// </summary>
    public interface IFinancialVisitor
    {
        void Visit(Account account);
        void Visit(Transaction transaction);
        void Visit(TransactionCategory category);
    }
}