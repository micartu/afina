using Mediator;
using AfinApp.Data.Shared;
using AfinApp.Data.Context;
using FluentValidation;
using AfinApp.ApiService.Features.Accounts.Requests;

namespace AfinApp.ApiService.Features.Accounts;

public class UpdateAccountHandler(IAccountRelevantContext ctx, IValidator<IAccountRequest> val) : IRequestHandler<UpdateAccountRequest, Result<AccountResponse>>
{
    public async ValueTask<Result<AccountResponse>> Handle(UpdateAccountRequest req, CancellationToken ct)
    {
        if (!val.Validate(req).IsValid)
            return new Error(ErrorType.Validation, Const.apiAccounts, "Validation failed");
        var item = await ctx.Accounts.FindAsync([req.Id], ct);
        if (item == null)
            return new Error(ErrorType.NotFound, $"{Const.apiAccounts}.NotFound", "Not found");
        item.FamilyId = req.FamilyId;
        item.Name = req.Name;
        item.Type = req.Type;
        item.Currency = req.Currency;
        item.InitialBalance = req.InitialBalance;
        await ctx.SaveChangesAsync(ct);
        return item.ConvertToResponse();
    }
}