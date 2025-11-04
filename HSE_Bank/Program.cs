using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using HSE_Bank.Core;
using HSE_Bank.Operations;
using HSE_Bank.Services;
using HSE_Bank.IO.Import;
using HSE_Bank.IO.Export;
using HSE_Bank.UI;
using HSE_Bank.Services.Analytics;

namespace HSE_Bank
{
    /// <summary>
    /// Главный класс приложения HSE_Bank.
    /// Демонстрирует использование всех 7 паттернов GoF:
    /// 1. Facade - AccountService, CategoryService, TransactionService
    /// 2. Command - CreateAccountOperation, DeleteAccountOperation, CreateTransactionOperation
    /// 3. Decorator - TimeMeasuringOperationDecorator
    /// 4. Template Method - FileReader с наследованием (CsvReader, JsonReader, YamlReader)
    /// 5. Visitor - AnalyticsVisitor для сбора статистики
    /// 6. Factory - BankFactory для создания объектов
    /// 7. Proxy - CachedDataRepository для кэширования
    /// </summary>
    class Program
    {
        private static CachedDataRepository _cachedRepository;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var services = new ServiceCollection();
            services.AddSingleton<DataRepository>();
            services.AddSingleton<BankFactory>();
            services.AddSingleton<OperationManager>();
            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<ICategoryService, CategoryService>();
            services.AddSingleton<ITransactionService, TransactionService>();
            services.AddSingleton<FileReader>();
            services.AddSingleton<FileWriter>();

            var serviceProvider = services.BuildServiceProvider();

            var repository = serviceProvider.GetRequiredService<DataRepository>();
            _cachedRepository = new CachedDataRepository(repository);

            var accountService = serviceProvider.GetRequiredService<IAccountService>();
            var categoryService = serviceProvider.GetRequiredService<ICategoryService>();
            var transactionService = serviceProvider.GetRequiredService<ITransactionService>();
            var fileReader = serviceProvider.GetRequiredService<FileReader>();
            var fileWriter = serviceProvider.GetRequiredService<FileWriter>();

            ShowMenu(accountService, categoryService, transactionService, fileReader, fileWriter);
        }

        static void ShowMenu(IAccountService accountService, ICategoryService categoryService,
                            ITransactionService transactionService, FileReader fileReader, FileWriter fileWriter)
        {
            bool running = true;
            while (running)
            {
                MenuPresenter.ShowMainMenu();
                string choice = InputValidator.GetStringInput("Выберите опцию: ", true);

                switch (choice)
                {
                    case "1":
                        ManageAccounts(accountService);
                        break;
                    case "2":
                        ManageCategories(categoryService);
                        break;
                    case "3":
                        ManageTransactions(transactionService, accountService, categoryService);
                        break;
                    case "4":
                        ExportData(accountService, categoryService, transactionService, fileWriter);
                        break;
                    case "5":
                        ImportData(accountService, categoryService, transactionService, fileReader);
                        break;
                    case "6":
                        accountService.Undo();
                        MenuPresenter.ShowSuccessMessage("Операция отменена!");
                        MenuPresenter.WaitForInput();
                        break;
                    case "7":
                        accountService.Redo();
                        MenuPresenter.ShowSuccessMessage("Операция повторена!");
                        MenuPresenter.WaitForInput();
                        break;
                    case "8":
                        ShowAnalytics(accountService, transactionService);
                        break;
                    case "9":
                        _cachedRepository.RefreshCache();
                        MenuPresenter.ShowSuccessMessage("Кэш обновлен!");
                        MenuPresenter.WaitForInput();
                        break;
                    case "0":
                        running = false;
                        Console.Clear();
                        MenuPresenter.ShowMessage("До свидания!");
                        break;
                    default:
                        MenuPresenter.ShowMessage("Неверный выбор!");
                        MenuPresenter.WaitForInput();
                        break;
                }
            }
        }

        static void ManageAccounts(IAccountService accountService)
        {
            bool managing = true;
            while (managing)
            {
                MenuPresenter.ShowAccountsMenu();
                string choice = InputValidator.GetStringInput("Выберите опцию: ", true);

                switch (choice)
                {
                    case "1":
                        CreateAccount(accountService);
                        break;
                    case "2":
                        DeleteAccount(accountService);
                        break;
                    case "3":
                        MenuPresenter.ShowAccounts(accountService.GetAllAccounts());
                        MenuPresenter.WaitForReturn();
                        break;
                    case "4":
                        managing = false;
                        break;
                    default:
                        MenuPresenter.ShowMessage("Неверный выбор!");
                        MenuPresenter.WaitForInput();
                        break;
                }
            }
        }

