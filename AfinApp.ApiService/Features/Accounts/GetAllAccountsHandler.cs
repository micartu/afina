using Mediator;
using AfinApp.Data.Context;
using Microsoft.EntityFrameworkCore;
using AfinApp.ApiService.Features.Accounts.Requests;

namespace AfinApp.ApiService.Features.Accounts;

public class GetAllAccountsHandler(IAccountRelevantContext ctx) : IRequestHandler<GetAllAccountsRequest, IEnumerable<AccountResponse>>
{
    public async ValueTask<IEnumerable<AccountResponse>> Handle(GetAllAccountsRequest req, CancellationToken ct)
        => await ctx.Accounts.Select(x => x.ConvertToResponse()).ToListAsync(ct);
}