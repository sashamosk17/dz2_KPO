namespace HSE_Bank.Core
{

    /// <summary>
    /// Доменная модель банковского счета.
    /// Принцип SRP (Single Responsibility): отвечает только за данные и бизнес-логику счета.
    /// </summary>
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Balance { get; set; }

        public Account() { }

        public Account(int id, string name, decimal balance)
        {
            Id = id;
            Name = name;
            Balance = balance;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Сумма пополнения должна быть положительной");

            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Сумма снятия должна быть положительной");

            if (Balance < amount)
                throw new InvalidOperationException("Недостаточно средств на счете");

            Balance -= amount;
        }

        public override string ToString()
        {
            return $"Счет #{Id}: {Name}, Баланс: {Balance:C}";
        }
    }
}