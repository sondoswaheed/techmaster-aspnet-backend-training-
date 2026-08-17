using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01_Drills.Drills
{
    internal class LoginValidator03
    {

        public static void validator()
        {
            const string username = "SondosWaheed";
            const string Password = "Sondos12345";
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"the {i + 1} attempt:");
                Console.WriteLine($"Enter the username:");
                string user = Console.ReadLine();
                Console.WriteLine($"Enter the password:");
                string pass = Console.ReadLine();
                if (string.Equals(username, user, StringComparison.OrdinalIgnoreCase) &&
                    (Password == pass))
                {
                    Console.WriteLine("Loin successful");
                    return;
                }
            }
            Console.WriteLine("Account locked. Too many failed attempts.");
        }


    }
}
