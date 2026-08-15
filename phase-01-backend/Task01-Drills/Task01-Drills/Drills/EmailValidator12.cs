using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01_Drills.Drills
{
        internal class EmailValidator12
        {
            public static void Validate()
            {
                Console.Write("Enter your email: ");
                string? email = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(email))
                {
                    Console.WriteLine("Email cannot be empty.");
                    return;
                }

                if (email.Contains(" "))
                {
                    Console.WriteLine("Invalid email: Email cannot contain spaces.");
                    return;
                }

                if (!email.Contains("@"))
                {
                    Console.WriteLine("Invalid email: Email must contain @.");
                    return;
                }

                if (!email.Contains("."))
                {
                    Console.WriteLine("Invalid email: Email must contain a dot.");
                    return;
                }

                if (email.StartsWith("@") || email.EndsWith("@"))
                {
                    Console.WriteLine("Invalid email: Email cannot start or end with @.");
                    return;
                }

                Console.WriteLine("Valid email.");
            }
        }
    }