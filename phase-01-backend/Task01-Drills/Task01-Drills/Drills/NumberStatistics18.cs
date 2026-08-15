using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01_Drills.Drills
{
        internal class NumberStatistics18
        {
            public static void Analyze()
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

                int sum = 0;
                int positiveCount = 0;
                int negativeCount = 0;

                int min = numbers[0];
                int max = numbers[0];

                foreach (int number in numbers)
                {
                    sum += number;

                    if (number > 0)
                    {
                        positiveCount++;
                    }
                    else if (number < 0)
                    {
                        negativeCount++;
                    }

                    if (number > max)
                    {
                        max = number;
                    }

                    if (number < min)
                    {
                        min = number;
                    }
                }

                double average = (double)sum / numbers.Count;

                Console.WriteLine("\n--- Number Statistics ---");
                Console.WriteLine($"Count: {numbers.Count}");
                Console.WriteLine($"Sum: {sum}");
                Console.WriteLine($"Average: {average:F2}");
                Console.WriteLine($"Max: {max}");
                Console.WriteLine($"Min: {min}");
                Console.WriteLine($"Positives: {positiveCount}");
                Console.WriteLine($"Negatives: {negativeCount}");
            }
        }
    }