using Mediator;
using AfinApp.Data.Shared;
using AfinApp.Data.Context;
using FluentValidation;
using AfinApp.Data.Entities;
using AfinApp.ApiService.Features.AssetPrices.Requests;

namespace AfinApp.ApiService.Features.AssetPrices;

public class CreateAssetPriceHandler(IAssetPriceRelevantContext ctx, IValidator<IAssetPriceRequest> val) : IRequestHandler<CreateAssetPriceRequest, Result<AssetPriceResponse>>
{
    public async ValueTask<Result<AssetPriceResponse>> Handle(CreateAssetPriceRequest req, CancellationToken ct)
    {
        if (!val.Validate(req).IsValid)
            return new Error(ErrorType.Validation, Const.apiAssetPrices, "Validation failed");
        var item = new AssetPrice
        {
            Id = Guid.NewGuid(),
            AssetId = req.AssetId,
            Date = req.Date,
            ClosePrice = req.ClosePrice
        };
        ctx.AssetPrices.Add(item);
        await ctx.SaveChangesAsync(ct);
        return item.ConvertToResponse();
    }
}