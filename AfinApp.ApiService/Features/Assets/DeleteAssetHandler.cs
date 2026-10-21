using Mediator;
using AfinApp.Data.Shared;
using AfinApp.Data.Context;
using AfinApp.ApiService.Features.Assets.Requests;

namespace AfinApp.ApiService.Features.Assets;

public class DeleteAssetHandler(IAssetRelevantContext ctx) : IRequestHandler<DeleteAssetRequest, Result>
{
    public async ValueTask<Result> Handle(DeleteAssetRequest req, CancellationToken ct)
    {
        var item = await ctx.Assets.FindAsync([req.Id], ct);
        if (item == null)
            return new Error(ErrorType.NotFound, $"{Const.apiAssets}.NotFound", "Not found");
        ctx.Assets.Remove(item);
        await ctx.SaveChangesAsync(ct);
        return Result.Success();
    }
}