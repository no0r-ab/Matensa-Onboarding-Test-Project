using Domain.Transactions;

namespace Presentation.Contracts.Users;

public record UserResponse(
    Guid Id,
    string FullName,
    string Email,
    string PhoneNumber,
    DateTime DateOfBirth,
    List<Domain.Transactions.Transaction> Transactions);
