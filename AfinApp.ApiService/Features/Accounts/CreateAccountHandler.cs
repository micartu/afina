using Mediator;
using AfinApp.Data.Shared;
using AfinApp.Data.Context;
using FluentValidation;
using AfinApp.Data.Entities;
using AfinApp.ApiService.Features.Accounts.Requests;

namespace AfinApp.ApiService.Features.Accounts;

public class CreateAccountHandler(IAccountRelevantContext ctx, IValidator<IAccountRequest> val) : IRequestHandler<CreateAccountRequest, Result<AccountResponse>>
{
    public async ValueTask<Result<AccountResponse>> Handle(CreateAccountRequest req, CancellationToken ct)
    {
        if (!val.Validate(req).IsValid)
            return new Error(ErrorType.Validation, Const.apiAccounts, "Validation failed");
        var item = new Account
        {
            Id = Guid.NewGuid(),
            FamilyId = req.FamilyId,
            Name = req.Name,
            Type = req.Type,
            Currency = req.Currency,
            InitialBalance = req.InitialBalance
        };
        ctx.Accounts.Add(item);
        await ctx.SaveChangesAsync(ct);
        return item.ConvertToResponse();
    }
}