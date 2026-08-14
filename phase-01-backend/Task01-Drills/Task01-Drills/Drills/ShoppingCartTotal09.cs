using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01_Drills.Drills
{
    internal class ShoppingCartTotal09
    {
        public static void Calculate()
        {
            Console.Write("Enter number of items: ");

            if (!int.TryParse(Console.ReadLine(), out int itemCount) || itemCount <= 0)
            {
                Console.WriteLine("Invalid number of items.");
                return;
            }

            decimal total = 0;

            for (int i = 1; i <= itemCount; i++)
            {
                Console.WriteLine($"\nItem {i}");

                Console.Write("Enter price: ");

                if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price <= 0)
                {
                    Console.WriteLine("Invalid price.");
                    return;
                }

                Console.Write("Enter quantity: ");

                if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
                {
                    Console.WriteLine("Invalid quantity.");
                    return;
                }

                decimal subtotal = price * quantity;
                total += subtotal;

                Console.WriteLine($"Item subtotal: {subtotal:F2}");
            }

            decimal discount = 0;

            if (total > 1000)
            {
                discount = total * 0.10m;
            }

            decimal finalTotal = total - discount;

            Console.WriteLine($"\nTotal: {total:F2}");
            Console.WriteLine($"Discount: {discount:F2}");
            Console.WriteLine($"Final total: {finalTotal:F2}");
        }
    }
}