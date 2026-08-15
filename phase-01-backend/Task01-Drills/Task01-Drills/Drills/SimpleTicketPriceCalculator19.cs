using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01_Drills.Drills
{
    internal class SimpleTicketPriceCalculator19
    {
        public static void Calculate()
        {
            decimal basePrice = 100;
            decimal discount = 0;

            Console.Write("Enter your age: ");

            if (!int.TryParse(Console.ReadLine(), out int age) || age < 0)
            {
                Console.WriteLine("Invalid age.");
                return;
            }

            Console.Write("Are you a student? (yes/no): ");
            string? studentInput = Console.ReadLine();

            bool isStudent = studentInput?.Equals("yes", StringComparison.OrdinalIgnoreCase) == true;

            if (age < 12)
            {
                discount = Math.Max(discount, 0.50m);
            }

            if (age > 60)
            {
                discount = Math.Max(discount, 0.30m);
            }

            if (isStudent)
            {
                discount = Math.Max(discount, 0.20m);
            }

            decimal finalPrice = basePrice * (1 - discount);

            Console.WriteLine($"Final ticket price: {finalPrice:F2}");
        }
    }
}
