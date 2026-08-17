using BankAccountSystem.Services;
using BankAccountSystem.UI;

namespace BankAccountSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BankService bankService = new BankService();

            ConsoleMenu menu = new ConsoleMenu(bankService);

            menu.Run();
        }
    }
}