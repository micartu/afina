using Mediator;
using AfinApp.Data.Context;
using Microsoft.EntityFrameworkCore;
using AfinApp.ApiService.Features.Transactions.Requests;

namespace AfinApp.ApiService.Features.Transactions;

public class GetAllTransactionsHandler(ITransactionRelevantContext ctx) : IRequestHandler<GetAllTransactionsRequest, IEnumerable<TransactionResponse>>
{
    public async ValueTask<IEnumerable<TransactionResponse>> Handle(GetAllTransactionsRequest req, CancellationToken ct)
        => await ctx.Transactions.Select(x => x.ConvertToResponse()).ToListAsync(ct);
}
