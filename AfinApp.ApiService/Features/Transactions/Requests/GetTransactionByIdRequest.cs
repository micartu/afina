using Mediator;
using AfinApp.Data.Shared;

namespace AfinApp.ApiService.Features.Transactions.Requests;

public record GetTransactionByIdRequest(Guid Id) : IRequest<Result<TransactionResponse>>;
