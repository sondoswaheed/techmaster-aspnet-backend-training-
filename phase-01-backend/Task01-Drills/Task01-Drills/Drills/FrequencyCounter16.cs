using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01_Drills.Drills
{
    internal class FrequencyCounter16
    {
        public static void CountFrequency()
        {
            List<int> numbers = new();

            Console.WriteLine("Enter numbers one by one.");
            Console.WriteLine("Press Enter to finish.");

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

            Dictionary<int, int> frequency = new();

            foreach (int number in numbers)
            {
                if (frequency.ContainsKey(number))
                {
                    frequency[number]++;
                }
                else
                {
                    frequency.Add(number, 1);
                }
            }

            Console.WriteLine("\nFrequency:");

            foreach (var pair in frequency)
            {
                Console.WriteLine($"{pair.Key} => {pair.Value}");
            }
        }
    }
}
