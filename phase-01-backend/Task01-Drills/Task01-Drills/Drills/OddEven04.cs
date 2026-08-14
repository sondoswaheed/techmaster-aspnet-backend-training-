using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01_Drills.Drills
{
    internal class OddEven04
    {
        public static void Analyzer()
        {

            int num;
            Console.WriteLine("Enter the size of list :");
            int count = int.Parse(Console.ReadLine());
            while (count <= 0)
            {
                Console.WriteLine("Enter the valid size number");
                count = int.Parse(Console.ReadLine());
            }
            List<int> Even = new List<int>();
            List<int> Odd = new List<int>();
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine("Enter the number: ");
                num = int.Parse(Console.ReadLine());
                while (num < 0)
                {
                    Console.WriteLine("Enter the number >= 0:");
                    num = int.Parse(Console.ReadLine());
                }
                if (num % 2 == 0)
                {
                    Even.Add(num);
                }
                else
                {
                    Odd.Add(num);
                }
            }
            if (Even.Count == 0)
                Console.WriteLine("Even list should be empty");
            else if (Odd.Count == 0)
                Console.WriteLine("Odd list should be empty");
            else
            {
                Console.WriteLine($"Even: {string.Join(", ", Even)} | Odd: {string.Join(", ", Odd)} ");
            }
        }
    }

}
