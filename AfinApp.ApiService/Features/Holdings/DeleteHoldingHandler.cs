using Mediator;
using AfinApp.Data.Shared;
using AfinApp.Data.Context;
using AfinApp.ApiService.Features.Holdings.Requests;

namespace AfinApp.ApiService.Features.Holdings;

public class DeleteHoldingHandler(IHoldingRelevantContext ctx) : IRequestHandler<DeleteHoldingRequest, Result>
{
    public async ValueTask<Result> Handle(DeleteHoldingRequest req, CancellationToken ct)
    {
        var item = await ctx.Holdings.FindAsync([req.Id], ct);
        if (item == null)
            return new Error(ErrorType.NotFound, $"{Const.apiHoldings}.NotFound", "Not found");
        ctx.Holdings.Remove(item);
        await ctx.SaveChangesAsync(ct);
        return Result.Success();
    }
}