using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01_Drills.Drills
{
    internal class WordCounter06
    {
        public static void Counter()
        {
            Console.Write("Enter a sentence: ");
            string? sentence = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(sentence))
            {
                Console.WriteLine("Sentence cannot be empty.");
                return;
            }

            sentence = sentence.Trim();

            string[] words = sentence.Split(' ',StringSplitOptions.RemoveEmptyEntries);

            Console.WriteLine($"Word count: {words.Length}");
        }
    }
}
