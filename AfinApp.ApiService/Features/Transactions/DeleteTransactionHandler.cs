using Mediator;
using AfinApp.Data.Shared;
using AfinApp.Data.Context;
using AfinApp.ApiService.Features.Transactions.Requests;

namespace AfinApp.ApiService.Features.Transactions;

public class DeleteTransactionHandler(ITransactionRelevantContext ctx) : IRequestHandler<DeleteTransactionRequest, Result>
{
    public async ValueTask<Result> Handle(DeleteTransactionRequest req, CancellationToken ct)
    {
        var item = await ctx.Transactions.FindAsync([req.Id], ct);
        if (item == null)
            return new Error(ErrorType.NotFound, $"{Const.apiTransactions}.NotFound", "Not found");
        ctx.Transactions.Remove(item);
        await ctx.SaveChangesAsync(ct);
        return Result.Success();
    }
}