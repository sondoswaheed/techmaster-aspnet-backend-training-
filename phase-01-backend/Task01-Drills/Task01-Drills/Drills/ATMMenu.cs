using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01_Drills.Drills
{
    internal class ATMMenu
    {
        public static void Run()
        {
            // intial balance
            decimal balance = 1000;
            bool exit = false;

            while (!exit)
            {
                //ATM options
                Console.WriteLine("\n--- ATM Menu ---");
                Console.WriteLine("1. Check Balance");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine($"Balance: {balance:F2}");
                        break;

                    case "2":
                        balance = Deposit(balance);
                        break;

                    case "3":
                        balance = Withdraw(balance);
                        break;

                    case "4":
                        //to break from choices
                        exit = true;
                        Console.WriteLine("Thank you for using the ATM.");
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        public static decimal Deposit(decimal balance)
        {
            Console.Write("Enter deposit amount: ");

            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Invalid amount.");
                return balance;
            }

            if (amount <= 0)
            {
                Console.WriteLine("Deposit must be positive.");
                return balance;
            }
            // add the deposit to the main balance
            balance += amount;

            Console.WriteLine($"Deposit successful. Balance: {balance:F2}");

            return balance;
        }

        public static decimal Withdraw(decimal balance)
        {
            Console.Write("Enter withdrawal amount: ");

            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Invalid amount.");
                return balance;
            }

            if (amount <= 0)
            {
                Console.WriteLine("Withdrawal must be positive.");
                return balance;
            }

            if (amount > balance)
            {
                Console.WriteLine("Insufficient balance.");
                return balance;
            }

            balance -= amount;

            Console.WriteLine($"Withdrawal successful. Balance: {balance:F2}");

            return balance;
        }
    }
}
