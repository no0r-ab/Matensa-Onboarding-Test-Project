using MediatR;
using SharedKernel.Result;

namespace Application.Services.Transactions.Create;

public record CreateTransactionCommand
(
    Guid ReceiverId,
    double Amount
    ) : IRequest<Result<TransactionResult>>;