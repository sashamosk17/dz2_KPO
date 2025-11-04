using System;
using System.Collections.Generic;
using HSE_Bank.Core;

namespace HSE_Bank.Services
{
    /// <summary>
    /// Интерфейс сервиса для работы со счетами.
    /// Принцип DIP (Dependency Inversion): зависимость от абстракции.
    /// </summary>
    public interface IAccountService
    {
        void CreateAccount(string name, decimal initialBalance);
        void DeleteAccount(int accountId);
        Account GetAccountById(int id);
        List<Account> GetAllAccounts();
        void UpdateAccountName(int accountId, string newName);
        void Undo();
        void Redo();
        int GetUndoCount();
        int GetRedoCount();
    }
}
