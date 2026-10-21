using Mediator;
using AfinApp.Data.Shared;
using AfinApp.Data.Context;
using FluentValidation;
using AfinApp.Data.Entities;
using AfinApp.ApiService.Features.Transactions.Requests;

namespace AfinApp.ApiService.Features.Transactions;

public class CreateTransactionHandler(ITransactionRelevantContext ctx, IValidator<ITransactionRequest> val) : IRequestHandler<CreateTransactionRequest, Result<TransactionResponse>>
{
    public async ValueTask<Result<TransactionResponse>> Handle(CreateTransactionRequest req, CancellationToken ct)
    {
        if (!val.Validate(req).IsValid)
            return new Error(ErrorType.Validation, Const.apiTransactions, "Validation failed");
        var item = new Transaction
        {
            Id = Guid.NewGuid(),
            AccountId = req.AccountId,
            CategoryId = req.CategoryId,
            Amount = req.Amount,
            Date = req.Date,
            Description = req.Description,
            Type = req.Type,
            TargetAccountId = req.TargetAccountId
        };
        ctx.Transactions.Add(item);
        await ctx.SaveChangesAsync(ct);
        return item.ConvertToResponse();
    }
}