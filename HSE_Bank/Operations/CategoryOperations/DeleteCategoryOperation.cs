using System;
using HSE_Bank.Core;

namespace HSE_Bank.Operations.CategoryOperations
{
    public class DeleteCategoryOperation : IOperation
    {
        private List<TransactionCategory> _categories;
        private TransactionCategory _categoryToDelete;

        public DeleteCategoryOperation(List<TransactionCategory> categories, TransactionCategory category)
        {
            _categories = categories;
            _categoryToDelete = category;
        }

        public void Execute()
        {
            _categories.Remove(_categoryToDelete);
        }

        public void Undo()
        {
            _categories.Add(_categoryToDelete);
        }
    }
}
