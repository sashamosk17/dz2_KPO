using Xunit;
using HSE_Bank.Core;
using HSE_Bank.Operations;
using HSE_Bank.Services;

namespace HSE_Bank.Tests
{
    /// <summary>
    /// Unit тесты для CategoryService.
    /// Тестируют создание, удаление, поиск категорий.
    /// </summary>
    public class CategoryServiceTests
    {
        [Fact]
        public void CreateCategory_ShouldAddCategoryToRepository()
        {
            var repository = new DataRepository();
            var factory = new BankFactory();
            var operationManager = new OperationManager();
            var categoryService = new CategoryService(repository, factory, operationManager);

            categoryService.CreateCategory("Income", "Зарплата");

            Assert.Single(repository.Categories);
            Assert.Equal("Income", repository.Categories[0].Type);
            Assert.Equal("Зарплата", repository.Categories[0].Name);
        }

        [Fact]
        public void CreateCategory_WithInvalidType_ShouldThrowException()
        {
            var repository = new DataRepository();
            var factory = new BankFactory();
            var operationManager = new OperationManager();
            var categoryService = new CategoryService(repository, factory, operationManager);

            Assert.Throws<ArgumentException>(() =>
                categoryService.CreateCategory("Invalid", "Категория"));
        }

        [Fact]
        public void GetCategoryById_ShouldReturnCorrectCategory()
        {
            var repository = new DataRepository();
            var factory = new BankFactory();
            var operationManager = new OperationManager();
            var categoryService = new CategoryService(repository, factory, operationManager);

            categoryService.CreateCategory("Expense", "Кафе");
            var category = categoryService.GetCategoryById(1);

            Assert.NotNull(category);
            Assert.Equal("Кафе", category.Name);
        }

        [Fact]
        public void DeleteCategory_ShouldRemoveCategoryFromRepository()
        {
            var repository = new DataRepository();
            var factory = new BankFactory();
            var operationManager = new OperationManager();
            var categoryService = new CategoryService(repository, factory, operationManager);

            categoryService.CreateCategory("Income", "Бонус");
            categoryService.DeleteCategory(1);

            Assert.Empty(repository.Categories);
        }
    }
}
