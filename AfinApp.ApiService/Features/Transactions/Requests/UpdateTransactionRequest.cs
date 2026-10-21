using Mediator;
using AfinApp.Data.Shared;
using AfinApp.Data.Entities;

namespace AfinApp.ApiService.Features.Transactions.Requests;

public record UpdateTransactionRequest(Guid Id, Guid AccountId, Guid? CategoryId, decimal Amount, DateTime Date, string Description, TransactionType Type, Guid? TargetAccountId) : ITransactionRequest, IRequest<Result<TransactionResponse>>;
