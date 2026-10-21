using Mediator;
using AfinApp.Data.Shared;
using AfinApp.Data.Context;
using AfinApp.ApiService.Features.AssetPrices.Requests;

namespace AfinApp.ApiService.Features.AssetPrices;

public class DeleteAssetPriceHandler(IAssetPriceRelevantContext ctx) : IRequestHandler<DeleteAssetPriceRequest, Result>
{
    public async ValueTask<Result> Handle(DeleteAssetPriceRequest req, CancellationToken ct)
    {
        var item = await ctx.AssetPrices.FindAsync([req.Id], ct);
        if (item == null)
            return new Error(ErrorType.NotFound, $"{Const.apiAssetPrices}.NotFound", "Not found");
        ctx.AssetPrices.Remove(item);
        await ctx.SaveChangesAsync(ct);
        return Result.Success();
    }
}