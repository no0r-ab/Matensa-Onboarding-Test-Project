using Microsoft.AspNetCore.Identity;

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


    private User(Guid id, string firstName, string lastName, string email, string phoneNumber, double balance, DateTime dateOfBirth)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Balance = balance;
        DateOfBirth = dateOfBirth;
    }

    private User(string firstName, string lastName, string email, string phoneNumber, double balance, DateTime dateOfBirth)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Balance = balance;
        DateOfBirth = dateOfBirth;
    }

    public static User Create(string firstName, string lastName, string email, string phoneNumber, double balance, DateTime dateOfBirth)
    {
        return new(Guid.NewGuid(), firstName, lastName, email, phoneNumber, balance, dateOfBirth);
    }

    public static User Update(string firstName, string lastName, string email, string phoneNumber, double balance, DateTime dateOfBirth)
    {
        return new(firstName, lastName, email, phoneNumber, balance, dateOfBirth);
    }

    public void SetPassword(string hashedPassword)
    {
        Password = hashedPassword;
    }

    public void AddAmount(double amount)
    {
        Balance += amount;
    }
}
