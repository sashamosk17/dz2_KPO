using System;
using System.Collections.Generic;
using System.Linq;
using HSE_Bank.Core;
using HSE_Bank.Operations;
using HSE_Bank.Operations.CategoryOperations;

namespace HSE_Bank.Services
{
    /// <summary>
    /// Реализация сервиса для управления категориями.
    /// Паттерн Facade (GoF): обеспечивает простой интерфейс для работы с командами.
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private DataRepository _repository;
        private BankFactory _factory;
        private OperationManager _operationManager;

        public CategoryService(DataRepository repository, BankFactory factory, OperationManager operationManager)
        {
            _repository = repository;
            _factory = factory;
            _operationManager = operationManager;
        }

        public void CreateCategory(string type, string name)
        {
            var operation = new CreateCategoryOperation(_factory, _repository.Categories, type, name);
            _operationManager.ExecuteOperation(operation);
        }

        public void DeleteCategory(int categoryId)
        {
            TransactionCategory category = GetCategoryById(categoryId);
            if (category != null)
            {
                var operation = new DeleteCategoryOperation(_repository.Categories, category);
                _operationManager.ExecuteOperation(operation);
            }
        }

        public TransactionCategory GetCategoryById(int id)
        {
            return _repository.Categories.FirstOrDefault(c => c.Id == id);
        }

        public List<TransactionCategory> GetAllCategories()
        {
            return _repository.Categories;
        }

        public void Undo()
        {
            _operationManager.Undo();
        }

        public void Redo()
        {
            _operationManager.Redo();
        }
    }
}
