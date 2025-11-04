using Xunit;
using HSE_Bank.Core;

namespace HSE_Bank.Tests
{
    /// <summary>
    /// Unit тесты для BankFactory.
    /// Тестируют создание объектов и генерацию уникальных ID.
    /// </summary>
    public class BankFactoryTests
    {
        [Fact]
        public void CreateAccount_ShouldGenerateUniqueIds()
        {
            var factory = new BankFactory();

            var account1 = factory.CreateAccount("Счет 1", 1000);
            var account2 = factory.CreateAccount("Счет 2", 2000);
            var account3 = factory.CreateAccount("Счет 3", 3000);

            Assert.Equal(1, account1.Id);
            Assert.Equal(2, account2.Id);
            Assert.Equal(3, account3.Id);
        }

        [Fact]
        public void CreateCategory_ShouldGenerateUniqueIds()
        {
            var factory = new BankFactory();

            var cat1 = factory.CreateCategory("Income", "Зарплата");
            var cat2 = factory.CreateCategory("Expense", "Кафе");

            Assert.Equal(1, cat1.Id);
            Assert.Equal(2, cat2.Id);
        }

        [Fact]
        public void CreateTransaction_ShouldGenerateUniqueIds()
        {
            var factory = new BankFactory();

            var tx1 = factory.CreateTransaction("Income", 1, 100, "Описание 1", 1);
            var tx2 = factory.CreateTransaction("Expense", 1, 200, "Описание 2", 2);

            Assert.Equal(1, tx1.Id);
            Assert.Equal(2, tx2.Id);
        }

        [Fact]
        public void CreateCategory_WithInvalidType_ShouldThrowException()
        {
            var factory = new BankFactory();

            Assert.Throws<ArgumentException>(() =>
                factory.CreateCategory("Invalid", "Категория"));
        }

        [Fact]
        public void CreateTransaction_WithInvalidType_ShouldThrowException()
        {
            var factory = new BankFactory();

            Assert.Throws<ArgumentException>(() =>
                factory.CreateTransaction("InvalidType", 1, 100, "Описание", 1));
        }

        [Fact]
        public void ResetCounters_ShouldResetAllIds()
        {
            var factory = new BankFactory();

            var account1 = factory.CreateAccount("Счет", 1000);
            Assert.Equal(1, account1.Id);

            factory.ResetCounters();

            var account2 = factory.CreateAccount("Счет", 1000);
            Assert.Equal(1, account2.Id);
        }
    }
}
