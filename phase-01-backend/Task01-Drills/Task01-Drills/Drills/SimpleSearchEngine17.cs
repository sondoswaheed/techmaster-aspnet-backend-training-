using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01_Drills.Drills
{
    internal class SimpleSearchEngine17
    {
        public static void Search()
        {
            List<string> names = new()
            {
                "Ali Hassan",
                "Farida Ali",
                "Sara Ahmed",
                "hamza Ayman",
                "Hassan Adel"
            };

            Console.Write("Enter search keyword: ");
            string? keyword = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                Console.WriteLine("Search keyword cannot be empty.");
                return;
            }

            bool found = false;

            foreach (string name in names)
            {
                if (name.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(name);
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("No results found.");
            }
        }
    }
}
