using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01_Drills.Drills
{
    internal class PasswordStrengthChecker08
    {
        public static void Checker()
        {
            Console.Write("Enter your password: ");
            string? password = Console.ReadLine();

            if (string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Password cannot be empty.");
                return;
            }

            bool hasUppercase = false;
            bool hasLowercase = false;
            bool hasDigit = false;
            bool hasSpecial = false;

            foreach (char c in password)
            {
                if (char.IsUpper(c))
                {
                    hasUppercase = true;
                }
                else if (char.IsLower(c))
                {
                    hasLowercase = true;
                }
                else if (char.IsDigit(c))
                {
                    hasDigit = true;
                }
                else
                {
                    hasSpecial = true;
                }
            }

            List<string> missingRules = new();

            if (password.Length < 8)
            {
                missingRules.Add("at least 8 characters");
            }

            if (!hasUppercase)
            {
                missingRules.Add("uppercase letter");
            }

            if (!hasLowercase)
            {
                missingRules.Add("lowercase letter");
            }

            if (!hasDigit)
            {
                missingRules.Add("digit");
            }

            if (!hasSpecial)
            {
                missingRules.Add("special character");
            }

            if (missingRules.Count == 0)
            {
                Console.WriteLine("Strong password.");
            }
            else
            {
                Console.WriteLine(
                    $"Weak password - missing: {string.Join(", ", missingRules)}"
                );
            }
        }
    }
}
