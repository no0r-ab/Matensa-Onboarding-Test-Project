namespace Presentation.Contracts.Transaction;

public record CreateTransactionRequest(Guid ReceiverId, double Amount);