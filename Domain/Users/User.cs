using Domain.Transactions;

namespace Domain.Users;

public class User : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Password { get; set; }
    public double Balance { get; set; }
    public bool IsAdmin { get; set; }

    public ICollection<Transaction> SentTransactions { get; set; } // Transactions sent by the user
    public ICollection<Transaction> ReceivedTransactions { get; set; } // Transactions received by the user


    private User(Guid id, string firstName, string lastName, string email, string phoneNumber, double balance, DateTime dateOfBirth, bool isAdmin)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Balance = balance;
        DateOfBirth = dateOfBirth;
        IsAdmin = isAdmin;
    }

    private User(string firstName, string lastName, string email, string phoneNumber, double balance, DateTime dateOfBirth, bool isAdmin)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Balance = balance;
        DateOfBirth = dateOfBirth;
        IsAdmin = isAdmin;
    }

    public static User Create(string firstName, string lastName, string email, string phoneNumber, double balance, DateTime dateOfBirth, bool isAdmin)
    {
        return new(Guid.NewGuid(), firstName, lastName, email, phoneNumber, balance, dateOfBirth, isAdmin);
    }

    public static User Update(string firstName, string lastName, string email, string phoneNumber, double balance, DateTime dateOfBirth, bool isAdmin)
    {
        return new(firstName, lastName, email, phoneNumber, balance, dateOfBirth, isAdmin);
    }

    public void SetPassword(string hashedPassword)
    {
        Password = hashedPassword;
    }

    public void AddAmount(double amount)
    {
        Balance += amount;
    }

    public void RemoveAmount(double amount)
    {
        Balance -= amount;
    }

}
