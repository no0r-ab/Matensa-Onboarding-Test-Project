using Application.Common.Interfaces;
using Domain.Transactions;
using MediatR;
using Microsoft.AspNetCore.Http;
using SharedKernel.Result;
using System.Security.Claims;
using UserDomain = Domain.Users.User;
namespace Application.Services.Transactions.Create;

internal class CreateTransactionHandler : IRequestHandler<CreateTransactionCommand, Result<TransactionResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateTransactionHandler(IUserRepository userRepository, ITransactionRepository transactionRepository, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _transactionRepository = transactionRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<TransactionResult>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        var sender = await _userRepository.Get(Guid.Parse(userId));

        if (sender == null)
            return Result<TransactionResult>.Failure(Error.NotFound("404", "User Not Found"));

        var reciever = await _userRepository.Get(request.ReceiverId);

        if (reciever == null)
            return Result<TransactionResult>.Failure(Error.NotFound("404", "Reciver User Not Found"));

        if (sender.Balance < request.Amount)
            return Result<TransactionResult>.Failure(Error.Validation("Insufficient balance", "Insufficient balance"));

        sender.RemoveAmount(request.Amount);
        var withdrawTransaction = Transaction.Create(sender.Id, request.ReceiverId, request.Amount, TransactionType.Withdrawal);

        reciever.AddAmount(request.Amount);
        var depositTransaction = Transaction.Create(sender.Id, request.ReceiverId, request.Amount, TransactionType.Deposit);

        await _userRepository.UpdateList(new List<UserDomain> { sender, reciever });
        await _transactionRepository.AddList(new List<Transaction> { withdrawTransaction, depositTransaction });

        return Result<TransactionResult>.Success(new TransactionResult(
           Id: reciever.Id,
           Balance: reciever.Balance
        ));
    }
}