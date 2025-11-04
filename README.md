# HSE_Bank

Консольное приложение для управления финансами, разработанное в рамках курса КПО. Приложение демонстрирует использование 7 паттернов проектирования GoF и принципов SOLID/GRASP.

## Реализованные паттерны GoF

1. **Facade** - AccountService, CategoryService, TransactionService
2. **Command** - CreateAccountOperation, DeleteAccountOperation, CreateCategoryOperation, DeleteCategoryOperation, CreateTransactionOperation, DeleteTransactionOperation
3. **Decorator** - TimeMeasuringOperationDecorator (измерение времени операций)
4. **Template Method** - FileReader, FileWriter (импорт/экспорт)
5. **Visitor** - AnalyticsVisitor (анализ финансов)
6. **Factory** - BankFactory (создание объектов с генерацией ID)
7. **Proxy** - CachedDataRepository (кэширование в памяти)

## Функциональность

- Управление счетами (создание, удаление, редактирование)
- Управление категориями (доход/расход)
- Управление операциями с автообновлением баланса
- Импорт/Экспорт данных (JSON, CSV, YAML)
- Аналитика по доходам и расходам
- Undo/Redo операций
- Отчеты о времени выполнения операций

## Принципы SOLID и GRASP

### SOLID

- **S - Single Responsibility**
  * AccountService отвечает только за счета
  * CategoryService отвечает только за категории
  * TransactionService отвечает только за операции

- **O - Open/Closed**
  * Можно добавлять новые типы операций без изменения существующего кода
  * IOperation интерфейс позволяет расширять функциональность

- **L - Liskov Substitution**
  * Services взаимозаменяемы через интерфейсы IAccountService, ICategoryService
  * Operation объекты взаимозаменяемы через IOperation

- **I - Interface Segregation** 
  * Маленькие специализированные интерфейсы вместо больших
  * IAccountService содержит только методы для счетов

- **D - Dependency Inversion**
  * DI-контейнер внедряет зависимости через конструктор
  * Services зависят от интерфейсов, а не конкретных реализаций

### GRASP

**Основные принципы:**

- **Information Expert**
  * Transaction знает как вычислить сумму
  * Account знает как обновить баланс
  * BankFactory знает как создать объект с правильным ID

- **Creator**
  * BankFactory создает все объекты в одном месте
  * AccountService содержит ссылку на Accounts, поэтому управляет их созданием
  * Централизованное создание = единая валидация

- **Controller**
  * Program.cs обрабатывает пользовательский ввод (Controller роль)
  * Services выполняют бизнес-логику
  * MenuPresenter отвечает за вывод

- **Low Coupling**
  * Services зависят от интерфейсов, а не конкретных классов
  * OperationManager не знает про UI
  * Каждый класс может быть изменен независимо

- **High Cohesion** 
  * AccountService содержит все методы для счетов
  * Transaction содержит все свойства операции
  * Классы узконаправленные и легко понять их назначение

- **Polymorphism**
  * IOperation используется вместо if/else для выбора типа операции
  * FileReader наследники (CsvReader, JsonReader) реализуют парсинг по-разному

- **Pure Fabrication**
  * Services - это Pure Fabrication
  * Они нужны для отделения UI логики от бизнес-логики
  * BankFactory - искусственный класс для централизованного создания

- **Indirection**
  * Repository служит прослойкой между Services и хранилищем
  * Services используют Repository вместо прямого доступа к спискам
  * CachedDataRepository - посредник между логикой и памятью

- **Protected Variations**
  * FileReader интерфейс остается стабильным, меняются только наследники
  * IDataRepository скрывает изменения в реализации хранилища
  * Новые форматы (CSV, XML) добавляются без изменения существующего кода


## Быстрый старт

### Клонировать репозиторий
```
git clone https://github.com/sashamosk17/dz2_KPO.git
cd dz2_KPO
```
### Запустить приложение
```
cd HSE_Bank
dotnet run
```
### Запустить тесты
```
cd HSE_Bank.Tests
dotnet test
```

## Структура проекта
```
HSE_Bank/
├── Core/ - Доменная модель (Account, Transaction, Category, Factory, Repository, Proxy)
├── Services/ - Слой услуг (Facade паттерн)
├── Operations/ - Command паттерн (Undo/Redo)
├── IO/Import - Импорт (Template Method)
├── IO/Export - Экспорт (Template Method)
├── UI/ - Пользовательский интерфейс
└── Program.cs

HSE_Bank.Tests/
```

## Используемые пакеты

- Microsoft.Extensions.DependencyInjection (DI-контейнер)
- Newtonsoft.Json (работа с JSON)
- YamlDotNet (работа с YAML)
- CsvHelper (работа с CSV)
- xUnit (Unit тестирование)

## Автор

Москалевич Александр БПИ246
