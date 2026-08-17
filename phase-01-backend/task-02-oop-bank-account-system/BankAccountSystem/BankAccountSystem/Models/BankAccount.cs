namespace BankAccountSystem.Models;

public class BankAccount
{
    public string AccountNumber { get; set; }

    public Customer Customer { get; set; }

    // prevent any change on balance protected it and make a encapsulation
    public decimal Balance { get; private set; }

    public AccountType AccountType { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsActive { get; set; }

    public List<Transaction> Transactions { get; set; } = new();

    public bool Withdraw(decimal amount) {
        if(Balance<=0 || !IsActive || amount > Balance)
            return false;
        Balance-=amount;
        return true;
    }

    public bool Deposit(decimal amount)
    {
        if (amount <= 0 || !IsActive)
            return false;
        Balance+=amount;
        return true;
    }


}