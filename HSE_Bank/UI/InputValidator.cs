using System;

namespace HSE_Bank.UI
{
    /// <summary>
    /// Принцип SRP: отвечает только за валидацию пользовательского ввода.
    /// </summary>
    public class InputValidator
    {
        public static int GetIntInput(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int result))
                    return result;
                Console.WriteLine("Некорректный ввод! Введите число.");
            }
        }

        public static decimal GetDecimalInput(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), out decimal result))
                    return result;
                Console.WriteLine("Некорректный ввод! Введите число.");
            }
        }

        public static string GetStringInput(string prompt, bool allowEmpty = false)
        {
            while (true)
            {
                Console.Write(prompt);
                string result = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(result) || allowEmpty)
                    return result;
                Console.WriteLine("Ввод не может быть пустым!");
            }
        }

        public static string GetTypeInput(string prompt = "Введите тип (Income/Expense): ")
        {
            while (true)
            {
                Console.Write(prompt);
                string type = Console.ReadLine();
                if (type.Equals("Income", StringComparison.OrdinalIgnoreCase) ||
                    type.Equals("Expense", StringComparison.OrdinalIgnoreCase))
                    return type;
                Console.WriteLine("Некорректный тип! Используйте Income или Expense");
            }
        }
    }
}
