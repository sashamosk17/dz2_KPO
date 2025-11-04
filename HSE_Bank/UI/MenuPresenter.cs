using System;
using System.Collections.Generic;
using HSE_Bank.Core;

namespace HSE_Bank.UI
{
    public static class MenuPresenter
    {
        public static void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("1. Управление счетами");
            Console.WriteLine("2. Управление категориями");
            Console.WriteLine("3. Управление операциями");
            Console.WriteLine("4. Экспорт данных");
            Console.WriteLine("5. Импорт данных");
            Console.WriteLine("6. Отменить операцию (Undo)");
            Console.WriteLine("7. Повторить операцию (Redo)");
            Console.WriteLine("8. Просмотр аналитики (Visitor паттерн)");
            Console.WriteLine("9. Обновить кэш данных (Proxy паттерн)");
            Console.WriteLine("0. Выход");
            Console.WriteLine();
        }

        public static void ShowAccountsMenu()
        {
            Console.Clear();
            Console.WriteLine("УПРАВЛЕНИЕ СЧЕТАМИ");
            Console.WriteLine("1. Создать новый счет");
            Console.WriteLine("2. Удалить счет");
            Console.WriteLine("3. Просмотреть все счета");
            Console.WriteLine("4. Вернуться в главное меню");
            Console.WriteLine();
        }

        public static void ShowCategoriesMenu()
        {
            Console.Clear();
            Console.WriteLine("УПРАВЛЕНИЕ КАТЕГОРИЯМИ");
            Console.WriteLine("1. Создать новую категорию");
            Console.WriteLine("2. Удалить категорию");
            Console.WriteLine("3. Просмотреть все категории");
            Console.WriteLine("4. Вернуться в главное меню");
            Console.WriteLine();
        }

        public static void ShowTransactionsMenu()
        {
            Console.Clear();
            Console.WriteLine("УПРАВЛЕНИЕ ОПЕРАЦИЯМИ");
            Console.WriteLine();
            Console.WriteLine("1. Создать новую операцию");
            Console.WriteLine("2. Удалить операцию");
            Console.WriteLine("3. Просмотреть все операции");
            Console.WriteLine("4. Расчитать баланс по счету");
            Console.WriteLine("5. Вернуться в главное меню");
            Console.WriteLine();
        }

        public static void ShowTransactionInfo()
        {
            Console.WriteLine("СОЗДАНИЕ НОВОЙ ОПЕРАЦИИ");
            Console.WriteLine();
            Console.WriteLine("Тип операции определяется выбранной категорией:");
            Console.WriteLine("- Income (Доход) - поступление денег на счет");
            Console.WriteLine("- Expense (Расход) - снятие денег со счета");
            Console.WriteLine();
        }

        public static void ShowAccounts(List<Account> accounts)
        {
            Console.Clear();
            Console.WriteLine("ВСЕ СЧЕТА");
            Console.WriteLine();

            if (accounts.Count == 0)
            {
                Console.WriteLine("Счетов нет");
                return;
            }

            foreach (var account in accounts)
            {
                Console.WriteLine("ID: {0}", account.Id);
                Console.WriteLine("Имя: {0}", account.Name);
                Console.WriteLine("Баланс: {0:C}", account.Balance);
                Console.WriteLine();
            }
        }

        public static void ShowAccountsForSelection(List<Account> accounts)
        {
            if (accounts.Count == 0)
            {
                Console.WriteLine("Счетов нет");
                return;
            }

            foreach (var account in accounts)
            {
                Console.WriteLine("ID {0}: {1} - Баланс: {2:C}", account.Id, account.Name, account.Balance);
            }
            Console.WriteLine();
        }

        public static void ShowCategories(List<TransactionCategory> categories)
        {
            Console.Clear();
            Console.WriteLine("ВСЕ КАТЕГОРИИ");
            Console.WriteLine();

            if (categories.Count == 0)
            {
                Console.WriteLine("Категорий нет");
                return;
            }

            foreach (var category in categories)
            {
                Console.WriteLine("ID: {0}", category.Id);
                Console.WriteLine("Тип: {0}", category.Type);
                Console.WriteLine("Имя: {0}", category.Name);
                Console.WriteLine();
            }
        }

        public static void ShowCategoriesForSelection(List<TransactionCategory> categories)
        {
            if (categories.Count == 0)
            {
                Console.WriteLine("Категорий нет");
                return;
            }

            foreach (var category in categories)
            {
                Console.WriteLine("ID {0}: {1} ({2})", category.Id, category.Name, category.Type);
            }
            Console.WriteLine();
        }

        public static void ShowTransactions(List<Transaction> transactions)
        {
            Console.Clear();
            Console.WriteLine("ВСЕ ОПЕРАЦИИ");
            Console.WriteLine();

            if (transactions.Count == 0)
            {
                Console.WriteLine("Операций нет");
                return;
            }

            foreach (var transaction in transactions)
            {
                Console.WriteLine("ID: {0}", transaction.Id);
                Console.WriteLine("Тип: {0}", transaction.Type);
                Console.WriteLine("Счет: #{0}", transaction.BankAccountId);
                Console.WriteLine("Сумма: {0:C}", transaction.Amount);
                Console.WriteLine("Дата: {0:dd.MM.yyyy HH:mm}", transaction.Date);
                Console.WriteLine("Описание: {0}", transaction.Description);
                Console.WriteLine("Категория: #{0}", transaction.CategoryId);
                Console.WriteLine();
            }
        }

        public static void ShowSuccessMessage(string message)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[Успех] {0}", message);
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void ShowErrorMessage(string message)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[Ошибка] {0}", message);
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void ShowMessage(string message)
        {
            Console.WriteLine();
            Console.WriteLine("[Информация] {0}", message);
            Console.WriteLine();
        }

        public static void WaitForInput()
        {
            Console.WriteLine("Нажмите Enter для продолжения...");
            Console.ReadLine();
        }

        public static void WaitForReturn()
        {
            Console.WriteLine();
            Console.WriteLine("Нажмите Enter для возврата...");
            Console.ReadLine();
        }
    }
}
