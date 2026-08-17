using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01_Drills.Drills
{
    internal class Expense
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }

        public Expense(string name, decimal amount)
        {
            Name = name;
            Amount = amount;
        }
    }

    internal class SimpleExpenseTracker14
    {
        public static void Track()
        {
            Console.Write("Enter number of expenses: ");

            if (!int.TryParse(Console.ReadLine(), out int count) || count <= 0)
            {
                Console.WriteLine("No expenses entered.");
                return;
            }

            List<Expense> expenses = new();

            for (int i = 1; i <= count; i++)
            {
                Console.WriteLine($"\nExpense {i}");

                Console.Write("Enter expense name: ");
                string? name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Expense name cannot be empty.");
                    return;
                }

                Console.WriteLine("Enter amount: ");

                // if user input another thing except decimal 
                if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
                {
                    Console.WriteLine("Invalid amount.");
                    return;
                }
                // store name and amount
                expenses.Add(new Expense(name, amount));
            }

            decimal total = 0;
            Expense highest = expenses[0];

            foreach (Expense expense in expenses)
            {
                // calculate total
                total += expense.Amount;

                if (expense.Amount > highest.Amount)
                {
                    highest = expense;
                }
            }

            decimal average = total / expenses.Count;

            Console.WriteLine("\n--- Expense Summary ---");
            Console.WriteLine($"Total: {total:F2}");
            Console.WriteLine($"Average: {average:F2}");
            Console.WriteLine($"Highest: {highest.Name} - {highest.Amount:F2}");
        }
    }
}