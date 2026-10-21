using Mediator;
using AfinApp.Data.Shared;
using AfinApp.Data.Context;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using AfinApp.ApiService.Features.Accounts.Requests;

namespace AfinApp.ApiService.Features.Accounts;

public class GetAccountByIdHandler(IAccountRelevantContext ctx) : IRequestHandler<GetAccountByIdRequest, Result<AccountResponse>>
{
    public async ValueTask<Result<AccountResponse>> Handle(GetAccountByIdRequest req, CancellationToken ct)
    {
        var item = await ctx.Accounts.Where(x => x.Id == req.Id)
            .Select(x => x.ConvertToResponse())
            .FirstOrDefaultAsync(ct);
        return item != null
            ? item
            : new Error(ErrorType.NotFound, $"{Const.apiAccounts}.NotFound", "Not found");
    }
}