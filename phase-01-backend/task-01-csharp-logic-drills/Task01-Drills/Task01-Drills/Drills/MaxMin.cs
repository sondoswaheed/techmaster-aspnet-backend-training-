using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01_Drills.Drills
{
    internal class MaxMin
    {
        public static void Finder()
        {
            List<int> numbers = new();

            while (true)
            {
                Console.Write("Enter number: ");

                if (int.TryParse(Console.ReadLine(), out int number))
                {
                    numbers.Add(number);
                }
                else
                {

                    Console.WriteLine("Invalid input. Stopping input...");
                    break;
                }
            }
            if (numbers.Count == 0)
            {
                Console.WriteLine("the list is empty");
                return;
            }
            int Min = numbers[0];
            int max = numbers[0];
            for (int i = 1; i < numbers.Count; i++)
            {
                if (numbers[i] > max)
                {
                    max = numbers[i];
                }
                if (numbers[i] < Min)
                {
                    Min = numbers[i];
                }
            }
            Console.WriteLine($"Max :{max} | Min : {Min}");
            int linqMax = numbers.Max();
            int linqMin = numbers.Min();

            Console.WriteLine($"LINQ Max: {linqMax} | LINQ Min: {linqMin}");
        }
    }
}
