using Mediator;
using AfinApp.Data.Shared;
using AfinApp.Data.Context;
using FluentValidation;
using AfinApp.ApiService.Features.Transactions.Requests;

namespace AfinApp.ApiService.Features.Transactions;

public class UpdateTransactionHandler(ITransactionRelevantContext ctx, IValidator<ITransactionRequest> val) : IRequestHandler<UpdateTransactionRequest, Result<TransactionResponse>>
{
    public async ValueTask<Result<TransactionResponse>> Handle(UpdateTransactionRequest req, CancellationToken ct)
    {
        if (!val.Validate(req).IsValid)
            return new Error(ErrorType.Validation, Const.apiTransactions, "Validation failed");
        var item = await ctx.Transactions.FindAsync([req.Id], ct);
        if (item == null)
            return new Error(ErrorType.NotFound, $"{Const.apiTransactions}.NotFound", "Not found");
        item.AccountId = req.AccountId;
        item.CategoryId = req.CategoryId;
        item.Amount = req.Amount;
        item.Date = req.Date;
        item.Description = req.Description;
        item.Type = req.Type;
        item.TargetAccountId = req.TargetAccountId;
        await ctx.SaveChangesAsync(ct);
        return item.ConvertToResponse();
    }
}