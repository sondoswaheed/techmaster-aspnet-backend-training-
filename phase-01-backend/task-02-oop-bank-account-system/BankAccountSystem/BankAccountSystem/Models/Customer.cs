namespace BankAccountSystem.Models;

public class Customer
{
    public int CustomerId { get; set; }

    public string FullName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public DateTime CreatedAt { get; set; }
}