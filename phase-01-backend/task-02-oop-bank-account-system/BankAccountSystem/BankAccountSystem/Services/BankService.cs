using BankAccountSystem.Models;

namespace BankAccountSystem.Services
{
    public class BankService
    {
        private readonly List<BankAccount> _accounts = new();
        public bool CreateAccount(string fullname, string email, string phonenumber, decimal initialbalance, AccountType accountType, out string message)
        {
            if (string.IsNullOrWhiteSpace(fullname))
            {
                message = "Customer name is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                message = "Email is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(phonenumber))
            {
                message = "Phone number is required.";
                return false;
            }

            Customer customer = new Customer
            {
                FullName = fullname,
                CustomerId = _accounts.Count + 1,
                Email = email,
                PhoneNumber = phonenumber,
                CreatedAt = DateTime.Now
            };

            string accountNumber = GenerateAccountNumber();

            BankAccount account = new BankAccount
            {
                AccountNumber = accountNumber,
                Customer = customer,
                AccountType = accountType,
                CreatedAt = DateTime.Now,
                IsActive = true
            };


            if (initialbalance < 0)
            {
                message = "Initial balance cannot be negative.";
                return false;
            }
            
                account.Deposit(initialbalance);

                account.Transactions.Add(new Transaction
                {
                    TransactionId = 1,
                    AccountNumber = account.AccountNumber,
                    TransactionType = TransactionType.Deposit,
                    Amount = initialbalance,
                    TransactionDate = DateTime.Now,
                    Description = "Initial deposit",
                    BalanceAfterTransaction = account.Balance
                });
            

            _accounts.Add(account);

            message = $"Account created successfully. Account Number: {accountNumber}";
            return true;
        }

        public bool deposite(string accountNumber , decimal amount , out string message)
        {
            BankAccount? account =FindAccount(accountNumber);

            if (account == null)
            {
                message = "Account not found.";
                return false;
            }

            if (amount <= 0)
            {
                message = "Deposit amount must be greater than zero.";
                return false;
            }

            if (!account.Deposit(amount))
            {
                message = "Deposit failed.";
                return false;
            }

            account.Transactions.Add(new Transaction
            {
                TransactionId = account.Transactions.Count + 1,
                TransactionDate= DateTime.Now,
                TransactionType= TransactionType.Deposit,
                Amount = amount,    
                AccountNumber= accountNumber,
                Description= "Money deposited",
                BalanceAfterTransaction= account.Balance
            });
            message = $"Deposit successful. New balance: {account.Balance:F2}";
            return true;
        }

        public bool withdraw(string accountNumber , decimal amount, out string message)
        {
            BankAccount? account = FindAccount(accountNumber);

            if (account == null)
            {
                message = "Account not found.";
                return false;
            }

            if (amount <= 0)
            {
                message = "Withdrawal amount must be greater than zero.";
                return false;
            }

            if (amount > account.Balance)
            {
                message = "Insufficient balance.";
                return false;
            }

            if (!account.Withdraw(amount))
            {
                message = "Withdrawal failed.";
                return false;
            }

            account.Transactions.Add(new Transaction
            {
                TransactionId = account.Transactions.Count + 1,
                AccountNumber = account.AccountNumber,
                TransactionType = TransactionType.Withdrawal,
                Amount = amount,
                TransactionDate = DateTime.Now,
                Description = "Money withdrawn",
                BalanceAfterTransaction = account.Balance
            });

            message = $"Withdrawal successful. New balance: {account.Balance:F2}";
            return true;
        }

        public bool Transfer(string fromAccountNumber, string toAccountNumber, decimal amount, out string message)
        {
            BankAccount? sender = FindAccount(fromAccountNumber);
            BankAccount? receiver = FindAccount(toAccountNumber);

            if (sender == null)
            {
                message = "Sender account not found.";
                return false;
            }

            if (receiver == null)
            {
                message = "Receiver account not found.";
                return false;
            }

            if (fromAccountNumber == toAccountNumber)
            {
                message = "Cannot transfer to the same account.";
                return false;
            }

            if (amount <= 0)
            {
                message = "Transfer amount must be greater than zero.";
                return false;
            }

            if (amount > sender.Balance)
            {
                message = "Insufficient balance.";
                return false;
            }

            if (!sender.Withdraw(amount))
            {
                message = "Transfer failed - balance of sender less than the amount .";
                return false;
            }
          

            if (!receiver.Deposit(amount))
            {
                message = "Transfer failed.";
                return false;
            }
            sender.Transactions.Add(new Transaction
        {
            TransactionId = sender.Transactions.Count + 1,
            AccountNumber = sender.AccountNumber,
            TransactionType = TransactionType.Transfer,
            Amount = amount,
            TransactionDate = DateTime.Now,
            Description = $"Transfer to {receiver.AccountNumber}",
            BalanceAfterTransaction = sender.Balance
    });

        receiver.Transactions.Add(new Transaction
        {
            TransactionId = receiver.Transactions.Count + 1,
            AccountNumber = receiver.AccountNumber,
            TransactionType = TransactionType.Transfer,
            Amount = amount,
            TransactionDate = DateTime.Now,
            Description = $"Transfer from {sender.AccountNumber}",
            BalanceAfterTransaction = receiver.Balance
        });
            message = "Transfer completed successfully.";
            return true;
        }

        public BankAccount? GetAccountDetails( string accountnumber){
            return FindAccount(accountnumber);
        }

        public List<Transaction> GetTransactionHistory(string accountnumber)
        {
            BankAccount? account = FindAccount(accountnumber);
            if (account == null)
            {
                return new List<Transaction>();
            }

            return account.Transactions.OrderByDescending(t => t.TransactionDate).ToList();
        }

        public BankAccount? FindAccount(string accountnum)
        {
            return _accounts.FirstOrDefault(a => a.AccountNumber.Equals(accountnum, StringComparison.OrdinalIgnoreCase));
        }
        public List<BankAccount> GetAllAccounts()
        {
            return _accounts;
        }

        private string GenerateAccountNumber()
        {
            int nextNumber = _accounts.Count + 1001;

            string accountNumber = $"ACC{nextNumber}";

            while (FindAccount(accountNumber) != null)
            {
                nextNumber++;
                accountNumber = $"ACC{nextNumber}";
            }

            return accountNumber;
        }

    }
}

