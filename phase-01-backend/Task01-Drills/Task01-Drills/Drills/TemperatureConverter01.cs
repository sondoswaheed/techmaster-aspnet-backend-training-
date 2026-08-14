using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01_Drills.Drills
{
    internal class TemperatureConverter01
    {
        public void Converter()
        {
            Console.WriteLine("Enter the celsius value: ");

            string? value = Console.ReadLine();

            if (decimal.TryParse(value, out decimal val))
            {
                decimal Fahrenheit = val * 9 / 5 + 32;
                Console.WriteLine($"the converted value {Fahrenheit:F2}");
            }
            else
            {
                Console.WriteLine("Invalid tempreture value ");
            }
        }
    }

}
