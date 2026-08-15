using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01_Drills.Drills
{
    internal class ArrayRotation15
    {
        public static void Rotate()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };

            if (numbers.Length == 0)
            {
                Console.WriteLine("Array is empty.");
                return;
            }

            //store the last element in temp 
            int temp = numbers[numbers.Length - 1];

            // shift elements right
            for (int i = numbers.Length - 1; i >= 1; i--)
            {
                numbers[i] = numbers[i - 1];
            }

            //make the last element in the first position
            numbers[0] = temp;

            Console.WriteLine($"Rotated array: [{string.Join(", ", numbers)}]");
        }
    }
}