        static void CreateAccount(IAccountService accountService)
        {
            Console.Clear();
            string name = InputValidator.GetStringInput("Введите имя счета: ");
            decimal balance = InputValidator.GetDecimalInput("Введите начальный баланс: ");

            try
            {
                accountService.CreateAccount(name, balance);
                MenuPresenter.ShowSuccessMessage("Счет создан успешно!");
            }
            catch (Exception ex)
            {
                MenuPresenter.ShowErrorMessage("Ошибка: " + ex.Message);
            }
            MenuPresenter.WaitForInput();
        }

        static void DeleteAccount(IAccountService accountService)
        {
            Console.Clear();
            MenuPresenter.ShowAccountsForSelection(accountService.GetAllAccounts());
            if (accountService.GetAllAccounts().Count == 0)
            {
                MenuPresenter.WaitForInput();
                return;
            }
            int id = InputValidator.GetIntInput("Введите ID счета для удаления: ");
            Account account = accountService.GetAccountById(id);
            if (account != null)
            {
                accountService.DeleteAccount(id);
                MenuPresenter.ShowSuccessMessage("Счет удален!");
            }
            else
            {
                MenuPresenter.ShowErrorMessage("Счет с таким ID не найден!");
            }
            MenuPresenter.WaitForInput();
        }

        static void ManageCategories(ICategoryService categoryService)
        {
            bool managing = true;
            while (managing)
            {
                MenuPresenter.ShowCategoriesMenu();
                string choice = InputValidator.GetStringInput("Выберите опцию: ", true);

                switch (choice)
                {
                    case "1":
                        CreateCategory(categoryService);
                        break;
                    case "2":
                        DeleteCategory(categoryService);
                        break;
                    case "3":
                        MenuPresenter.ShowCategories(categoryService.GetAllCategories());
                        MenuPresenter.WaitForReturn();
                        break;
                    case "4":
                        managing = false;
                        break;
                    default:
                        MenuPresenter.ShowMessage("Неверный выбор!");
                        MenuPresenter.WaitForInput();
                        break;
                }
            }
        }

        static void CreateCategory(ICategoryService categoryService)
        {
            Console.Clear();
            string type = InputValidator.GetTypeInput();
            string name = InputValidator.GetStringInput("Введите имя категории: ");

            try
            {
                categoryService.CreateCategory(type, name);
                MenuPresenter.ShowSuccessMessage("Категория создана!");
            }
            catch (Exception ex)
            {
                MenuPresenter.ShowErrorMessage("Ошибка: " + ex.Message);
            }
            MenuPresenter.WaitForInput();
        }

        static void DeleteCategory(ICategoryService categoryService)
        {
            Console.Clear();
            MenuPresenter.ShowCategoriesForSelection(categoryService.GetAllCategories());
            if (categoryService.GetAllCategories().Count == 0)
            {
                MenuPresenter.WaitForInput();
                return;
            }
            int id = InputValidator.GetIntInput("Введите ID категории для удаления: ");
            TransactionCategory category = categoryService.GetCategoryById(id);
            if (category != null)
            {
                categoryService.DeleteCategory(id);
                MenuPresenter.ShowSuccessMessage("Категория удалена!");
            }
            else
            {
                MenuPresenter.ShowErrorMessage("Категория с таким ID не найдена!");
            }
            MenuPresenter.WaitForInput();
        }

        static void ManageTransactions(ITransactionService transactionService,
                              IAccountService accountService, ICategoryService categoryService)
        {
            bool managing = true;
            while (managing)
            {
                MenuPresenter.ShowTransactionsMenu();
                string choice = InputValidator.GetStringInput("Выберите опцию: ", true);

                switch (choice)
                {
                    case "1":
                        CreateTransaction(transactionService, accountService, categoryService);
                        break;
                    case "2":
                        DeleteTransaction(transactionService);
                        break;
                    case "3":
                        MenuPresenter.ShowTransactions(transactionService.GetAllTransactions());
                        MenuPresenter.WaitForReturn();
                        break;
                    case "4":
                        CalculateBalance(transactionService);
                        break;
                    case "5":
                        managing = false;
                        break;
                    default:
                        MenuPresenter.ShowMessage("Неверный выбор!");
                        MenuPresenter.WaitForInput();
                        break;
                }
            }
        }

