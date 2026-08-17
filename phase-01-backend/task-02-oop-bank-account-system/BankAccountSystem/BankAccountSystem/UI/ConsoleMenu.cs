using BankAccountSystem.Models;
using BankAccountSystem.Services;

namespace BankAccountSystem.UI
{
    public class ConsoleMenu
    {

        private readonly BankService _bankService;

        public ConsoleMenu(BankService bankService)
        {
            _bankService=bankService;
        }

        public void Run()
        {
            bool running = true;

            while (running)
            {
                DisplayMenu();

                Console.Write("Choose an option: ");
                string? choice = Console.ReadLine();

                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        CreateAccount();
                        break;

                    case "2":
                        DepositMoney();
                        break;

                    case "3":
                        WithdrawMoney();
                        break;

                    case "4":
                        TransferMoney();
                        break;

                    case "5":
                        ViewAccountDetails();
                        break;

                    case "6":
                        ViewTransactionHistory();
                        break;

                    case "7":
                        ViewAllAccounts();
                        break;

                    case "8":
                        running = false;
                        Console.WriteLine("Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }

                if (running)
                {
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
        private void DisplayMenu()
        {
            Console.WriteLine("======================================");
            Console.WriteLine("       TechMaster Bank System");
            Console.WriteLine("======================================");
            Console.WriteLine("1. Create Customer Account");
            Console.WriteLine("2. Deposit Money");
            Console.WriteLine("3. Withdraw Money");
            Console.WriteLine("4. Transfer Money");
            Console.WriteLine("5. View Account Details");
            Console.WriteLine("6. View Transaction History");
            Console.WriteLine("7. View All Accounts");
            Console.WriteLine("8. Exit");
            Console.WriteLine("======================================");
        }

        private void CreateAccount()
        {
            Console.WriteLine("======= Create account ========");
            Console.WriteLine("FullName:");
            string fullName = Console.ReadLine() ?? "";
            Console.WriteLine("Email:");
            string Email = Console.ReadLine() ?? "";
            Console.WriteLine("InitialBalance:");
            decimal.TryParse(Console.ReadLine()  , out decimal InitialBalance);
            Console.WriteLine("Phone:");
            string Phone = Console.ReadLine() ?? "";

            Console.WriteLine("AccountType:");
            Console.WriteLine("1.Saving");
            Console.WriteLine("2.Current");
            int.TryParse(Console.ReadLine() , out int type);

            AccountType accountType;
            if (type == 1)
            {
                accountType = AccountType.Savings;
            }
            else if (type == 2)
            {
                accountType = AccountType.Current;
            }
            else {
                Console.WriteLine("no Account tyd ");
                return;
            }

            _bankService.CreateAccount(fullName, Email, Phone, InitialBalance, accountType,out string message);

            Console.WriteLine(message);

        }

        private void DepositMoney()
        {
            Console.WriteLine("======= Deposit Money ========");
            Console.Write("AccountNumber: ");
            var AccNum=Console.ReadLine() ?? "";

            Console.Write("\n Amount:");
            decimal.TryParse(Console.ReadLine(), out decimal amount);
            _bankService.deposite(AccNum, amount, out string message);

            Console.WriteLine(message);
        }

        private void WithdrawMoney()
        {
            Console.WriteLine("===== Withdraw Money =====");

            Console.Write("Account Number: ");
            string accountNumber = Console.ReadLine() ?? "";

            Console.Write("Amount: ");
            decimal.TryParse(Console.ReadLine(), out decimal amount);

             _bankService.withdraw( accountNumber,amount,out string message);

            Console.WriteLine(message);
        }
        private void TransferMoney()
        {
            Console.WriteLine("===== Transfer Money =====");

            Console.Write("From Account: ");
            string fromAccountNumber = Console.ReadLine() ?? "";

            Console.Write("To Account: ");
            string toAccountNumber = Console.ReadLine() ?? "";

            Console.Write("Amount: ");
            decimal.TryParse(Console.ReadLine(), out decimal amount);

            _bankService.Transfer(fromAccountNumber,toAccountNumber,amount,out string message);

            Console.WriteLine(message);
        }

        private void ViewAccountDetails()
        {
            Console.WriteLine("===== Account Details =====");

            Console.Write("Account Number: ");
            string accountNumber = Console.ReadLine() ?? "";

            BankAccount? account =
                _bankService.GetAccountDetails(accountNumber);

            if (account == null)
            {
                Console.WriteLine("Account not found.");
                return;
            }

            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"Account Number : {account.AccountNumber}");
            Console.WriteLine($"Customer Name  : {account.Customer.FullName}");
            Console.WriteLine($"Email          : {account.Customer.Email}");
            Console.WriteLine($"Phone          : {account.Customer.PhoneNumber}");
            Console.WriteLine($"Account Type   : {account.AccountType}");
            Console.WriteLine($"Balance        : {account.Balance:F2}");
            Console.WriteLine($"Created Date   : {account.CreatedAt}");
            Console.WriteLine($"Status         : {(account.IsActive ? "Active" : "Inactive")}");
            Console.WriteLine("----------------------------------------");
        }


        private void ViewTransactionHistory()
        {
            Console.WriteLine("===== Transaction History =====");

            Console.Write("Account Number: ");
            string accountNumber = Console.ReadLine() ?? "";

            BankAccount? account =_bankService.FindAccount(accountNumber);

            if (account == null)
            {
                Console.WriteLine("Account not found.");
                return;
            }

            List<Transaction> transactions =
                _bankService.GetTransactionHistory(accountNumber);

            if (transactions.Count == 0)
            {
                Console.WriteLine("No transactions found for this account.");
                return;
            }

            Console.WriteLine();

            foreach (Transaction transaction in transactions)
            {
                Console.WriteLine("----------------------------------------");
                Console.WriteLine($"Type        : {transaction.TransactionType}");
                Console.WriteLine($"Amount      : {transaction.Amount:F2}");
                Console.WriteLine($"Date        : {transaction.TransactionDate}");
                Console.WriteLine($"Description : {transaction.Description}");
                Console.WriteLine($"Balance     : {transaction.BalanceAfterTransaction:F2}");
            }

            Console.WriteLine("----------------------------------------");
        }


        private void ViewAllAccounts()
        {
            Console.WriteLine("===== All Accounts =====");

            List<BankAccount> accounts =_bankService.GetAllAccounts();

            if (accounts.Count == 0)
            {
                Console.WriteLine("No accounts found.");
                return;
            }

            Console.WriteLine(
                $"{"Account Number",-18}" + $"{"Customer Name",-20}" +
                $"{"Type",-12}" + $"{"Balance",-12}" + $"Status");

            Console.WriteLine("----------------------------------------------------------------");

            foreach (BankAccount account in accounts)
            {
                Console.WriteLine(
                    // 18 reserved 18 char 
                    $"{account.AccountNumber,-18}" +
                    $"{account.Customer.FullName,-20}" +
                    $"{account.AccountType,-12}" +
                    $"{account.Balance,-12:F2}" +
                    $"{(account.IsActive ? "Active" : "Inactive")}");
            }
        }
    }

}
