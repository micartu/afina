using Mediator;
using AfinApp.Data.Shared;
using AfinApp.Data.Context;
using AfinApp.ApiService.Features.Accounts.Requests;

namespace AfinApp.ApiService.Features.Accounts;

public class DeleteAccountHandler(IAccountRelevantContext ctx) : IRequestHandler<DeleteAccountRequest, Result>
{
    public async ValueTask<Result> Handle(DeleteAccountRequest req, CancellationToken ct)
    {
        var item = await ctx.Accounts.FindAsync([req.Id], ct);
        if (item == null)
            return new Error(ErrorType.NotFound, $"{Const.apiAccounts}.NotFound", "Not found");
        ctx.Accounts.Remove(item);
        await ctx.SaveChangesAsync(ct);
        return Result.Success();
    }
}