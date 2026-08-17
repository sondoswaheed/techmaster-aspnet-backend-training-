using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01_Drills.Drills
{
    internal class NameFormatter07
    {
        public static void Formatter()
        {
            Console.Write("Enter your full name: ");
            string? name = Console.ReadLine();

            // per empty string
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty.");
                return;
            }

            // trim=> to remove extra spaces  , split => to split the string when have a space
            string[] parts = name.Trim().Split(' ',StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < parts.Length; i++)
            {
                // make all string lower
                parts[i] = parts[i].ToLower();

                // make the first char upper 
                parts[i] = char.ToUpper(parts[i][0]) + parts[i].Substring(1);
            }

            // the final formatted name
            string formattedName = string.Join(" ", parts);

            Console.WriteLine($"Formatted name: {formattedName}");
        }
    }
}