        static void CreateTransaction(ITransactionService transactionService,
                              IAccountService accountService, ICategoryService categoryService)
        {
            Console.Clear();
            MenuPresenter.ShowTransactionInfo();

            MenuPresenter.ShowCategoriesForSelection(categoryService.GetAllCategories());
            if (categoryService.GetAllCategories().Count == 0)
            {
                MenuPresenter.ShowErrorMessage("Категорий нет!");
                MenuPresenter.WaitForInput();
                return;
            }

            int categoryId = InputValidator.GetIntInput("Введите ID категории: ");
            while (categoryService.GetCategoryById(categoryId) == null)
            {
                MenuPresenter.ShowErrorMessage("Категория не найдена!");
                categoryId = InputValidator.GetIntInput("Введите ID категории: ");
            }

            TransactionCategory category = categoryService.GetCategoryById(categoryId);
            string type = category.Type;

            MenuPresenter.ShowAccountsForSelection(accountService.GetAllAccounts());
            if (accountService.GetAllAccounts().Count == 0)
            {
                MenuPresenter.ShowErrorMessage("Счетов нет!");
                MenuPresenter.WaitForInput();
                return;
            }

            int bankAccountId = InputValidator.GetIntInput("Введите ID счета операции: ");
            while (accountService.GetAccountById(bankAccountId) == null)
            {
                MenuPresenter.ShowErrorMessage("Счет не найден!");
                bankAccountId = InputValidator.GetIntInput("Введите ID счета операции: ");
            }

            decimal amount = InputValidator.GetDecimalInput("Введите сумму: ");
            Account account = accountService.GetAccountById(bankAccountId);

            if (type.Equals("Expense", StringComparison.OrdinalIgnoreCase))
            {
                while (account.Balance < amount)
                {
                    MenuPresenter.ShowErrorMessage("Недостаточно средств! На счете: " + account.Balance.ToString("C"));
                    amount = InputValidator.GetDecimalInput("Введите сумму: ");
                }
            }

            string description = InputValidator.GetStringInput("Введите описание: ", true);

            try
            {
                transactionService.CreateTransaction(type, bankAccountId, amount, description, categoryId);
                MenuPresenter.ShowSuccessMessage("Операция создана!");
                _cachedRepository.InvalidateCache();
            }
            catch (Exception ex)
            {
                MenuPresenter.ShowErrorMessage("Ошибка: " + ex.Message);
            }
            MenuPresenter.WaitForInput();
        }

        static void DeleteTransaction(ITransactionService transactionService)
        {
            Console.Clear();
            MenuPresenter.ShowTransactions(transactionService.GetAllTransactions());
            if (transactionService.GetAllTransactions().Count == 0)
            {
                MenuPresenter.WaitForInput();
                return;
            }
            int id = InputValidator.GetIntInput("Введите ID транзакции для удаления: ");
            Transaction transaction = transactionService.GetTransactionById(id);
            if (transaction != null)
            {
                transactionService.DeleteTransaction(id);
                MenuPresenter.ShowSuccessMessage("Транзакция удалена!");
                _cachedRepository.InvalidateCache();
            }
            else
            {
                MenuPresenter.ShowErrorMessage("Транзакция не найдена!");
            }
            MenuPresenter.WaitForInput();
        }

        static void CalculateBalance(ITransactionService transactionService)
        {
            Console.Clear();
            int accId = InputValidator.GetIntInput("Введите ID счета: ");
            decimal balance = transactionService.CalculateBalance(accId);
            MenuPresenter.ShowMessage("Баланс по счету: " + balance.ToString("C"));
            MenuPresenter.WaitForInput();
        }

        static void ShowAnalytics(IAccountService accountService, ITransactionService transactionService)
        {
            Console.Clear();

            var visitor = new AnalyticsVisitor();

            foreach (var account in accountService.GetAllAccounts())
            {
                visitor.Visit(account);
            }

            foreach (var transaction in transactionService.GetAllTransactions())
            {
                visitor.Visit(transaction);
            }

            visitor.PrintReport();
            MenuPresenter.WaitForInput();
        }

