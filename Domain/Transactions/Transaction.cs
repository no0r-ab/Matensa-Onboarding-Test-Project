namespace Domain.Transactions;

public class Transaction : Entity
{
    public Guid SenderId { get; set; }
    public Guid ReceiverId { get; set; }
    public double Amount { get; set; }
    public TransactionType TransactionType { get; set; }

    private Transaction(Guid id, Guid senderId, Guid receiverId, double amount, TransactionType transactionType)
    {
        Id = id;
        SenderId = senderId;
        ReceiverId = receiverId;
        Amount = amount;
        TransactionType = transactionType;
    }

    private Transaction(Guid senderId, Guid receiverId, double amount, TransactionType transactionType)
    {
        SenderId = senderId;
        ReceiverId = receiverId;
        Amount = amount;
        TransactionType = transactionType;
    }

    public static Transaction Create(Guid senderId, Guid receiverId, double amount, TransactionType transactionType)
    {
        return new(Guid.NewGuid(), senderId, receiverId, amount, transactionType);
    }

    public void SetTransactionType(TransactionType type)
    {
        TransactionType = type;
    }
}