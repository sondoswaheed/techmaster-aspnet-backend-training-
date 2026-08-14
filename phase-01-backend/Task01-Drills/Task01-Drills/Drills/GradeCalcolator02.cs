using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01_Drills.Drills
{
    internal class GradeCalcolator02
    {
        public static void calc()
        {
            Console.WriteLine("Enter your grade:");
            bool n = int.TryParse(Console.ReadLine(), out int num);
            if (n)
            {
                if (num > 100 || num < 0)
                {
                    Console.WriteLine("Score must be between 0 and 100.");
                }
                else if (num >= 0 && num < 60)
                {
                    Console.WriteLine("Grade: F");
                }
                else if (num >= 60 && num < 70)
                {
                    Console.WriteLine("Grade: D");
                }
                else if (num >= 70 && num < 80)
                {
                    Console.WriteLine("Grade: C");
                }
                else if (num >= 80 && num < 90)
                {
                    Console.WriteLine("Grade: B");
                }
                else if (num >= 90 && num <= 100)
                {
                    Console.WriteLine("Grade: A");
                }

            }
            else
            {
                Console.WriteLine("Invalid input");
            }
        }
    }
}
