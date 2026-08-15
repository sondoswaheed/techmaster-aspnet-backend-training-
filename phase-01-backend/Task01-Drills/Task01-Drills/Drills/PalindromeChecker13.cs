using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01_Drills.Drills
{
    internal class PalindromeChecker13
    {
        public static void Check()
        {
            Console.Write("Enter text: ");
            string? text = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine("Text cannot be empty.");
                return;
            }

            // Clean the text and make it all  lower
            string cleanedText = text.Trim().ToLower();

            // Bonus: Ignore spaces
            cleanedText = cleanedText.Replace(" ", "");

            // Reverse the text
            char[] characters = cleanedText.ToCharArray();
            Array.Reverse(characters);

            string reversedText = new string(characters);

            // Compare original with reversed
            if (cleanedText == reversedText)
            {
                Console.WriteLine("Palindrome");
            }
            else
            {
                Console.WriteLine("Not Palindrome");
            }
        }
    }
}