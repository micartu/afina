using Mediator;
using AfinApp.Data.Shared;
using AfinApp.Data.Context;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using AfinApp.ApiService.Features.Transactions.Requests;

namespace AfinApp.ApiService.Features.Transactions;

public class GetTransactionByIdHandler(ITransactionRelevantContext ctx) : IRequestHandler<GetTransactionByIdRequest, Result<TransactionResponse>>
{
    public async ValueTask<Result<TransactionResponse>> Handle(GetTransactionByIdRequest req, CancellationToken ct)
    {
        var item = await ctx.Transactions
            .Where(x => x.Id == req.Id)
            .Select(x => x.ConvertToResponse())
            .FirstOrDefaultAsync(ct);
        return item != null
            ? item
            : new Error(ErrorType.NotFound, $"{Const.apiTransactions}.NotFound", "Not found");
    }
}