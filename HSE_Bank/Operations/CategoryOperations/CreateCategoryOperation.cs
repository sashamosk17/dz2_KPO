using System;
using HSE_Bank.Core;

namespace HSE_Bank.Operations.CategoryOperations
{
    public class CreateCategoryOperation : IOperation
    {
        private BankFactory _factory;
        private List<TransactionCategory> _categories;
        private TransactionCategory _createdCategory;

        public CreateCategoryOperation(BankFactory factory, List<TransactionCategory> categories, string type, string name)
        {
            _factory = factory;
            _categories = categories;
            _createdCategory = factory.CreateCategory(type, name);
        }

        public void Execute()
        {
            _categories.Add(_createdCategory);
        }

        public void Undo()
        {
            _categories.Remove(_createdCategory);
        }
    }
}
