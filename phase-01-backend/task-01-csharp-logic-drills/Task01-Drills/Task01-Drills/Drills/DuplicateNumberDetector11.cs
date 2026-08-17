using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01_Drills.Drills
{
        internal class DuplicateNumberDetector11
        {
            public static void Detector()
            {
                List<int> numbers = new();

                Console.WriteLine("Enter numbers one by one.");
                Console.WriteLine("Press Enter without a number to finish.");

                while (true)
                {
                    Console.Write("Enter number: ");

                    string? input = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(input))
                    {
                        break;
                    }

                    if (int.TryParse(input, out int number))
                    {
                        numbers.Add(number);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter an integer.");
                    }
                }

                if (numbers.Count == 0)
                {
                    Console.WriteLine("The list is empty.");
                    return;
                }

                HashSet<int> seen = new();
                HashSet<int> duplicates = new();

                foreach (int number in numbers)
                {
                    if (!seen.Add(number))
                    {
                        duplicates.Add(number);
                    }
                }

                if (duplicates.Count == 0)
                {
                    Console.WriteLine("No duplicates found.");
                }
                else
                {
                    Console.WriteLine($"Duplicates: {string.Join(", ", duplicates)}");
                }
            }
        }
    }