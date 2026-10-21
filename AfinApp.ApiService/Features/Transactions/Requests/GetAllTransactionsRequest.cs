using Mediator;

namespace AfinApp.ApiService.Features.Transactions.Requests;

public record GetAllTransactionsRequest() : IRequest<IEnumerable<TransactionResponse>>;
