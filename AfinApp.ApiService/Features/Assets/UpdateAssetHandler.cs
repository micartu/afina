using Mediator;
using AfinApp.Data.Shared;
using AfinApp.Data.Context;
using FluentValidation;
using AfinApp.ApiService.Features.Assets.Requests;

namespace AfinApp.ApiService.Features.Assets;

public class UpdateAssetHandler(IAssetRelevantContext ctx, IValidator<IAssetRequest> val) : IRequestHandler<UpdateAssetRequest, Result<AssetResponse>>
{
    public async ValueTask<Result<AssetResponse>> Handle(UpdateAssetRequest req, CancellationToken ct)
    {
        if (!val.Validate(req).IsValid)
            return new Error(ErrorType.Validation, Const.apiAssets, "Validation failed");
        var item = await ctx.Assets.FindAsync([req.Id], ct);
        if (item == null)
            return new Error(ErrorType.NotFound, $"{Const.apiAssets}.NotFound", "Not found");
        item.Ticker = req.Ticker;
        item.Name = req.Name;
        item.AssetType = req.AssetType;
        await ctx.SaveChangesAsync(ct);
        return item.ConvertToResponse();
    }
}