        static void ExportData(IAccountService accountService, ICategoryService categoryService,
                        ITransactionService transactionService, FileWriter fileWriter)
        {
            Console.Clear();
            Console.WriteLine("=== Экспорт данных ===");
            Console.WriteLine("0. Выход");
            Console.WriteLine("1. Экспорт в JSON");
            Console.WriteLine("2. Экспорт в CSV");
            Console.WriteLine("3. Экспорт в YAML");
            string choice = InputValidator.GetStringInput("Выберите формат: ", true);

            string filePathAccounts = "";
            string filePathCategories = "";
            string filePathTransactions = "";

            switch (choice)
            {
                case "0":
                    return;
                case "1":
                    filePathAccounts = "accounts.json";
                    filePathCategories = "categories.json";
                    filePathTransactions = "transactions.json";
                    break;
                case "2":
                    filePathAccounts = "accounts.csv";
                    filePathCategories = "categories.csv";
                    filePathTransactions = "transactions.csv";
                    break;
                case "3":
                    filePathAccounts = "accounts.yaml";
                    filePathCategories = "categories.yaml";
                    filePathTransactions = "transactions.yaml";
                    break;
                default:
                    MenuPresenter.ShowErrorMessage("Неверный выбор!");
                    MenuPresenter.WaitForInput();
                    return;
            }

            try
            {
                fileWriter.WriteFile(filePathAccounts, accountService.GetAllAccounts());
                MenuPresenter.ShowSuccessMessage("Счета экспортированы в " + filePathAccounts);

                fileWriter.WriteFile(filePathCategories, categoryService.GetAllCategories());
                MenuPresenter.ShowSuccessMessage("Категории экспортированы в " + filePathCategories);

                fileWriter.WriteFile(filePathTransactions, transactionService.GetAllTransactions());
                MenuPresenter.ShowSuccessMessage("Операции экспортированы в " + filePathTransactions);
            }
            catch (Exception ex)
            {
                MenuPresenter.ShowErrorMessage("Ошибка: " + ex.Message);
            }
            MenuPresenter.WaitForInput();
        }

        static void ImportData(IAccountService accountService, ICategoryService categoryService,
                       ITransactionService transactionService, FileReader fileReader)
        {
            Console.Clear();
            Console.WriteLine("=== Импорт данных ===");
            Console.WriteLine("0. Выход");
            Console.WriteLine("1. Импорт из JSON");
            Console.WriteLine("2. Импорт из CSV");
            Console.WriteLine("3. Импорт из YAML");
            string choice = InputValidator.GetStringInput("Выберите формат: ", true);

            string filePathAccounts = "";
            string filePathCategories = "";
            string filePathTransactions = "";

            switch (choice)
            {
                case "0":
                    return;
                case "1":
                    filePathAccounts = "accounts.json";
                    filePathCategories = "categories.json";
                    filePathTransactions = "transactions.json";
                    break;
                case "2":
                    filePathAccounts = "accounts.csv";
                    filePathCategories = "categories.csv";
                    filePathTransactions = "transactions.csv";
                    break;
                case "3":
                    filePathAccounts = "accounts.yaml";
                    filePathCategories = "categories.yaml";
                    filePathTransactions = "transactions.yaml";
                    break;
                default:
                    MenuPresenter.ShowErrorMessage("Неверный выбор!");
                    MenuPresenter.WaitForInput();
                    return;
            }

            try
            {
                var accounts = fileReader.ReadFile<List<Account>>(filePathAccounts);
                if (accounts != null && accounts.Count > 0)
                {
                    foreach (var account in accounts)
                    {
                        accountService.CreateAccount(account.Name, account.Balance);
                    }
                    MenuPresenter.ShowSuccessMessage("Счета импортированы: " + accounts.Count + " записей");
                }

                var categories = fileReader.ReadFile<List<TransactionCategory>>(filePathCategories);
                if (categories != null && categories.Count > 0)
                {
                    foreach (var category in categories)
                    {
                        categoryService.CreateCategory(category.Type, category.Name);
                    }
                    MenuPresenter.ShowSuccessMessage("Категории импортированы: " + categories.Count + " записей");
                }

                var transactions = fileReader.ReadFile<List<Transaction>>(filePathTransactions);
                if (transactions != null && transactions.Count > 0)
                {
                    foreach (var transaction in transactions)
                    {
                        transactionService.CreateTransaction(transaction.Type, transaction.BankAccountId,
                                                             transaction.Amount, transaction.Description, transaction.CategoryId);
                    }
                    MenuPresenter.ShowSuccessMessage("Операции импортированы: " + transactions.Count + " записей");
                }

                _cachedRepository.RefreshCache();
            }
            catch (Exception ex)
            {
                MenuPresenter.ShowErrorMessage("Ошибка: " + ex.Message);
            }
            MenuPresenter.WaitForInput();
        }
    }
}
