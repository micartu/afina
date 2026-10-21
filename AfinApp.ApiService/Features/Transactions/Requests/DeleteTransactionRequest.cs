using Mediator;
using AfinApp.Data.Shared;

namespace AfinApp.ApiService.Features.Transactions.Requests;

public record DeleteTransactionRequest(Guid Id) : IRequest<Result>;
