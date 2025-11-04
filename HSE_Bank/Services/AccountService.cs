using System;
using System.Collections.Generic;
using System.Linq;
using HSE_Bank.Core;
using HSE_Bank.Operations;
using HSE_Bank.Operations.AccountOperations;
using HSE_Bank.Operations.Decorators;

namespace HSE_Bank.Services
{
    /// <summary>
    /// Реализация сервиса для управления счетами.
    /// Паттерн Facade (GoF): упрощает взаимодействие с Command и Models.
    /// Паттерн Decorator: используется для измерения времени операций.
    /// Принцип SRP: отвечает только за бизнес-логику счетов.
    /// </summary>
    public class AccountService : IAccountService
    {
        private DataRepository _repository;
        private BankFactory _factory;
        private OperationManager _operationManager;
        private bool _enableTiming = true;

        public AccountService(DataRepository repository, BankFactory factory, OperationManager operationManager)
        {
            _repository = repository;
            _factory = factory;
            _operationManager = operationManager;
        }

        public void CreateAccount(string name, decimal initialBalance)
        {
            var operation = new CreateAccountOperation(_factory, _repository.Accounts, name, initialBalance);

            if (_enableTiming)
            {
                var timedOperation = new TimeMeasuringOperationDecorator(operation, "CreateAccount");
                _operationManager.ExecuteOperation(timedOperation);
            }
            else
            {
                _operationManager.ExecuteOperation(operation);
            }
        }

        public void DeleteAccount(int accountId)
        {
            Account account = GetAccountById(accountId);
            if (account != null)
            {
                var operation = new DeleteAccountOperation(_repository.Accounts, account);

                if (_enableTiming)
                {
                    var timedOperation = new TimeMeasuringOperationDecorator(operation, "DeleteAccount");
                    _operationManager.ExecuteOperation(timedOperation);
                }
                else
                {
                    _operationManager.ExecuteOperation(operation);
                }
            }
        }

        public Account GetAccountById(int id)
        {
            return _repository.Accounts.FirstOrDefault(a => a.Id == id);
        }

        public List<Account> GetAllAccounts()
        {
            return _repository.Accounts;
        }

        public void UpdateAccountName(int accountId, string newName)
        {
            Account account = GetAccountById(accountId);
            if (account != null)
            {
                var operation = new UpdateAccountOperation(account, newName);

                if (_enableTiming)
                {
                    var timedOperation = new TimeMeasuringOperationDecorator(operation, "UpdateAccount");
                    _operationManager.ExecuteOperation(timedOperation);
                }
                else
                {
                    _operationManager.ExecuteOperation(operation);
                }
            }
        }

        public void Undo()
        {
            _operationManager.Undo();
        }

        public void Redo()
        {
            _operationManager.Redo();
        }

        public int GetUndoCount()
        {
            return _operationManager.GetUndoCount();
        }

        public int GetRedoCount()
        {
            return _operationManager.GetRedoCount();
        }

        public void SetTimingEnabled(bool enabled)
        {
            _enableTiming = enabled;
        }
    }
}
