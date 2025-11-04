using System;
using System.Collections.Generic;
using HSE_Bank.Core;

namespace HSE_Bank.Services
{
    /// <summary>
    /// Интерфейс сервиса для работы с категориями.
    /// Принцип DIP (Dependency Inversion): зависимость от абстракции.
    /// </summary>
    public interface ICategoryService
    {
        void CreateCategory(string type, string name);
        void DeleteCategory(int categoryId);
        TransactionCategory GetCategoryById(int id);
        List<TransactionCategory> GetAllCategories();
        void Undo();
        void Redo();
    }
}
