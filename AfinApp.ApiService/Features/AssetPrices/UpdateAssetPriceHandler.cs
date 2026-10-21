using Mediator;
using AfinApp.Data.Shared;
using AfinApp.Data.Context;
using FluentValidation;
using AfinApp.ApiService.Features.AssetPrices.Requests;

namespace AfinApp.ApiService.Features.AssetPrices;

public class UpdateAssetPriceHandler(IAssetPriceRelevantContext ctx, IValidator<IAssetPriceRequest> val) : IRequestHandler<UpdateAssetPriceRequest, Result<AssetPriceResponse>>
{
    public async ValueTask<Result<AssetPriceResponse>> Handle(UpdateAssetPriceRequest req, CancellationToken ct)
    {
        if (!val.Validate(req).IsValid)
            return new Error(ErrorType.Validation, Const.apiAssetPrices, "Validation failed");
        var item = await ctx.AssetPrices.FindAsync([req.Id], ct);
        if (item == null)
            return new Error(ErrorType.NotFound, $"{Const.apiAssetPrices}.NotFound", "Not found");
        item.AssetId = req.AssetId;
        item.Date = req.Date;
        item.ClosePrice = req.ClosePrice;
        await ctx.SaveChangesAsync(ct);
        return item.ConvertToResponse();
    }
}