using Domain.Transactions;

namespace Application.Services.Users;

public record UserResult(
    Guid Id,
    string FullName,
    string Email,
    string PhoneNumber,
    double Balance,
    DateTime DateOfBirth,
    bool IsAdmin,
    ICollection<Transaction> SentTransactions,
    ICollection<Transaction> ReceivedTransactions
    );

