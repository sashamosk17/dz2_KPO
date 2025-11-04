using System;
using System.Collections.Generic;

namespace HSE_Bank.Core
{
    /// <summary>
    /// Паттерн Proxy: предоставляет заменитель для другого объекта для контроля доступа.
    /// Реализует in-memory кэш для быстрого доступа к данным.
    /// </summary>
    public class CachedDataRepository : IDataRepository
    {
        private readonly DataRepository _realRepository;
        private Dictionary<string, object> _cache;
        private bool _isCacheValid = false;

        public List<Account> Accounts
        {
            get
            {
                if (!_isCacheValid)
                    RefreshCache();
                return _realRepository.Accounts;
            }
        }

        public List<TransactionCategory> Categories
        {
            get
            {
                if (!_isCacheValid)
                    RefreshCache();
                return _realRepository.Categories;
            }
        }

        public List<Transaction> Transactions
        {
            get
            {
                if (!_isCacheValid)
                    RefreshCache();
                return _realRepository.Transactions;
            }
        }

        public CachedDataRepository(DataRepository realRepository)
        {
            _realRepository = realRepository;
            _cache = new Dictionary<string, object>();
            RefreshCache();
        }

        public void RefreshCache()
        {
            _cache["accounts"] = _realRepository.Accounts;
            _cache["categories"] = _realRepository.Categories;
            _cache["transactions"] = _realRepository.Transactions;
            _isCacheValid = true;
            Console.WriteLine("Кэш обновлен");
        }

        public void InvalidateCache()
        {
            _isCacheValid = false;
            Console.WriteLine("Кэш инвалидирован");
        }
    }
}